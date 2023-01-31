using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Company;

namespace Web.Areas.Admin.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public CompanyService(ICompanyRepository companyRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _companyRepository = companyRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<CompanyIndexVM> GetAllAsync()
        {
            var model = new CompanyIndexVM
            {
                Companies = await _companyRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(CompanyCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _companyRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda company mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.CompanyPhoto))
            {
                _modelState.AddModelError("CompanyPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.CompanyPhoto, 1024))
            {
                _modelState.AddModelError("CompanyPhoto", "File olcusu 1024 kbdan boyukdur");
                return false;
            }



            var company = new Company
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.CompanyPhoto),
            };

            await _companyRepository.CreateAsync(company);
            return true;
        }


        public async Task<CompanyUpdateVM> GetUpdateModelAsync(int id)
        {


            var company = await _companyRepository.GetAsync(id);

            if (company == null) return null;

            var model = new CompanyUpdateVM
            {
                Id = company.Id,
                Title = company.Title,
                Text = company.Text,
                Description = company.Description,
                CompanyPhotoName = company.PhotoName
            };

            return model;

        }


        public async Task<bool> UpdateAsync(CompanyUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _companyRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda başlıq mövcuddur");
                return false;
            }
            if (model.CompanyPhoto != null)
            {
                if (!_fileService.IsImage(model.CompanyPhoto))
                {
                    _modelState.AddModelError("CompanyPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.CompanyPhoto, 1024))
                {
                    _modelState.AddModelError("CompanyPhoto", "File olcusu 1024 kbdan boyukdur");
                    return false;
                }
            }

            var collection = await _companyRepository.GetAsync(model.Id);




            if (collection != null)
            {
                collection.Id = model.Id;
                collection.Title = model.Title;
                collection.ModifiedAt = DateTime.Now;
                collection.Description = model.Description;
                collection.Text = model.Text;


                if (model.CompanyPhoto != null)
                {
                    collection.PhotoName = await _fileService.UploadAsync(model.CompanyPhoto);
                }

                await _companyRepository.UpdateAsync(collection);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var collection = await _companyRepository.GetAsync(id);
            if (collection != null)
            {
                _fileService.Delete(collection.PhotoName);

                await _companyRepository.DeleteAsync(collection);

                return true;

            }

            return false;
        }
    }
}
