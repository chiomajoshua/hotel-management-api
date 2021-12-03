using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers;
using hotel_management_api_identity.Core.Storage.QueryRepository;
using hotel_management_api_identity.Features.Enquiry.Menu.Config;
using hotel_management_api_identity.Features.Enquiry.Menu.Model;

namespace hotel_management_api_identity.Features.Enquiry.Menu.Service
{
    public interface IMenuService : IAutoDependencyCore
    {
        /// <summary>
        /// Fetches Menu By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<GenericResponse<MenuResponse>> GetMenuByName(string name);

        /// <summary>
        /// Get All Menu
        /// </summary>
        /// <returns></returns>
        Task<GenericResponse<IEnumerable<MenuResponse>>> GetAllMenu(int pageSize, int pageNumber);

        /// <summary>
        /// Check if Menu Exists Before Creating
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsMenuExists(string name);
    }


    public class MenuService : IMenuService
    {
        private readonly ILogger<MenuService> _logger;
        private readonly IDapperQuery<Core.Storage.Models.Menu> _menuQuery;
        public MenuService(IDapperQuery<Core.Storage.Models.Menu> menuQuery, ILogger<MenuService> logger)
        {
            _logger = logger;
            _menuQuery = menuQuery;
        }

        public async Task<GenericResponse<IEnumerable<MenuResponse>>> GetAllMenu(int pageSize, int pageNumber)
        {
            try
            {
                var result = await _menuQuery.GetByAsync(pageSize, pageNumber);
                if (result is not null) return new GenericResponse<IEnumerable<MenuResponse>> { Data = result.ToList().ToMenuList(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<IEnumerable<MenuResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<IEnumerable<MenuResponse>> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<GenericResponse<MenuResponse>> GetMenuByName(string name)
        {
            try
            {
                var query = new Dictionary<string, string>() { { "Item", name } };
                var result = await _menuQuery.GetByDefaultAsync(query);
                if (result is not null) return new GenericResponse<MenuResponse> { Data = result.ToMenu(), IsSuccessful = true, Message = ResponseMessages.OperationSuccessful };
                return new GenericResponse<MenuResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new GenericResponse<MenuResponse> { IsSuccessful = false, Message = ResponseMessages.NoRecordFound };
            }
        }

        public async Task<bool> IsMenuExists(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return false;
                var query = new Dictionary<string, string>() { { "Item", name } };
                var result = await _menuQuery.IsExistAsync(query);
                if (result) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}