namespace Web.ViewModels
{
    public class HomeIndexVM
    {
        public List<Core.Entities.HomeMainSlider> HomeMainSliders { get; set; }
        public List<Core.Entities.Brand> Brands { get; set; }
        public List<Core.Entities.Feature> Features { get; set; }
        public List<Core.Entities.Collection> Collections{ get; set; }
        public List<Core.Entities.Article> Articles{ get; set; }
        public List<Core.Entities.Product> Products { get; set; }
    }
}
