using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Domain.Services;
using SistemaControle.Domain.Services.Interfaces;
using SistemaControle.Domain.Shared;
using SistemaControle.Infra.EF.DbContext;
using SistemaControle.Infra.Repository;

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
        services.AddDbContext<SistemaControleContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    #region Repositorios
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<ITransacoesRepository, TransacoesRepository>();
    }
    #endregion

    #region Services
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<ITransacoesService, TransacoesService>();
    }
    #endregion

    private static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CriarPessoaRequestDto>());
    }
}
