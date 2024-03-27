using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using System.Threading.Tasks;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers
{
    [ApiController]
    public class GetUserDataController: ControllerBase
    {
        private ClientService _clientService;
        public GetUserDataController()
        {
            _clientService = new ClientService();
        }
        [HttpPost("getUserData")]
        public async Task<IActionResult> GetUserData(GetUserDataRequest request)
        {

            var orderData = _clientService.GetUserData(request);

            return Ok(new
            {
                ordersData = orderData.ordersData,
                userData = orderData.userData
            });
        }
    }
}