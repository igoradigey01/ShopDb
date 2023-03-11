using System;
using System.Collections.Generic;


namespace ShopDb
{
    public  class CategoriaP
    {
        public CategoriaP()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Hidden { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
