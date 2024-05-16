using System.ComponentModel.DataAnnotations;

namespace WarehouseApi.Models;

public class Product_Warehouse
{
    [Required]
    public int IdProductWarehouse { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdOrder { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
}