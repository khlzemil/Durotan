namespace Web.ViewModels
{
    public class ShopIndexVM
    {
        public List<Core.Entities.Product>?  Products{ get; set; }
        public List<Core.Entities.Tag> Tags { get; set; }

        public string? Title { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 6;

        public int PageCount { get; set; }
    }
}
