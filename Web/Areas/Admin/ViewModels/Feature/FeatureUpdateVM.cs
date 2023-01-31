namespace Web.Areas.Admin.ViewModels.Feature
{
    public class FeatureUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? FeaturePhotoName { get; set; }
        public IFormFile? FeaturePhoto { get; set; }
    }
}
