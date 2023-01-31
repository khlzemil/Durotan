namespace Web.Areas.Admin.ViewModels.Company
{
    public class CompanyUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string CompanyPhotoName { get; set; }
        public IFormFile CompanyPhoto { get; set; }
    }
}
