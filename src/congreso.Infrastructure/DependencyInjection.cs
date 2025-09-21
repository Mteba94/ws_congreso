using congreso.Application.Interfaces.Authentication;
using congreso.Application.Interfaces.ExternalWS;
using congreso.Application.Interfaces.Persistence;
using congreso.Application.Interfaces.Services;
using congreso.Infrastructure.Authentication;
using congreso.Infrastructure.ExternalServices.Service;
using congreso.Infrastructure.Persistence.Context;
using congreso.Infrastructure.Persistence.Repositories;
using congreso.Infrastructure.Services;
using logging.Interface;
using logging.Model;
using logging.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TallerIdentity.Infrastructure.Authentication;

namespace congreso.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CongressConnection"), x => x.MigrationsAssembly(assembly));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));

        var infraAsm = Assembly.GetExecutingAssembly();
        foreach (var impl in infraAsm.GetTypes()
                     .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")))
        {
            var @interface = impl.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + impl.Name);
            if (@interface != null)
                services.AddScoped(@interface, impl);
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();

        services.AddScoped<ISendEmailAPI, SendEmailAPI>();

        services.AddScoped<IFileLogger, FileLogger>();
        services.Configure<LogsSettings>(configuration.GetSection("Logs"));

        services.ConfigureHttpClientDefaults(builder =>
        {
            builder.ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:EmailService"]!);
            });
        });

        services.AddTransient<IExcelService, ExcelService>();
        services.AddTransient<IPdfService, PdfService>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
