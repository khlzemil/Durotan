using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OurJournal : BaseEntity
    {
        public string Title { get; set; }
        public string BlogName { get; set; }
        public DateTime BlogDate { get; set; }
        public string PhotoName { get; set; }
    }
}
