﻿using EShop.Models.Models;

namespace EShop.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    public OrderHeader OrderHeader { get; set; }
}
