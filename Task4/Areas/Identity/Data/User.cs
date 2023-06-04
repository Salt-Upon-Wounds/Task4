﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task4.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string Name { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime lastLoggedAt { get; set; }
    public bool Status { get; set; }
}
