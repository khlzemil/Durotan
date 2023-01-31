using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFeatureRepository _featureRepository;

        public HomeService( IHomeMainSliderRepository homeMainSliderRepository, IProductRepository productRepository ,IArticleRepository articleRepository ,ICollectionRepository collectionRepository ,IBrandRepository brandRepository, IFeatureRepository featureRepository ,IActionContextAccessor actionContextAccessor)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _collectionRepository = collectionRepository;
            _brandRepository = brandRepository;
            _featureRepository = featureRepository;
        }


        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                Features = await _featureRepository.GetAllAsync(),
                Collections = await _collectionRepository.GetAllAsync(),
                Articles = await _articleRepository.GetTwoArticle(),
                Products = await _productRepository.GetFirstFiveProductAsync()
            };
            return model;

        }
    }
}
