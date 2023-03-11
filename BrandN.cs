using System.Collections.Generic;


namespace ShopDb
{
    public class BrandN
    {
        public BrandN()
        {
            Nomenclatures = new HashSet<Nomenclature>();
        }
        public int Id { get; set; }       
        public string Name { get; set; }       
        public bool Hidden { get; set; }
        public int PostavchikId { get; set; }
        public virtual PostavchikN Postavchik { get; set; }
        public virtual ICollection<Nomenclature> Nomenclatures { get; set; }
    }
}
