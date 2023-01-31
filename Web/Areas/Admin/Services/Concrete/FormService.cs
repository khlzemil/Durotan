using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Form;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FormService : IFormService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFormRepository _formRepository;

        public FormService(IFormRepository  formRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _formRepository = formRepository;
        }

        public async Task<FormIndexVM> GetAllAsync()
        {
            var model = new FormIndexVM
            {
                Forms = await _formRepository.GetOrderForm()
            };
            return model;

        }
        public async Task<FormDetailsVM> GetDetailsModelAsync(int id)
        {
            var form = await _formRepository.GetAsync(id);
            if (form == null) return null;

            var model = new FormDetailsVM
            {
                Form = form,
            };

            return model;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var form = await _formRepository.GetAsync(id);

            if (form != null)
            {
                await _formRepository.DeleteAsync(form);

                return true;
            }
            return false;
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var form = await _formRepository.GetAsync(id);

            if (form != null)
            {
                form.Status = Core.Constants.FormStatus.Read;
                form.ModifiedAt = DateTime.Now;

                await _formRepository.UpdateAsync(form);

                return true;
            }
            return false;
        }
    }
}
