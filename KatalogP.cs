using System;
using System.Collections.Generic;

#nullable disable

namespace ShopDb
{
    public partial class KatalogP
    {
        public KatalogP()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public bool Flag_link { get; set; }  // есть переход на внешнюю или внутреннюю ссылку заданню в Link
        public bool Flag_href { get; set; }  // это  переход на внешнюю ссылку 
        public string Link { get; set; }
        public string DecriptSEO { get; set; }
        public string KeywordsSEO { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
