using Core.Constants;
using Core.Entities;

namespace Web.ViewModels
{
    public class ShopDetailsVM
    {
        public int Id { get; set; }
        public List<Core.Entities.ProductPhoto> ProductPhotos { get; set; }
        public Core.Entities.Product Product { get; set; }
        public int ProductId { get; set; }
        public string Specificity { get; set; }
        public string Name { get; set; }
        public string MainPhotoName { get; set; }
        public int Cost { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
