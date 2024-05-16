using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.DTO;
using WarehouseApi.Services;

namespace WarehouseApi.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<int> AddProductToWarehouse(Product_WarehouseDTO productWarehouseDTO)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        
        //1) Czy ilosc jest > 0
        if (productWarehouseDTO.Amount < 1)
        {
            Console.WriteLine("Za mala ilosc");
            return 0;
        }
        
        //1) Czy istnieje taki produkt
        await using var cmd_check_product = new SqlCommand();
        cmd_check_product.Connection = con;
        cmd_check_product.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd_check_product.Parameters.AddWithValue("@IdProduct", productWarehouseDTO.IdProduct);
        
        var dr_check_product = cmd_check_product.ExecuteReader();
        double Price;
        if (!dr_check_product.Read())
        {
            Console.WriteLine("Nie ma takiego produktu");
            return 0;
        }
        Price = Convert.ToDouble(dr_check_product["Price"]);
        dr_check_product.Close();
        
        //1) Czy istnieje taki magazyn
        await using var cmd_check_warehouse = new SqlCommand();
        cmd_check_warehouse.Connection = con;
        cmd_check_warehouse.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd_check_warehouse.Parameters.AddWithValue("@IdWarehouse", productWarehouseDTO.IdWarehouse);
        
        var dr_check_warehouse = cmd_check_warehouse.ExecuteReader();
        if (!dr_check_warehouse.Read())
        {
            Console.WriteLine("Nie ma takiego magazynu");
            return 0;
        }
        dr_check_warehouse.Close();
        
        
        //2) Czy istnieje takie zamowienie
        await using var cmd_check_order = new SqlCommand();
        cmd_check_order.Connection = con;
        cmd_check_order.CommandText = "SELECT * FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt";
        cmd_check_order.Parameters.AddWithValue("@IdProduct", productWarehouseDTO.IdProduct);
        cmd_check_order.Parameters.AddWithValue("@Amount", productWarehouseDTO.Amount);
        cmd_check_order.Parameters.AddWithValue("@CreatedAt", productWarehouseDTO.CreatedAt);
        
        var dr_check_order = cmd_check_order.ExecuteReader();
        int IdOrder;
        if (!dr_check_order.Read())
        {
            Console.WriteLine("Nie ma takiego zamowienia");
            return 0;
        }
        IdOrder = (int)dr_check_order["IdOrder"];
        dr_check_order.Close();
        
        //3) Czy nie zostało juz zrealizowane
        await using var cmd_check_product_warehouse = new SqlCommand();
        cmd_check_product_warehouse.Connection = con;
        cmd_check_product_warehouse.CommandText = "SELECT * FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        cmd_check_product_warehouse.Parameters.AddWithValue("@IdOrder", IdOrder);
        
        var dr_check_product_warehouse = cmd_check_product_warehouse.ExecuteReader();
        if (dr_check_product_warehouse.Read())
        {
            dr_check_product_warehouse.Close();
            Console.WriteLine("Takie zamowienie zostalo juz zrealizowane");
            return 0;
        }
        dr_check_product_warehouse.Close();
        
        //4) Aktualizujemy FullfilledAt
        await using var cmd_change_date = new SqlCommand();
        cmd_change_date.Connection = con;
        cmd_change_date.CommandText = "UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder";
        cmd_change_date.Parameters.AddWithValue("@IdOrder", IdOrder);
        cmd_change_date.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        
        //5) Dodajemy do Product_Warehouse
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAT) VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY()";
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouseDTO.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouseDTO.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
        cmd.Parameters.AddWithValue("@Amount", productWarehouseDTO.Amount);
        cmd.Parameters.AddWithValue("@Price", Price * productWarehouseDTO.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

        var result = cmd.ExecuteScalar();
        return Convert.ToInt32(result);
    }
}