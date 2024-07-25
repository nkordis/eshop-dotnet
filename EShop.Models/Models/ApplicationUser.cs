﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; } = default!;

    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

}

