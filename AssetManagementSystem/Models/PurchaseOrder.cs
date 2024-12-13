using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class PurchaseOrder
{
    public int PoId { get; set; }

    public DateTime PoDate { get; set; }

    public int VndId { get; set; }

    public decimal? PoTotal { get; set; }

    public int PurchasedBy { get; set; }

    public virtual User? PurchasedByNavigation { get; set; } = null!;

    public virtual Vendor? Vnd { get; set; } = null!;
}
