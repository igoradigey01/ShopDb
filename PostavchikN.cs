using System.Collections.Generic;

namespace ShopDb
{
    public class PostavchikN
    {
        public PostavchikN()
        {
            Nomenclatures = new HashSet<Nomenclature>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool Hidden { get; set; }
        public virtual ICollection<Nomenclature> Nomenclatures { get; set; }
        public virtual ICollection<CategoriaN> CategoriaNs { get; set; }
        public virtual ICollection<KatalogN> KatalogNs { get; set; }
        public virtual ICollection<ColorN> ColorNs { get; set; }    
        public virtual ICollection<BrandN> BrandNs { get; set; }
        public virtual ICollection<ArticleN> ArticleNs { get; set; }
    }
}