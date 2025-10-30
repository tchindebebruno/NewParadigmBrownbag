using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubsManager.Application.Abstractions;
using SubsManager.Application.Mapping;
using SubsManager.Application.Ports;
using SubsManager.Infrastructure.Providers;
using SubsManager.Infrastructure.Repositories;


namespace SubsManager.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var cs = config.GetConnectionString("Postgres") ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRINGS") ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

            services.AddDbContext<SubsDbContext>(opt =>
            {
                opt.UseNpgsql(cs);
            });


            // UoW + Repos
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddSingleton<IDateTimeProvider, SystemClock>();
            services.AddSingleton<IPaymentGateway, FakePaymentGateway>();

            var configMap = TypeAdapterConfig.GlobalSettings;
            new MappingConfig().Register(configMap);
            services.AddSingleton(configMap);
            services.AddScoped<IMapper, Mapper>();
            return services;
        }
    }

}