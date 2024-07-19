using System.ComponentModel.DataAnnotations;

namespace EShop.Models.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Size { get; set; }
    [Required]
    [Display(Name = "List Price")]
    [Range(1,100000)]
    public decimal ListPrice { get; set; }
}

