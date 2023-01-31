using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IHomeService
    {
        Task<HomeIndexVM> GetAllAsync();
    }
}
