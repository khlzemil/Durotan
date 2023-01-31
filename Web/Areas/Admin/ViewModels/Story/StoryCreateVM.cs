namespace Web.Areas.Admin.ViewModels.Story
{
    public class StoryCreateVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IFormFile StoryPhoto { get; set; }
    }
}
