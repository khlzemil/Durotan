using Core.Constants;
using Core.Entities;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public ProductUpdateVM()
        {
            Photos = new List<IFormFile>();
        }
        public int Id { get; set; }
        public string Specificity { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int OldCost { get; set; }
        public string? MainPhotoName { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public ICollection<ProductPhoto>? ProductPhotos { get; set; }
    }
}
