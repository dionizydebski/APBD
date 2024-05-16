using System.ComponentModel.DataAnnotations;

namespace WarehouseApi.Models;

public class Product
{
    [Required]
    public int IdProduct { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public double Price { get; set; }
}