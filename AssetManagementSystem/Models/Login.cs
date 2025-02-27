﻿using System;
using System.Collections.Generic;

namespace AssetManagementSystem.Models;

public partial class Login
{
    public int LId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role? Role { get; set; } = null!;

    public virtual User? User { get; set; }
}
