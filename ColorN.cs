using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb
{
    public class ColorN
    {
        public ColorN()
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
