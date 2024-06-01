using Microsoft.EntityFrameworkCore;
using PharmacyApi.Context;
using PharmacyApi.Repositories;
using PharmacyApi.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        builder.Services.AddScoped<IPharmacyService, PharmacyService>();
        
        builder.Services.AddDbContext<ApbdContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}