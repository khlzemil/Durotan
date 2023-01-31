using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IOurJournalRepository _ourJournalRepository;
        private readonly IArticleRepository _articleRepository;

        public BlogService(IOurJournalRepository ourJournalRepository, IArticleRepository articleRepository , IActionContextAccessor actionContextAccessor)
        {
            _ourJournalRepository = ourJournalRepository;
            _articleRepository = articleRepository;
        }


        public async Task<BlogIndexVM> GetAllAsync(BlogIndexVM model)
        {
            var pageCount = await _articleRepository.GetPageCountAsync(model.Take);

            if (model.Page <= 0) return model;

            var articles = await _articleRepository.PaginateBlogsAsync(model.Page, model.Take);

            model = new BlogIndexVM
            {
                OurJournals = await _ourJournalRepository.GetAllAsync(),
                Articles = articles,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                TwoArticles = await _articleRepository.GetTwoArticle(),
            };
            return model;

        }
    }
}
