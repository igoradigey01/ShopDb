using System;
using System.Collections.Generic;

#nullable disable

namespace ShopDb
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<ImageP>();
            ProductNomenclatures = new HashSet<ProductNomenclature>();
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Markup { get; set; }
        public string Description { get; set; }
        public string ImageGuid { get; set; }  // name image
        public int KatalogId { get; set; }
        public int MaterialId { get; set; }
        public int CategoriaId { get; set; }
        public virtual KatalogP Katalog { get; set; }
        public virtual CategoriaP Categoria { get; set; }
        public virtual MaterialP Material { get; set; }
        public virtual ICollection<ImageP> Images { get; set; }
        public virtual ICollection<ProductNomenclature> ProductNomenclatures { get; set; }
    }
}
