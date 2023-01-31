using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Article : BaseEntity
    {
        public string PhotoName { get; set; }
        public DateTime ArticleDate { get; set; }
        public string Title { get; set; }
        public string ArticleName { get; set; }
        public string Author { get; set; }
    }
}
