using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string UserType { get; set; } = null!;

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}


