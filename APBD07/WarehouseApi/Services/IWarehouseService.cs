using WarehouseApi.DTO;

namespace WarehouseApi.Services;

public interface IWarehouseService
{
    Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDTO);
}