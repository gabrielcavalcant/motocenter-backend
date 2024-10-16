using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OficinaMotocenter.Application.AutoMapping;
using OficinaMotocenter.Application.Services;
using OficinaMotocenter.Application.Validation.Customer;
using OficinaMotocenter.Application.Validation.Motorcycle;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Persistence.Context;
using OficinaMotocenter.Persistence.Repositories;

namespace OficinaMotocenter.CrossCutting.DependencyInjection
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            // Contexto do EF Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositórios
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Serviços
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<ICustomerService, CustomerService>();


            // AutoMapper
            services.AddAutoMapper(typeof(MotorcycleMappingProfile));
            services.AddAutoMapper(typeof(CustomerMappingProfile));


            // FluentValidation: Registrar validadores
            services.AddValidatorsFromAssemblyContaining<CreateMotorcycleRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateMotorcycleRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();



            // Configurar FluentValidation na pipeline MVC
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

        }
    }
}
