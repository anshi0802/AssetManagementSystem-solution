using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class AssetDefinition
{
    public int AdId { get; set; }

    public string AdName { get; set; } = null!;

    public int AdTypeId { get; set; }

    public string? AdClass { get; set; }

    public virtual AssetType AdType { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
