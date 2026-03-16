using System;
using System.Collections.Generic;

namespace GulyaevLib;

public partial class ProductType
{
    public int Id { get; set; }

    public string? ProductType1 { get; set; }

    public float? CoafProductType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
