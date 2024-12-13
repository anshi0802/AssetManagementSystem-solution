using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class Vendor
{
    public int VndId { get; set; }

    public string VndName { get; set; } = null!;

    public string? VndAddr { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<AssetType> Ats { get; set; } = new List<AssetType>();
}
