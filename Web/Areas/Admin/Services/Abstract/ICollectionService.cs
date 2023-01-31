using Web.Areas.Admin.ViewModels.Collection;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ICollectionService
    {
        Task<CollectionIndexVM> GetAllAsync();

        Task<bool> CreateAsync(CollectionCreateVM model);

        Task<CollectionUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(CollectionUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
