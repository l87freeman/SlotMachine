using System.Threading.Tasks;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SlotMachine.Extensions;
using SlotMachine.Requests;

namespace SlotMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IConsumerBalanceService _consumerBalanceService;

    public BalanceController(IConsumerBalanceService consumerBalanceService)
    {
        _consumerBalanceService = consumerBalanceService;
    }

    [HttpPut]
    public async Task<IActionResult> Deposit(DepositBalanceRequest request)
    {
        await _consumerBalanceService.DepositAsync(request.ToModel(), HttpContext.RequestAborted);
        return Ok();
    }
}