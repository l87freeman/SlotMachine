using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Models;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly IMongoCollection<ConsumerBalanceModel> _balancesCollection;

    public BalanceRepository(IMongoCollection<ConsumerBalanceModel> balancesCollection)
    {
        _balancesCollection = balancesCollection;
    }

    public async Task<ConsumerBalance> GetAsync(string consumerId, CancellationToken cancellationToken)
    {
        var result = await _balancesCollection
            .Find(c => c.Id == consumerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            throw new EntityNotFoundException(nameof(ConsumerBalanceModel), consumerId);
        }

        return new ConsumerBalance(consumerId, result.Balance, result.Version);
    }

    public async Task<ConsumerBalance> UpdateAsync(ConsumerBalance balance, CancellationToken cancellationToken)
    {
        var filter = Builders<ConsumerBalanceModel>.Filter.And(
            Builders<ConsumerBalanceModel>.Filter.Eq(x => x.Id, balance.ConsumerId),
            Builders<ConsumerBalanceModel>.Filter.Eq(x => x.Version, balance.Version));
        
        var update = new ConsumerBalanceModel
        {
            Balance = balance.Balance,
            Version = balance.Version + 1,
            Id = balance.ConsumerId,
        };

        var options = new FindOneAndReplaceOptions<ConsumerBalanceModel>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        try
        {
            var result = await _balancesCollection.FindOneAndReplaceAsync(filter, update, options, cancellationToken);
            return new ConsumerBalance(result.Id, result.Balance, balance.Version);
        }
        catch (MongoCommandException e) when (e.Code == 11000)
        {
            throw new UpdateConflictException(nameof(ConsumerBalanceModel));
        }
    }
}