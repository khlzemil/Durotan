using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Specificity { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int OldCost { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string MainPhotoName { get; set; }

        public ICollection<ProductPhoto> ProductPhotos { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
