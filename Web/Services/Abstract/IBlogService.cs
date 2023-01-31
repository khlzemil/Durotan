using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IBlogService
    {
        Task<BlogIndexVM> GetAllAsync(BlogIndexVM model);
    }
}
