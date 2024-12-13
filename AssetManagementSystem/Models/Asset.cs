using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class Asset
{
    public int AmId { get; set; }

    public int AssetNumber { get; set; }

    public int AmAtypeId { get; set; }

    public int AmMakeId { get; set; }

    public int AmAdId { get; set; }

    public string AmModel { get; set; } = null!;

    public string AmSnumber { get; set; } = null!;

    public string AmMyyear { get; set; } = null!;

    public DateTime AmPdate { get; set; }

    public string AmWarranty { get; set; } = null!;

    public string? AmStatus { get; set; }

    public virtual AssetDefinition? AmAd { get; set; } = null!;

    public virtual AssetType? AmAtype { get; set; } = null!;

    public virtual Vendor? AmMake { get; set; } = null!;

    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
}
