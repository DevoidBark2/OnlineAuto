using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class CheckUserController:ControllerBase
{
    private ClientService _clientService;

    public CheckUserController()
    {
        _clientService = new ClientService();
    }
    [HttpPost("checkUser")]
    public async Task<IActionResult> CheckUser(CheckUserRequest checkUserRequest)
    {
        var checkUser = _clientService.CheckUser(checkUserRequest);

        return Ok(new
        {
            success = checkUser
        });
    }
}