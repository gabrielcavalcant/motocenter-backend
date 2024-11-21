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
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Persistence.UnitOfWork;

namespace OficinaMotocenter.CrossCutting.DependencyInjection
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            // Contexto do EF Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositórios
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();


            // Serviços
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();

            // AutoMapper
            services.AddAutoMapper(typeof(MotorcycleMappingProfile));
            services.AddAutoMapper(typeof(CustomerMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));
            services.AddAutoMapper(typeof(RoleMappingProfile));
            services.AddAutoMapper(typeof(PermissionMappingProfile));
            services.AddAutoMapper(typeof(TeamMappingProfile));
            services.AddAutoMapper(typeof(TeamMemberMappingProfile));



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
