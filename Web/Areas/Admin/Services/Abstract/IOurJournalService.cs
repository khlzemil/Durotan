using Web.Areas.Admin.ViewModels.OurJournal;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurJournalService
    {
        Task<OurJournalIndexVM> GetAllAsync();

        Task<bool> CreateAsync(OurJournalCreateVM model);

        Task<OurJournalUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(OurJournalUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
