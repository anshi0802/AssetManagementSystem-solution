using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class AssetType
{
    public int AtId { get; set; }

    public string AtName { get; set; } = null!;

    public virtual ICollection<AssetDefinition> AssetDefinitions { get; set; } = new List<AssetDefinition>();

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<Vendor> Vnds { get; set; } = new List<Vendor>();
}
