using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductIndexVM> GetAllAsync();
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(ProductUpdateVM model);

        Task<bool> DeleteAsync(int id);
        Task<bool> DeletePhotoAsync(int id);

        Task<ProductPhotoUpdateVM> GetPhotoUpdateModelAsync(int id);
        Task<bool> UpdatePhotoAsync(ProductPhotoUpdateVM model);


        Task<bool> AddTagsAsync(ProductAddTagsVM model);
        Task<ProductAddTagsVM> GetAddTagsModelAsync(int id);
    }
}
