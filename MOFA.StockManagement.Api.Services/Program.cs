
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Infrastructure.Business.Mapper;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Domain.Patterns.Repositories;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Repositories.Trackable;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Infrastructure.Business;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Domain.Patterns.Repositories.Trackable;
using MOFA.StockManagement.Domain.Patterns.Repositories;
using MOFA.StockManagement.Infrastructure.Business.Mapper;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using System.Diagnostics.Metrics;
using System.Net.Sockets;

namespace MOFA.SuportCente.Api.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.SuppressModelStateInvalidFilter = true;
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MOFA.StockManagement.Api.Service", Version = "v1" });
#if (!DEBUG)
                        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer",
                            Description = "Input your access_token to access this Api"
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "bearerAuth"
                                    }
                                },
                                new List<string>()
                            }
                        });
#endif
            });
            var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<DBContext>(opts => opts.UseSqlServer(defaultConnectionString));
            builder.Services
            .AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services
            .AddScoped<DbContext, DbContextBase>()
                .AddScoped<DbContextBase, DBContext>()
                .AddScoped<IUnitOfWork<DbContextBase>, UnitOfWork<DbContextBase>>();


            builder.Services
            // Config Area
                .AddScoped<ITrackableRepository<ItemType, DbContextBase>, TrackableRepository<ItemType, DbContextBase>>()
                .AddScoped<ITrackableRepository<Warehouse, DbContextBase>, TrackableRepository<Warehouse, DbContextBase>>()
                ;

            builder.Services
                //Config Area
                .AddScoped<IItemTypeService, ItemTypeService>()
                .AddScoped<IWarehouseService, WarehouseService>()
                ;


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
