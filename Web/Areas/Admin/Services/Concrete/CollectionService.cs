using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Collection;

namespace Web.Areas.Admin.Services.Concrete
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public CollectionService(ICollectionRepository collectionRepository , IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _collectionRepository = collectionRepository;

            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<CollectionIndexVM> GetAllAsync()
        {
            var model = new CollectionIndexVM
            {
                Collections = await _collectionRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(CollectionCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _collectionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda collection mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.CollectionPhoto))
            {
                _modelState.AddModelError("CollectionPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.CollectionPhoto, 1024))
            {
                _modelState.AddModelError("CollectionPhoto", "File olcusu 1024 kbdan boyukdur");
                return false;
            }



            var collection = new Collection
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                ExploreButton = model.ExploreButton,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                CollectionPhotoName = await _fileService.UploadAsync(model.CollectionPhoto),
            };

            await _collectionRepository.CreateAsync(collection);
            return true;
        }


        public async Task<CollectionUpdateVM> GetUpdateModelAsync(int id)
        {


            var collection = await _collectionRepository.GetAsync(id);

            if (collection == null) return null;

            var model = new CollectionUpdateVM
            {
                Id = collection.Id,
                Title = collection.Title,
                Text = collection.Text,
                ExploreButton = collection.ExploreButton,
                Description = collection.Description,
                CollectionPhotoName = collection.CollectionPhotoName
            };

            return model;

        }


        public async Task<bool> UpdateAsync(CollectionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _collectionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda başlıq mövcuddur");
                return false;
            }
            if (model.CollectionPhoto != null)
            {
                if (!_fileService.IsImage(model.CollectionPhoto))
                {
                    _modelState.AddModelError("CollectionPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.CollectionPhoto, 1024))
                {
                    _modelState.AddModelError("CollectionPhoto", "File olcusu 1024 kbdan boyukdur");
                    return false;
                }
            }

            var collection = await _collectionRepository.GetAsync(model.Id);




            if (collection != null)
            {
                collection.Id = model.Id;
                collection.Title = model.Title;
                collection.ModifiedAt = DateTime.Now;
                collection.Description = model.Description;
                collection.Text = model.Text;
                collection.ExploreButton = model.ExploreButton;


                if (model.CollectionPhoto != null)
                {
                    collection.CollectionPhotoName = await _fileService.UploadAsync(model.CollectionPhoto);
                }

                await _collectionRepository.UpdateAsync(collection);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var collection = await _collectionRepository.GetAsync(id);
            if (collection != null)
            {
                _fileService.Delete(collection.CollectionPhotoName);

                await _collectionRepository.DeleteAsync(collection);

                return true;

            }

            return false;
        }
    }
}
