using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly ICompanyRepository _companyRepository;

        public AboutService(IStoryRepository storyRepository, ICompanyRepository companyRepository, IActionContextAccessor actionContextAccessor)
        {
            _storyRepository = storyRepository;
            _companyRepository = companyRepository;
        }


        public async Task<AboutIndexVM> GetAllAsync()
        {
            var model = new AboutIndexVM
            {
                Stories = await _storyRepository.GetAllAsync(),
                Companies = await _companyRepository.GetAllAsync()
            };
            return model;

        }
    }
}
