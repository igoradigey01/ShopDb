using System;
using System.Collections.Generic;

#nullable disable

namespace ShopDb
{
    public partial class MaterialP
    {
        public MaterialP()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
