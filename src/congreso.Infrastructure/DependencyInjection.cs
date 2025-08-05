using congreso.Application.Interfaces.Persistence;
using congreso.Application.Interfaces.Services;
using congreso.Infrastructure.Persistence.Context;
using congreso.Infrastructure.Persistence.Repositories;
using congreso.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace congreso.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfigurationManager configuration)
        {
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CongressConnection"), x => x.MigrationsAssembly(assembly));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
