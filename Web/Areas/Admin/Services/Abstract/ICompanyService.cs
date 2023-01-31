using Web.Areas.Admin.ViewModels.Company;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ICompanyService
    {
        Task<CompanyIndexVM> GetAllAsync();

        Task<bool> CreateAsync(CompanyCreateVM model);

        Task<CompanyUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(CompanyUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
