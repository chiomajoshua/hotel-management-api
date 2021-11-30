using Autofac;
using Autofac.Extensions.DependencyInjection;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.MiddlewareExtensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("pol",
        builder =>
        {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
    builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
    
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
        endpoints.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            endpoints.MapHealthChecks("/healthz");
        }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}