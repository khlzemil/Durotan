using Web.Areas.Admin.ViewModels.Feature;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFeatureService
    {
        Task<FeatureIndexVM> GetAllAsync();

        Task<bool> CreateAsync(FeatureCreateVM model);

        Task<FeatureUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(FeatureUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
