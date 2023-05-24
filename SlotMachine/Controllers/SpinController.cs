using System.Threading.Tasks;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SlotMachine.Extensions;
using SlotMachine.Requests;

namespace SlotMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class SpinController : ControllerBase
{
    private readonly ISpinService _spinService;

    public SpinController(ISpinService spinService)
    {
        _spinService = spinService;
    }

    [HttpPost]
    public async Task<IActionResult> Spin(SpinRequest request)
    {
        var result = await _spinService.MakeSpinAsync(request.ToModel(), HttpContext.RequestAborted);
        return Created("", result.ToResponse());
    }
}