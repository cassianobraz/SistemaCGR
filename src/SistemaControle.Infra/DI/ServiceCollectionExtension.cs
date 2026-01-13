using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaControle.Application.Pessoa.Dtos.Requests;

namespace SistemaControle.Infra.DI;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContextConfig(services, configuration);
        AddRepositories(services);
        AddServices(services);
        AddHandlers(services);
        return services;
    }
    private static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<TableOrderContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        //services.AddScoped<IAuthRepository, AuthRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        //services.AddScoped<ITokenService, TokenService>();
    }

    private static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CriarPessoaRequestDto>());
    }
}
