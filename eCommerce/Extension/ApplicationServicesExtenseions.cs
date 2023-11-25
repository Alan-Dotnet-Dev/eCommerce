using Core.Interfaces;
using eCommerce.API.Error;
using eCommerce.Core.Interfaces;
using eCommerce.Infrastructure.Repository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eCommerce.API.Extension
{
    public static class ApplicationServicesExtenseions 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(ops =>
            {
                ops.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(s => s.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/myapp-.txt", rollingInterval: RollingInterval.Day) // Log to a text file
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog(); // Integrate Serilog with ASP.NET Core logging
            });
            return services;
        }
    }
}
