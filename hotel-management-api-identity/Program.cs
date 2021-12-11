using Autofac;
using Autofac.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.MiddlewareExtensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341")
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerService();

    var jwtTokenSettings = builder.Configuration.GetSection("JwtTokenSettings");
    builder.Services.Configure<JwtToken>(jwtTokenSettings);
    builder.Services.AddIdentity(jwtTokenSettings.Get<JwtToken>());
    


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("pol",
        builder =>
        {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
    builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
    builder.Services.AddHealthChecks();
    
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacContainerModule());        
    });

    var app = builder.Build();
    app.ConfigureExceptionHandler();
    app.UseSerilogRequestLogging();
    



    var supportedCultures = new[] { new CultureInfo("en-GB"), new CultureInfo("en-US") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-GB"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

    app.UseSwaggerService(app.Environment);
    app.UseRouting();
    app.UseCors("pol");
    app.UseHttpsRedirection();
    
    

    app.UseAuthorization();
    app.UseAuthenticationMiddleware();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            AllowCachingResponses = false,
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapControllers();           
    });

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}