
using Inventory.Models;
using Inventory.Models.IRepositories;
using Inventory.Models.Reopositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Inventory.Helpers;

namespace Inventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<InventoryManagmentSystemContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });


            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });



            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<InventoryManagmentSystemContext>();

            builder.Services.AddControllers();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IinventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<IStockLevelRepository, StockLevelRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddSingleton<GenerateJwtTokenHelper>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
