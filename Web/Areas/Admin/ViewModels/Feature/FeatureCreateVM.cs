namespace Web.Areas.Admin.ViewModels.Feature
{
    public class FeatureCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile FeaturePhoto { get; set; }
    }
}
