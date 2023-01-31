using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IAboutService
    {
        Task<AboutIndexVM> GetAllAsync();
    }
}
