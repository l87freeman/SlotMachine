using Application.Services;
using Application.Services.Abstractions;
using Domain.Repositories;
using Domain.UseCases;
using Domain.UseCases.Abstractions;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Application.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddMongoDbCollections(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

        services.AddSingleton(s => s.GetRequiredService<IMongoClient>().GetDatabase(databaseName));


        services.AddSingleton(s =>
            s.GetRequiredService<IMongoDatabase>()
                .GetCollection<ConsumerBalanceModel>(configuration["MongoDb:BalanceCollectionName"]));

        services.AddSingleton(s =>
            s.GetRequiredService<IMongoDatabase>()
                .GetCollection<GridConfigurationModel>(configuration["MongoDb:GridConfigurationCollectionName"]));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IBalanceRepository, BalanceRepository>();
        services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IDepositToBalanceUseCase, DepositToBalanceUseCase>();
        services.AddScoped<IGridFillInUseCase, GridFillInUseCase>();
        services.AddScoped<ISpinUseCase, SpinUseCase>();
        services.AddScoped<IModifyGridConfigUseCase, ModifyGridConfigUseCase>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IConsumerBalanceService, ConsumerBalanceService>();
        services.AddScoped<ISpinService, SpinService>();
        services.AddScoped<IAdminService, AdminService>();

        return services;
    }
}