﻿
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    [ValidateNever]
    public Product Product { get; set; }
    public string ApplicationUserId { get; set; }
    [ForeignKey(nameof(ApplicationUserId))]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }
}
