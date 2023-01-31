using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurJournal;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurJournalService : IOurJournalService
    {
        private readonly IOurJournalRepository _ourJournalRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public OurJournalService(IOurJournalRepository ourJournalRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _ourJournalRepository = ourJournalRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<OurJournalIndexVM> GetAllAsync()
        {
            var model = new OurJournalIndexVM
            {
                OurJournal = await _ourJournalRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(OurJournalCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _ourJournalRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda blog mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.JournalPhoto))
            {
                _modelState.AddModelError("CollectionPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.JournalPhoto, 1024))
            {
                _modelState.AddModelError("CollectionPhoto", "File olcusu 1024 kbdan boyukdur");
                return false;
            }



            var ourJournal = new OurJournal
            {
                Id = model.Id,
                Title = model.Title,
                BlogDate = model.BlogDate,
                BlogName = model.BlogName,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.JournalPhoto),
            };

            await _ourJournalRepository.CreateAsync(ourJournal);
            return true;
        }


        public async Task<OurJournalUpdateVM> GetUpdateModelAsync(int id)
        {


            var blog = await _ourJournalRepository.GetAsync(id);

            if (blog == null) return null;

            var model = new OurJournalUpdateVM
            {
                Id = blog.Id,
                Title = blog.Title,
                BlogDate = blog.BlogDate,
                BlogName = blog.BlogName,
                JournalPhotoName = blog.PhotoName
            };

            return model;

        }


        public async Task<bool> UpdateAsync(OurJournalUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _ourJournalRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda başlıq mövcuddur");
                return false;
            }
            if (model.JournalPhoto != null)
            {
                if (!_fileService.IsImage(model.JournalPhoto))
                {
                    _modelState.AddModelError("JournalPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.JournalPhoto, 1024))
                {
                    _modelState.AddModelError("JournalPhoto", "File olcusu 1024 kbdan boyukdur");
                    return false;
                }
            }

            var ourJournal = await _ourJournalRepository.GetAsync(model.Id);




            if (ourJournal != null)
            {
                ourJournal.Id = model.Id;
                ourJournal.Title = model.Title;
                ourJournal.ModifiedAt = DateTime.Now;
                ourJournal.BlogName = model.BlogName;
                ourJournal.BlogDate = model.BlogDate;


                if (model.JournalPhoto != null)
                {
                    ourJournal.PhotoName = await _fileService.UploadAsync(model.JournalPhoto);
                }

                await _ourJournalRepository.UpdateAsync(ourJournal);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var ourJournal = await _ourJournalRepository.GetAsync(id);
            if (ourJournal != null)
            {
                _fileService.Delete(ourJournal.PhotoName);

                await _ourJournalRepository.DeleteAsync(ourJournal);

                return true;
            }

            return false;
        }
    }
}
