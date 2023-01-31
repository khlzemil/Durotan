using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IContactService
    {
        Task<bool> CreateAsync(ContactIndexVM model);
    }
}
