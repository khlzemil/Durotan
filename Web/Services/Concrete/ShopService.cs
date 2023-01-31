using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IProductRepository _productRepository;
        private readonly IProductPhotoRepository _productPhotoRepository;
        private readonly ITagRepository _tagRepository;

        public ShopService(IProductRepository productRepository,IProductPhotoRepository productPhotoRepository ,ITagRepository tagRepository ,
            IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productRepository = productRepository;
            _productPhotoRepository = productPhotoRepository;
            _tagRepository = tagRepository;
        }


        public async Task<ShopIndexVM> GetAllAsync(ShopIndexVM model)
        {
            var pageCount = await _productRepository.GetPageCountAsync(model.Take, model.Title);

            if (model.Page <= 0 ) return model;

            var products = await _productRepository.Filter(model.Title, model.Page, model.Take);

            model = new ShopIndexVM
            {
                Products = products,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                Title = model.Title,
                Tags = await _tagRepository.GetAllAsync(),

            };
            return model;

        }

        public async Task<ShopDetailsVM> GetAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ShopDetailsVM
            {
                Id = product.Id,
                Specificity = product.Specificity,
                Name = product.Name,
                MainPhotoName = product.MainPhotoName,
                Cost = product.Cost,
                Color = product.Color,
                Description = product.Description,
                Status = product.ProductStatus,
                ProductPhotos = await _productRepository.GetProductWithPhotosAsync(product.Id),
                Products = await _productRepository.GetFirstFourProductAsync()
            };
            return model;

        }


        public async Task<ShopTagProductIndexVM> TagProductAsync(int id)
        {
            var tag = await _tagRepository.GetWithProductsAsync(id);
            if (tag == null) return null;

            var model = new ShopTagProductIndexVM
            {
                Tag = tag
            };
            return model;
        }
    }
}
