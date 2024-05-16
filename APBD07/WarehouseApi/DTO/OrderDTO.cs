namespace WarehouseApi.DTO;

public record OrderDTO(int IdOrder, int IdProduct, int Amount, DateTime CreatedAt, DateTime FullfilledAt);