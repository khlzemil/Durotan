using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFormRepository _formRepository;

        public ContactService(IFormRepository formRepository ,
            IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _formRepository = formRepository;
        }


        public async Task<bool> CreateAsync(ContactIndexVM model)
        {
            if (model.Status == null)
            {
                model.Status = 0;
            }
            if (!_modelState.IsValid) return false;

            var form = new Form
            {
                Subject = model.Subject,
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            await _formRepository.CreateAsync(form);

            return true;
        }
    }
}
