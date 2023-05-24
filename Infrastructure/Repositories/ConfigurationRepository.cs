using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Models;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

//TODO: add implementation which will store value in redis and implement IOptionsMonitor<T> with subscription to redis change events in order to invoke OnChange. For simplicity use mongodb. Also here is an assumption that grid might be different for different spins
public class ConfigurationRepository : IConfigurationRepository
{
    private readonly IMongoCollection<GridConfigurationModel> _gridConfigurationModel;

    public ConfigurationRepository(IMongoCollection<GridConfigurationModel> gridConfigurationModel)
    {
        _gridConfigurationModel = gridConfigurationModel;
    }

    public async Task<GridConfiguration> GetAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _gridConfigurationModel
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            throw new EntityNotFoundException(nameof(ConsumerBalanceModel), id);
        }

        return new GridConfiguration
        {
            Id = result.Id,
            Width = result.Width,
            Height = result.Height
        };
    }

    public async Task<GridConfiguration> UpdateAsync(GridConfiguration model, CancellationToken cancellationToken)
    {
        var filter = Builders<GridConfigurationModel>.Filter.Eq(x => x.Id, model.Id);

        var update = new GridConfigurationModel
        {
            Height = model.Height,
            Width = model.Width,
            Id = model.Id,
        };

        var options = new FindOneAndReplaceOptions<GridConfigurationModel>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var result = await _gridConfigurationModel.FindOneAndReplaceAsync(filter, update, options, cancellationToken);
        return new GridConfiguration
        {
            Width = result.Width,
            Height = result.Height,
            Id = result.Id
        };
    }
}