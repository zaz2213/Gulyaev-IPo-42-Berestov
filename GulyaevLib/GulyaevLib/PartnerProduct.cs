using System;
using System.Collections.Generic;

namespace GulyaevLib;

public partial class PartnerProduct
{
    public int Id { get; set; }

    public int? Product { get; set; }

    public int Partner { get; set; }

    public int? PartnerCount { get; set; }

    public DateOnly? SaleDate { get; set; }

    public virtual Partner? PartnerNavigation { get; set; }

    public virtual Product? ProductNavigation { get; set; }
}
