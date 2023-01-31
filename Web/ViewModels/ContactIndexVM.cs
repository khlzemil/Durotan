using Core.Constants;

namespace Web.ViewModels
{
    public class ContactIndexVM
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public FormStatus? Status { get; set; } = 0;
    }
}
