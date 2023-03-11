using System;
using System.Collections.Generic;

#nullable disable

namespace ShopDb
{
    public partial class Nomenclature
    {
        public Nomenclature()
        {
            ProductNomenclatures = new HashSet<ProductNomenclature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
      //  public Guid  { get; set; } 
      
        public string Description { get; set; }
        public float Price { get; set; }
        public float Markup { get; set; }     
        public string Guid { get; set; } // name for images in wwwroot
        public int Position { get; set; } // for by Sort in list render
        public bool Hidden { get; set; }

        public int KatalogId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int ArticleId { get; set; }
        public int PostavchikId { get; set; }
        public bool InStock { get; set; } // в наличие на складе
        public bool Sale { get; set; } // распродажа
        public virtual KatalogN Katalog { get; set; }
        public virtual ColorN Color { get; set; }
        public virtual BrandN Brand { get; set; }
        public virtual ArticleN Article { get; set; }
        public virtual PostavchikN Postavchik { get; set; }

        public virtual ICollection<ProductNomenclature> ProductNomenclatures { get; set; }
    }
}
