namespace Web.Areas.Admin.ViewModels.Story
{
    public class StoryUpdateVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IFormFile StoryPhoto { get; set; }
        public string StoryPhotoName { get; set; }
    }
}
