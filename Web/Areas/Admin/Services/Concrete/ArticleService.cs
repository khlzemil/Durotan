using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Article;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public ArticleService(IArticleRepository articleRepository , IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _articleRepository = articleRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<ArticleIndexVM> GetAllAsync()
        {
            var model = new ArticleIndexVM
            {
                Articles = await _articleRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(ArticleCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            if (!_fileService.IsImage(model.ArticlePhoto))
            {
                _modelState.AddModelError("ArticlePhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.ArticlePhoto, 700))
            {
                _modelState.AddModelError("ArticlePhoto", "File olcusu 700 kbdan boyukdur");
                return false;
            }


            var article = new Article
            {
                Id = model.Id,
                ArticleDate = model.ArticleDate,
                ArticleName = model.ArticleName,
                Author = model.Author,
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.ArticlePhoto),
            };

            await _articleRepository.CreateAsync(article);
            return true;
        }


        public async Task<ArticleUpdateVM> GetUpdateModelAsync(int id)
        {


            var article = await _articleRepository.GetAsync(id);

            if (article == null) return null;

            var model = new ArticleUpdateVM
            {
                Id = article.Id,
                ArticleDate = article.ArticleDate,
                ArticleName = article.ArticleName,
                Author = article.Author,
                Title = article.Title,
                ArticlePhotoName = article.PhotoName,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(ArticleUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (model.ArticlePhoto != null)
            {
                if (!_fileService.IsImage(model.ArticlePhoto))
                {
                    _modelState.AddModelError("ArticlePhoto ", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.ArticlePhoto, 700))
                {
                    _modelState.AddModelError("ArticlePhoto ", "File olcusu 700 kbdan boyukdur");
                    return false;
                }
            }

            var article = await _articleRepository.GetAsync(model.Id);


            if (article != null)
            {
                article.Id = model.Id;
                article.Author = model.Author;
                article.ArticleName = model.ArticleName;
                article.ArticleDate = model.ArticleDate;
                article.Title = model.Title;
                article.ModifiedAt = DateTime.Now;

                if (model.ArticlePhoto != null)
                {
                    article.PhotoName = await _fileService.UploadAsync(model.ArticlePhoto);
                }

                await _articleRepository.UpdateAsync(article);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _articleRepository.GetAsync(id);
            if (brand != null)
            {
                _fileService.Delete(brand.PhotoName);

                await _articleRepository.DeleteAsync(brand);

                return true;
            }

            return false;
        }
    }
}
