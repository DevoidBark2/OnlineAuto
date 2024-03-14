using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetOrderDetailsController : ControllerBase
{
    [HttpGet("getOrderDetails")]
    public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                var customerOrderDetails = db.Users.FirstOrDefault(u => u.Id == order.userId);
                
                if (customerOrderDetails != null)
                {
                    var customerDetails = new
                    {
                        customerId = customerOrderDetails.Id,
                        firstName = customerOrderDetails.firstName,
                        secondName = customerOrderDetails.secondName,
                        role = customerOrderDetails.userRole,
                        phone = customerOrderDetails.phone
                    };

                    var carrierOrderDetails = db.Users.Where(u => u.Id == order.customerId).ToList();

                    if (carrierOrderDetails.Count > 0)
                    {
                        var carrierDetailsList = new List<object>();

                        foreach (var carrierOrderDetail in carrierOrderDetails)
                        {
                            var carrierDetails = new
                            {
                                customerId = carrierOrderDetail.Id,
                                firstName = carrierOrderDetail.firstName,
                                secondName = carrierOrderDetail.secondName,
                                role = carrierOrderDetail.userRole,
                                phone = carrierOrderDetail.phone
                            };

                            carrierDetailsList.Add(carrierDetails);
                        }

                        return Ok(new
                        {
                            success = true,
                            order = order,
                            customerOrderDetails = customerDetails,
                            carrierOrderDetails = carrierDetailsList
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            success = true,
                            order = order,
                            customerOrderDetails = customerDetails
                        });
                    }
                }
            }

            return Ok(new
            {
                success = false
            });
        }
    }
}