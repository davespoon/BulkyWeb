using System.ComponentModel.DataAnnotations;

namespace Bulky.Models;

public class Product
{
    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    public string Description { get; set; }
    [Required] public string ISBN { get; set; }
    [Required] public string Author { get; set; }

    [Display(Name = "List Price 1-49")]
    [Required]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Display(Name = "List Price 50-99")]
    [Required]
    [Range(1, 1000)]
    public double ListPrice50 { get; set; }

    [Display(Name = "List Price 100+")]
    [Required]
    [Range(1, 1000)]
    public double ListPrice100 { get; set; }
}