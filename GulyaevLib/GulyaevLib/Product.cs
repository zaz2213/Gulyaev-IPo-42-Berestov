using System;
using System.Collections.Generic;

namespace GulyaevLib;

public partial class Product
{
    public int Id { get; set; }

    public int? ProductType { get; set; }

    public string? ProductName { get; set; }

    public int? PartnerArtc { get; set; }

    public float? PartnerPrice { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual ProductType? ProductTypeNavigation { get; set; }
}
