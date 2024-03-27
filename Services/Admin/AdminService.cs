using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Models.Response;

namespace OnlineAuto.Services.Admin;

public class AdminService
{
    public AllDataResponseResponse getALlData()
    {
        using (var db = new ApplicationContext())
        {
            var selectedUsers =  db.Users
                .Where(user => user.firstName != null && user.secondName != null && user.email != null)
                .Select(user => new UserResponse
                {
                    id = user.Id,
                    firstName = user.firstName,
                    secondName = user.secondName,
                    phone = user.phone,
                    email = user.email,
                    role = user.userRole
                })
                .ToList();

            var orders = db.Orders.ToList();
            
            var response = new AllDataResponseResponse
            {
                orderData = orders,
                userData = selectedUsers
            };

            return response;
        }
    }

    public AllDataResponseResponse GetAllOrders()
    {
        using (var db = new ApplicationContext())
        {
            var orderDetails = db.Orders.ToList();

            var selectedUsers = db.Users.Where(user => user.userRole == "logist" || user.userRole == "carrier")
                .Select(user => new UserResponse
                {
                    id = user.Id,
                    firstName = user.firstName,
                    secondName = user.secondName,
                    email = user.email,
                    role = user.userRole
                })
                .ToList();

            var response = new AllDataResponseResponse
            {
                orderData = orderDetails,
                userData = selectedUsers
            };
            
            return response;
        }
    }

    public bool DeleteUser(DeleteUserRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == request.userId);

            if (user != null)
            {
                if (user.userRole == "logist")
                {
                    var userOrders = db.Orders.Where(order => order.userId == request.userId);
                    db.Orders.RemoveRange(userOrders);
                }

                if (user.userRole == "carrier")
                {
                    var userOrders = db.Orders.Where(order => order.userId == request.userId);
                    db.Orders.RemoveRange(userOrders);
                }
                
                db.Users.Remove(user);
                db.SaveChanges();

                return true;
            }

            return false;
        }
    }

    public bool ChangeOrder(ChangeOrderAdminRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == request.orderId);

            if (order != null)
            {
                order.customerId = request.carrierId;
                db.Orders.Update(order);
                db.SaveChanges();

                return true;
            }

            return false;
        }
    }

    public bool AddNewOrder(AddNewOrderRequestAdmin orderRequest)
    {
        using (var db = new ApplicationContext())
        {
            var order = new Order
            {
                From = orderRequest.pointOne,
                To = orderRequest.pointTwo,
                Price = orderRequest.price,
                Date = orderRequest.date,
                Comment = orderRequest.comment,
                userId = orderRequest.customerId,
                customerId = orderRequest.carrierId
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return true;
        }
    }
    
    public GetOrderDetailsResponse GetOrderDetails(int orderId)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                var customerOrderDetails = db.Users.FirstOrDefault(u => u.Id == order.userId);
                
                if (customerOrderDetails != null)
                {
                    var carrierUsersInfo = new List<UserInfoResponse>();
                    
                    var carrierUsers = db.Users.Where(user => user.userRole == "carrier").ToList();
                    if (carrierUsers.Count > 0)
                    {
                       
                        carrierUsersInfo = carrierUsers.Select(user => new UserInfoResponse
                        {
                            Id = user.Id,
                            firstName = user.firstName,
                            secondName = user.secondName,
                            role = user.userRole
                        }).ToList();
                    }
                    
                    var customerDetails = new UserInfoResponse
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
                        var carrierDetailsList = new List<UserInfoResponse>();

                        foreach (var carrierOrderDetail in carrierOrderDetails)
                        {
                            var carrierDetails = new UserInfoResponse
                            {
                                customerId = carrierOrderDetail.Id,
                                firstName = carrierOrderDetail.firstName,
                                secondName = carrierOrderDetail.secondName,
                                role = carrierOrderDetail.userRole,
                                phone = carrierOrderDetail.phone
                            };

                            carrierDetailsList.Add(carrierDetails);
                        }

                        return new GetOrderDetailsResponse
                        {
                            success = true,
                            order = order,
                            customerOrderDetails = customerDetails,
                            carrierOrderDetails = carrierDetailsList
                        };
                    }
                    else
                    {
                        return new GetOrderDetailsResponse
                        {
                            success = true,
                            order = order,
                            customerOrderDetails = customerDetails,
                            carrierUsers = carrierUsersInfo
                        };
                    }
                }
            }

            return new GetOrderDetailsResponse
            {
                success = true
            };
        }
    }

    public GetUserDetailsResponse GetUserDetails(int userId)
    {
        using (var db = new ApplicationContext())
        {
            var userDetails = db.Users.FirstOrDefault(user => user.Id == userId);
            
            if (userDetails != null)
            {
                if (userDetails.userRole == "logist")
                {
                   
                    var ordersLogist = db.Orders.Where(order => order.userId == userDetails.Id).ToList();
                    var responseLogist = new GetUserDetailsResponse
                    {
                        success = true,
                        user = new UserResponse
                        {
                            firstName = userDetails.firstName,
                            secondName = userDetails.secondName,
                            phone = userDetails.phone,
                            role = userDetails.userRole
                        },
                        orders = ordersLogist
                    };

                    return responseLogist;
                }

                if (userDetails.userRole == "carrier")
                {
                    var ordersCarrier = db.Orders.Where(order => order.customerId == userDetails.Id).ToList();
                    var responseCarrier = new GetUserDetailsResponse
                    {
                        success = true,
                        orders = ordersCarrier,
                        user = new UserResponse
                        {
                            firstName = userDetails.firstName,
                            secondName = userDetails.secondName,
                            phone = userDetails.phone,
                            role = userDetails.userRole
                        }
                    };

                    return responseCarrier;
                }

                return new GetUserDetailsResponse
                {
                    success = false
                };
            }

            return new GetUserDetailsResponse
            {
                success = false
            };
        }
    }
}