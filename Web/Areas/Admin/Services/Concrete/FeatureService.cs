using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Feature;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public FeatureService(IFeatureRepository featureRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _featureRepository = featureRepository;

            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<FeatureIndexVM> GetAllAsync()
        {
            var model = new FeatureIndexVM
            {
                Features = await _featureRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(FeatureCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _featureRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda feature mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.FeaturePhoto))
            {
                _modelState.AddModelError("FeaturePhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.FeaturePhoto, 1024))
            {
                _modelState.AddModelError("FeaturePhoto", "File olcusu 1024 kbdan boyukdur");
                return false;
            }



            var feature = new Feature
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.FeaturePhoto),
            };

            await _featureRepository.CreateAsync(feature);
            return true;
        }


        public async Task<FeatureUpdateVM> GetUpdateModelAsync(int id)
        {


            var feature = await _featureRepository.GetAsync(id);

            if (feature == null) return null;

            var model = new FeatureUpdateVM
            {
                Id = feature.Id,
                Title = feature.Title,
                Description = feature.Description,
                FeaturePhotoName = feature.PhotoName
            };

            return model;

        }


        public async Task<bool> UpdateAsync(FeatureUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _featureRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda başlıq mövcuddur");
                return false;
            }
            if (model.FeaturePhoto != null)
            {
                if (!_fileService.IsImage(model.FeaturePhoto))
                {
                    _modelState.AddModelError("FeaturePhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.FeaturePhoto, 1024))
                {
                    _modelState.AddModelError("FeaturePhoto", "File olcusu 1024 kbdan boyukdur");
                    return false;
                }
            }

            var feature = await _featureRepository.GetAsync(model.Id);




            if (feature != null)
            {
                feature.Id = model.Id;
                feature.Title = model.Title;
                feature.ModifiedAt = DateTime.Now;
                feature.Description = model.Description;


                if (model.FeaturePhoto != null)
                {
                    feature.PhotoName = await _fileService.UploadAsync(model.FeaturePhoto);
                }

                await _featureRepository.UpdateAsync(feature);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var feature = await _featureRepository.GetAsync(id);
            if (feature != null)
            {
                _fileService.Delete(feature.PhotoName);

                await _featureRepository.DeleteAsync(feature);

                return true;

            }

            return false;
        }
    }
}
