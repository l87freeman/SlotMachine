using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Application.Services.Abstractions;

public interface IAdminService
{
    Task SetupGridConfigurationAsync(GridConfigurationModel model, CancellationToken cancellationToken);
}