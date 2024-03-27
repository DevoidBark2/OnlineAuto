using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers.Client;

[ApiController]
public class SaveUserDataController: ControllerBase
{
    private ClientService _clientService;

    public SaveUserDataController()
    {
        _clientService = new ClientService();
    }
    [HttpPost("saveUserData")]
    public async Task<IResult> SaveUserData(SaveUserDataRequest request)
    {
        var user = _clientService.SaveUserData(request);
        return Results.Ok(new
        {
            userData = user
        });
    }
}