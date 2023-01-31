using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public BrandService(IBrandRepository brandRepository , IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<BrandIndexVM> GetAllAsync()
        {
            var model = new BrandIndexVM
            {
                Brands = await _brandRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(BrandCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            if (!_fileService.IsImage(model.BrandPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.BrandPhoto, 700))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 700 kbdan boyukdur");
                return false;
            }


            var brand = new Brand
            {
                Id = model.Id,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.BrandPhoto),
            };

            await _brandRepository.CreateAsync(brand);
            return true;
        }


        public async Task<BrandUpdateVM> GetUpdateModelAsync(int id)
        {


            var brand = await _brandRepository.GetAsync(id);

            if (brand == null) return null;

            var model = new BrandUpdateVM
            {
                Id = brand.Id,
                BrandPhotoName = brand.PhotoName,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(BrandUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (model.BrandPhoto != null)
            {
                if (!_fileService.IsImage(model.BrandPhoto))
                {
                    _modelState.AddModelError("BrandPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.BrandPhoto, 700))
                {
                    _modelState.AddModelError("BrandPhoto", "File olcusu 700 kbdan boyukdur");
                    return false;
                }
            }

            var brand = await _brandRepository.GetAsync(model.Id);


            if (brand != null)
            {
                brand.Id = model.Id;
                brand.ModifiedAt = DateTime.Now;

                if (model.BrandPhoto != null)
                {
                    brand.PhotoName = await _fileService.UploadAsync(model.BrandPhoto);
                }

                await _brandRepository.UpdateAsync(brand);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand != null)
            {
                _fileService.Delete(brand.PhotoName);

                await _brandRepository.DeleteAsync(brand);

                return true;

            }

            return false;
        }
    }
}
