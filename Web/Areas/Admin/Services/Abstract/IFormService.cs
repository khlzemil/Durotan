using Web.Areas.Admin.ViewModels.Form;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFormService
    {
        Task<FormIndexVM> GetAllAsync();
        Task<FormDetailsVM> GetDetailsModelAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangeStatus(int id);
    }
}
