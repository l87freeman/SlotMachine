using System.Threading.Tasks;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SlotMachine.Extensions;
using SlotMachine.Requests;

namespace SlotMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPut("grid-config")]
    public async Task<IActionResult> Spin(GridConfigurationRequest request)
    {
        await _adminService.SetupGridConfigurationAsync(request.ToModel(), HttpContext.RequestAborted);
        return Ok();
    }
}