using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Response;

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
                    var carrierUsersInfo = Array.Empty<UserInfoResponse>();
                    
                    var carrierUsers = db.Users.Where(user => user.userRole == "carrier").ToList();
                    if (carrierUsers.Count > 0)
                    {
                       
                        carrierUsersInfo = carrierUsers.Select(user => new UserInfoResponse
                        {
                            Id = user.Id,
                            firstName = user.firstName,
                            secondName = user.secondName,
                            role = user.userRole
                        }).ToArray();
                    }
                    
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
                            customerOrderDetails = customerDetails,
                            carrierUsers = carrierUsersInfo
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