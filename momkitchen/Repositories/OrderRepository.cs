using AutoMapper;
using Microsoft.EntityFrameworkCore;
using momkitchen.Constrants;
using momkitchen.Mapper;
using momkitchen.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace momkitchen.Services
{
    public class OrderRepository : IOrderRepository
    {
        DbSet<Order> _orders;

        private readonly MomkitchenContext _ctx;
        private readonly IMapper _mapper;

        public OrderRepository(MomkitchenContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
            _orders = _ctx.Set<Order>();
        }

        public static DateTime TranferDateTimeByTimeZone(DateTime dateTime, string timezoneArea)
        {

            ReadOnlyCollection<TimeZoneInfo> collection = TimeZoneInfo.GetSystemTimeZones();
            var timeZone = collection.ToList().Where(x => x.DisplayName.ToLower().Contains(timezoneArea)).First();

            var timeZoneLocal = TimeZoneInfo.Local;

            var utcDateTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneLocal, timeZone);

            return utcDateTime;
        }

        public static DateTime GetDateTimeTimeZoneVietNam()
        {

            return TranferDateTimeByTimeZone(DateTime.Now, "hanoi");
        }
        public static DateTime? StringToDateTimeVN(string dateStr)
        {

            var isValid = DateTime.TryParseExact(
                                dateStr,
                                "d'/'M'/'yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out var date
                            );
            return isValid ? date : null;
        }
        public int GetBatchIdByBuildingId(int buildingId)
        {
            var batch = 0;
            if ((buildingId >= 1) && (buildingId <= 3))
            {
                batch = 1;
            };
            if (buildingId > 3 && buildingId <= 6)
            {
                batch = 2;
            }
            if (buildingId > 6 && buildingId <= 9)
            {
                batch = 3;
            }
            if (buildingId > 9 && buildingId <= 12)
            {
                batch = 4;
            }
            if (buildingId > 12 && buildingId <= 15)
            {
                batch = 5;
            }
            return batch;
        }

        public OrderDetail GetOrderDetailByOrderId(int id)
        {
            return _ctx.OrderDetails.Where(x => x.OrderId == id).Include(x => x.Order).ThenInclude(x => x.Payments).Select(x => new OrderDetail()).FirstOrDefault();
        }

        public async Task CreateOrder(OrderDto orderDto)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? transaction = _ctx.Database.BeginTransaction();
            var customerid = _ctx.Customers.Where(x => x.Email == orderDto.Email).AsNoTracking().Select(x => x.Id).FirstOrDefault();
            var buildingid = _ctx.Customers.Where(x => x.Email == orderDto.Email).AsNoTracking().Select(x => x.DefaultBuilding).FirstOrDefault();
            var customerphone = _ctx.Customers.Where(x => x.Email == orderDto.Email).AsNoTracking().Select(x => x.Phone).FirstOrDefault();
            var email = orderDto.Email;
            var buildingId = orderDto.BuildingId;
            var batchid = GetBatchIdByBuildingId((int)buildingId);
            var now = GetDateTimeTimeZoneVietNam();

            var newOrder = new Order()
            {
                Email = email,
                Date = now,
                DeliveryTime = now.AddMinutes(15),
                CustomerId = customerid,
                BuildingId = orderDto.BuildingId,
                CustomerPhone = customerphone,
                Status = OrderConstrants.NEWSTATUS,
                DeliveryStatus = OrderConstrants.NEWDELIVERYSTATUS,
                Quantity = orderDto.Quantity,
                TotalPrice = orderDto.TotalPrice,
                BatchId = batchid,
                SessionId = orderDto.SessionId,
                Note = orderDto.Note,
            };

            foreach (var paymentDto in orderDto.Payments)
            {
                var payment = new Payment()
                {
                    Order = newOrder,
                    Type = PaymentConstrants.TYPE,
                    Status = PaymentConstrants.PAID,
                    Amount = paymentDto.Amount,
                };
                await _ctx.Payments.AddAsync(payment);
            }

            var quantity = 0;
            var total = 0;
            foreach (var orderDetailDto in orderDto.OrderDetails)
            {
                quantity += orderDetailDto.Quantity;
                total += (int)orderDetailDto.Price;
                var orderDetail = new OrderDetail()
                {
                    Order = newOrder,
                    SessionPackageId = orderDetailDto.SessionPackageId,
                    Price = orderDetailDto.Price,
                    Quantity = orderDetailDto.Quantity,
                    Status = OrderdetailConstrants.ACTIVE,
                };

                await _ctx.OrderDetails.AddAsync(orderDetail);

                // Minus Remainquantity
                var sessionPackageIds = orderDto.OrderDetails.Select(od => od.SessionPackageId).ToList();
                var sessionPackages = await _ctx.SessionPackages.Where(sp => sessionPackageIds.Contains(sp.Id)).ToListAsync();
                foreach (var sessionPackage in sessionPackages)
                {
                    var orderDetailQuantity = orderDto.OrderDetails.FirstOrDefault(od => od.SessionPackageId == sessionPackage.Id)?.Quantity ?? 0;
                    sessionPackage.RemainQuantity -= orderDetailQuantity;
                    _ctx.SessionPackages.Update(sessionPackage);
                }

               
            }
            newOrder.Quantity = quantity;
            newOrder.TotalPrice = total;
            var createdOrder = await _ctx.Orders.AddAsync(newOrder);
            await _ctx.SaveChangesAsync();
            transaction.Commit();
        }



        public async Task<Order> GetOrderByID(int id)
        {
            return await _ctx.Orders.Where(x => x.Id == id).Include(x => x.OrderDetails).ThenInclude(x => x.SessionPackage)
                .ThenInclude(x => x.FoodPackage).ThenInclude(x => x.Chef).AsNoTracking().FirstOrDefaultAsync();

        }

        //public async Task<Order> GetOrderDetailByOrderId(int id)
        //{
        //    var result = await _ctx.Orders.FindAsync(id);
        //    var orderid = await _c
        //    if (result != null)
        //    {
        //        re
        //    }
        //}

        public Order Test(int id)
        {
            return (Order)_orders.Where(x => x.Id == id).Select(x => new Order()
            {
                Id = x.Id,
                Date = x.Date,
                CustomerId = x.CustomerId,
                BatchId = x.BatchId,
                Status = x.Status,
                DeliveryStatus = x.DeliveryStatus,
                BuildingId = x.BuildingId,
                Quantity = x.Quantity,
                SessionId = x.SessionId,
                Email = x.Email,
                CustomerPhone = x.CustomerPhone,
                DeliveryTime = x.DeliveryTime,
                Note = x.Note,
                Session = new Session()
                {
                    Id = x.Session.Id,
                    CreateDate = x.Session.CreateDate,
                    StartTime = x.Session.StartTime,
                    EndTime = x.Session.EndTime,
                    Status = x.Session.Status,
                    Title = x.Session.Title,
                }
            }).FirstOrDefault();
        }

        public List<Order> GetOrderDetailByOrderId()
        {
            var result = _ctx.Orders.Select(x => new Order
            {
                Id=x.Id,
                Date=x.Date,
                Customer = x.Customer,
                Batch = x.Batch,
                Status = x.Status,
                DeliveryStatus=x.DeliveryStatus,
                Building = x.Building,
                Session = x.Session,
                Email = x.Email,
                CustomerPhone=x.CustomerPhone,
                DeliveryTime=x.DeliveryTime,
                Note=x.Note,
            }).ToList();

            return result;
        }

        public Payment GetPaymentByOrderId(int orderid)
        {
            var result = _ctx.Payments.Where(x => x.OrderId == orderid).Select(x => new Payment
            {
                Type=x.Type,
                OrderId=x.OrderId,
                Status=x.Status,
                Amount=x.Amount,
            }).FirstOrDefault();

            return result;
        }

        public List<Order> GetAllOrder()
        {
            var result = _ctx.Orders.ToList();
            return result;
        }

        public int CountOrder()
        {
            var result = _ctx.Orders.Count();
            return result;
        }

        public int CountTotalPrice()
        {
            var result = _ctx.Orders.Sum(o => o.TotalPrice);
            return (int)result;
        }

        public List<Order> GetOrderByEmailCustomer(string emailcustomer)
        {
            var result = _ctx.Orders
        .Where(x => x.Email == emailcustomer)
        .Include(x => x.Customer)
        .Include(x => x.OrderDetails)
            .ThenInclude(x => x.SessionPackage)
                .ThenInclude(x => x.FoodPackage)
        .ToList();

            return result;
        }

        public List<SessionPackage> GetSessionPackageIdbyOrderId(int orderid)
        {
            var result = _ctx.OrderDetails.Where(x => x.OrderId == orderid).Select(x => new SessionPackage
            {
                Id = x.OrderId,
                FoodPackage = x.SessionPackage.FoodPackage,
                SessionId = x.SessionPackage.SessionId,
                Price = x.SessionPackage.Price,
                Quantity = x.SessionPackage.Quantity,
                RemainQuantity = x.SessionPackage.RemainQuantity,
                Status = x.SessionPackage.Status,
                CreateDate = x.SessionPackage.CreateDate,

            }).ToList();

            return result;
        }

    }
}
