using Web.Areas.Admin.ViewModels.Article;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IArticleService
    {
        Task<ArticleIndexVM> GetAllAsync();

        Task<bool> CreateAsync(ArticleCreateVM model);

        Task<ArticleUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(ArticleUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
