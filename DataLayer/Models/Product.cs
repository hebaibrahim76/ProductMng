using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class Product
    {
        public int IdPr { get; set; }
        public string Name { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public DateTime? Expiraydate { get; set; }

        public  SubCategory SubCategory { get; set; }

        
    }
}
