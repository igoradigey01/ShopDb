using System;
using System.Collections.Generic;

#nullable disable

namespace ShopDb
{        /// <summary>
///  img связанные с product
/// </summary>
    public partial class ImageP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
