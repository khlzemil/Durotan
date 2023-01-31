namespace Web.Areas.Admin.ViewModels.Company
{
    public class CompanyCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public IFormFile CompanyPhoto { get; set; }
    }
}
