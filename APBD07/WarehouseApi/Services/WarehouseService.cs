using WarehouseApi.DTO;
using WarehouseApi.Repositories;

namespace WarehouseApi.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDTO)
    {
        return _warehouseRepository.AddProductToWarehouse(productWarehouseDTO);
    }
}