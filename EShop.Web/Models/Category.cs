using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    [DisplayName("Category Name")]
    public string Name { get; set; } = null!;
    [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
    [DisplayName("Display Order")]
    public int DisplayOrder { get; set; }
}

