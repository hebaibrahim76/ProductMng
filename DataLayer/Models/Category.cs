using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
