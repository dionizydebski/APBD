using WarehouseApi.DTO;

namespace WarehouseApi.Repositories;

public interface IWarehouseRepository
{
    Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDTO);
}