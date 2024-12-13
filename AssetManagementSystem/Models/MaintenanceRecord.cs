using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class MaintenanceRecord
{
    public int MrId { get; set; }

    public int AssetId { get; set; }

    public DateTime MaintenanceDate { get; set; }

    public string? Description { get; set; }

    public decimal? Cost { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
