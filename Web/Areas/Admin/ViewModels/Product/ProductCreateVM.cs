using Core.Constants;
using Core.Entities;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public int Id { get; set; }
        public string Specificity { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int OldCost { get; set; }
        public IFormFile MainPhoto { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public List<IFormFile>? ProductPhotos { get; set; }
    }
}
