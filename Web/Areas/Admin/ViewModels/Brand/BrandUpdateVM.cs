namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public int Id { get; set; }
        public string? BrandPhotoName { get; set; }
        public IFormFile? BrandPhoto { get; set; }
    }
}
