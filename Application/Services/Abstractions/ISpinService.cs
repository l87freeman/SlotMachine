using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Application.Services.Abstractions;

public interface ISpinService
{
    Task<SpinResultModel> MakeSpinAsync(SpinAttemptModel request, CancellationToken cancellationToken);
}