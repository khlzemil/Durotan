using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductPhoto : BaseEntity
    {
        public string? PhotoName { get; set; }
        public int ProductId { get; set; }
        public int Order { get; set; }
        public Product  Product{ get; set; }
    }
}
