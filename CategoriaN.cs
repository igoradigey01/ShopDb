using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb
{
    public class CategoriaN
    {
        public CategoriaN()
        {
            Katalogs = new HashSet<KatalogN>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
     //   public bool RoleF { get; set; } // Crlient Fornitura render
      //  public bool RoleT { get; set; } // Ткани Рендер
        public bool Flag_link { get; set; }  // есть переход на внешнюю или внутреннюю ссылку заданню в Link
        public bool Flag_href { get; set; }  // это  переход на внешнюю ссылку 
        public string Link { get; set; }
        public bool Hidden { get; set; }
        public string DecriptSEO { get; set; }
        public int PostavchikId { get; set; }
        public virtual PostavchikN Postavchik { get; set; }
        public virtual ICollection<KatalogN> Katalogs { get; set; }
    }
}
