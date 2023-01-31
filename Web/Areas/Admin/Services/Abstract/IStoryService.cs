using Web.Areas.Admin.ViewModels.Story;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStoryService
    {
        Task<StoryIndexVM> GetAllAsync();

        Task<bool> CreateAsync(StoryCreateVM model);

        Task<StoryUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(StoryUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
