namespace Web.Areas.Admin.ViewModels.Collection
{
    public class CollectionCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string ExploreButton { get; set; }
        public IFormFile CollectionPhoto { get; set; }
    }
}
