using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class User
{
    public int UId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? LId { get; set; }

    public virtual Login? LIdNavigation { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
