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
        public int GetBatchIdByBuildingId(string email)
        {
            var batch = 0;
            var buildingid = _ctx.Customers.Where(x => x.Email == email).AsNoTracking().Select(x => x.DefaultBuilding).FirstOrDefault();
            if ((buildingid >= 1) && (buildingid <= 3))
            {
                batch = 1;
            };
            if (buildingid > 3 && buildingid <= 6)
            {
                batch = 2;
            }
            if (buildingid > 6 && buildingid <= 9)
            {
                batch = 3;
            }
            if (buildingid > 9 && buildingid <= 12)
            {
                batch = 4;
            }
            if (buildingid > 12 && buildingid <= 15)
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
            var batchid = GetBatchIdByBuildingId(email);
            var now = GetDateTimeTimeZoneVietNam();

            var newOrder = new Order()
            {
                Email = email,
                Date = now,
                DeliveryTime = now.AddMinutes(15),
                CustomerId = customerid,
                BuildingId = buildingid,
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
    }
}
