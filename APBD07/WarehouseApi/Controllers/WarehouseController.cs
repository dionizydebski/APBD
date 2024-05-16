using Microsoft.AspNetCore.Mvc;
using WarehouseApi.DTO;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public IActionResult AddProductToWarehouse(Product_WarehouseDTO productWarehouseDTO)
    {
        var result = _warehouseService.AddProductToWarehouse(productWarehouseDTO).Result;
        if (result != 0)
        {
            Console.WriteLine(result);
            return StatusCode(StatusCodes.Status201Created);
        }
        return StatusCode(StatusCodes.Status400BadRequest);
    }
}