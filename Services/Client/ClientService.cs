using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Models.Response;

namespace OnlineAuto.Services.Client;

public class ClientService
{
    public UserResponse SaveUserData(SaveUserDataRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == request.id);
            if (user != null)
            {
                user.firstName = request.firstName; 
                user.secondName = request.secondName;
                user.email = request.email;
                user.phone = request.phone;

                db.Users.Update(user);
                db.SaveChangesAsync();

                var responseUser = new UserResponse
                {
                    id = user.Id,
                    firstName = user.firstName,
                    phone = user.phone,
                    secondName = user.secondName,
                    email = user.email,
                    role = user.userRole
                };

                return responseUser;
            }

            return null;
        }
    }

    public List<Order> SortOrder(string sortBy)
    {
        using (var db = new ApplicationContext())
        {
            IQueryable<Order> orders = db.Orders;
            
            switch (sortBy)
            {
                case "price_high":
                    orders = orders.OrderBy(o => o.Price);
                    break;
                case "price_low":
                    orders = orders.OrderByDescending(o => o.Price);
                    break;
                case "date_high":
                    orders = orders.OrderBy(o => o.Date);
                    break;
                case "date_low":
                    orders = orders.OrderByDescending(o => o.Date);
                    break;
            }

            var sortedOrders = orders.ToList();
            return sortedOrders;
        }
    }

    public UserDataResponse GetUserData(GetUserDataRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var orders = new List<Order>();

            if (request.roleUser == "logist")
            {
                orders = db.Orders.Where(order => order.userId == request.UserId).ToList();
            }
            else
            {
                orders = db.Orders.Where(order => order.customerId == request.UserId).ToList();
            }

            var userData = db.Users
                .Where(user => user.Id == request.UserId)
                .Select(user => new UserResponse
                {
                    id = user.Id,
                    firstName = user.firstName,
                    secondName = user.secondName,
                    email = user.email,
                    phone = user.phone,
                    role = user.userRole
                })
                .FirstOrDefault();
                
            var response = new UserDataResponse
            {
                ordersData = orders,
                userData = userData
            };

            return response;
        }
    }

    public bool DeleteOrder(DeleteOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == request.orderId && order.userId == request.userId);

            if (order == null)
            {
                return false;
            }
            db.Orders.Remove(order);
            db.SaveChangesAsync();

            return true;
        }
    }

    public bool AddNewOrder(AddNewOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == request.orderId);

            if (order == null)
            {
                return false;
            }

            order.customerId = request.customerId;
            db.Orders.Update(order);

            db.SaveChanges();

            return true;
        }
    }

    public bool ChangeOrderData(OrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.userId == request.userId);

            if (order != null)
            {
                order.From = request.pointOne;
                order.To = request.pointTwo;
                order.Comment = request.comment;
                order.Price = request.price;
                order.Date = request.date;
                db.SaveChanges();

                return true;
            }
            return false;
        }
    }

    public Order CreateOrder(OrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = new Order
            {
                From = request.pointOne,
                To = request.pointTwo,
                Price = request.price,
                Date = request.date,
                Comment = request.comment,
                userId = request.userId,
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return order;
        }
    }

    public List<Order> GetAllOrder()
    {
        using (var db = new ApplicationContext())
        {
            var orders = db.Orders.ToList();

            return orders;
        }
    }

    public bool DeleteCustomerOrder(DeleteCustomerOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => 
                order.Id == request.orderId && 
                order.customerId == request.carrierId
            );

            if (order != null)
            {
                order.customerId = 0;
                db.SaveChanges();

                return true;
            }
            
            return false;
        }
    }

    public bool CheckUser(CheckUserRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == request.userId);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }

    public GetOrderDataResponse GetOrderData(int orderId)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return new GetOrderDataResponse
                {
                    success = false,
                };
            }

            var user = db.Users.FirstOrDefault(user => user.Id == order.userId);
            if (user == null)
            {
                return new GetOrderDataResponse
                {
                    success = false,
                };
            }
            var userObject = new UserResponse()
            {
                id = user.Id,
                firstName = user.firstName,
                secondName = user.secondName,
                phone = user.phone
            };
           
            if (order.customerId != 0)
            {
                var carrierUser = db.Users.FirstOrDefault(userCarrier => userCarrier.Id == order.customerId);
                
                var carrierObject = new UserResponse
                {
                    id = carrierUser.Id,
                    firstName = carrierUser.firstName,
                    secondName = carrierUser.secondName,
                    phone = carrierUser.phone
                };
                return new GetOrderDataResponse
                {
                    success = true,
                    order = order,
                    user = userObject,
                    carrierUser = carrierObject
                };
            }
            return new GetOrderDataResponse
            {
                success = true,
                order = order,
                user = userObject
            };
        }
    }
}