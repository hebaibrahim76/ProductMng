using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int IdSub { get; set; }
        public int? IdCategory { get; set; }
        public string Name { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
