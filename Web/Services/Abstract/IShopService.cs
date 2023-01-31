using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAllAsync(ShopIndexVM model);


        Task<ShopDetailsVM> GetAsync(int id);

        Task<ShopTagProductIndexVM> TagProductAsync(int id);

        //Task<ShopProductIndexVM> CategoryProductAsync(int id);
        //Task<ShopBrandProductIndexVM> BrandProductAsync(int id);
        //Task<ShopTagProductIndexVM> TagProductAsync(int id);
    }
}
