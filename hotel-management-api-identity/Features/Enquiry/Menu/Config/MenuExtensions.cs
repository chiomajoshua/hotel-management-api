using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Features.Enquiry.Menu.Model;
using hotel_management_api_identity.Features.Onboarding.Models;

namespace hotel_management_api_identity.Features.Enquiry.Menu.Config
{
    public static class MenuExtensions
    {
        public static IEnumerable<MenuResponse> ToMenuList(this List<Core.Storage.Models.Menu> menuData)
        {
            new List<MenuResponse>().AddRange(menuData.Select(data => new MenuResponse()
            {
                Category = data.Category.Description(),
                CreatedById = data.CreatedById,
                CreatedOn = data.CreatedOn,
                Description = data.Description,
                Id = data.Id,
                Item = data.Item,
                ModifiedById = data.ModifiedById,
                ModifiedOn = data.ModifiedOn,
                Price = data.Price
            }));
            return new List<MenuResponse>();
        }


        public static MenuResponse ToMenu(this Core.Storage.Models.Menu menuData)
        {
            return new MenuResponse()
            {
                Category = menuData.Category.Description(),
                CreatedById = menuData.CreatedById,
                CreatedOn = menuData.CreatedOn,
                Description = menuData.Description,
                Id = menuData.Id,
                Item = menuData.Item,
                ModifiedById = menuData.ModifiedById,
                ModifiedOn = menuData.ModifiedOn,
                Price = menuData.Price
            };
        }

        public static Core.Storage.Models.Menu ToDbMenu(this CreateMenuRequest createMenuRequest)
        {
            return new Core.Storage.Models.Menu()
            {
                Item = createMenuRequest.Item,
                Price = createMenuRequest.Price,
                Category = createMenuRequest.Category,
                Description = createMenuRequest.Description
            };
        }
    }
}