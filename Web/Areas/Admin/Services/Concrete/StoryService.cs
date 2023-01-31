using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Story;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public StoryService(IStoryRepository storyRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _storyRepository = storyRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<StoryIndexVM> GetAllAsync()
        {
            var model = new StoryIndexVM
            {
                Stories = await _storyRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(StoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;


            if (!_fileService.IsImage(model.StoryPhoto))
            {
                _modelState.AddModelError("StoryPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.StoryPhoto, 1200))
            {
                _modelState.AddModelError("StoryPhoto", "File olcusu 1200 kbdan boyukdur");
                return false;
            }


            var story = new Story
            {
                Id = model.Id,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.StoryPhoto),
            };

            await _storyRepository.CreateAsync(story);
            return true;
        }


        public async Task<StoryUpdateVM> GetUpdateModelAsync(int id)
        {


            var story = await _storyRepository.GetAsync(id);

            if (story == null) return null;

            var model = new StoryUpdateVM
            {
                Id = story.Id,
                Description = story.Description,
                StoryPhotoName = story.PhotoName
            };

            return model;

        }


        public async Task<bool> UpdateAsync(StoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (model.StoryPhoto != null)
            {
                if (!_fileService.IsImage(model.StoryPhoto))
                {
                    _modelState.AddModelError("StoryPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.StoryPhoto, 700))
                {
                    _modelState.AddModelError("StoryPhoto", "File olcusu 700 kbdan boyukdur");
                    return false;
                }
            }

            var story = await _storyRepository.GetAsync(model.Id);


            if (story != null)
            {
                story.Id = model.Id;
                story.Description = model.Description;
                story.ModifiedAt = DateTime.Now;

                if (model.StoryPhoto != null)
                {
                    story.PhotoName = await _fileService.UploadAsync(model.StoryPhoto);
                }

                await _storyRepository.UpdateAsync(story);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var story = await _storyRepository.GetAsync(id);
            if (story != null)
            {
                _fileService.Delete(story.PhotoName);

                await _storyRepository.DeleteAsync(story);

                return true;

            }

            return false;
        }
    }
}
