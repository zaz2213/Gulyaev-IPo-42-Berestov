using System;
using System.Collections.Generic;

namespace GulyaevLib;

public partial class Partner
{
    public int Id { get; set; }

    public string? PartnerType { get; set; }

    public string? PartnerName { get; set; }

    public string? PartnerDirector { get; set; }

    public string? PartnerEmail { get; set; }

    public string? PartnerNumber { get; set; }

    public string? PartnerAddress { get; set; }

    public string? PartnerInn { get; set; }

    public short? PartnerRating { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}
