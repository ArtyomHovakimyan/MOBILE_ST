//using Mic.Volo.MOBILE_ST.Data.AppDbCont;
//using Mic.Volo.MOBILE_ST.Data.Models;
//using Mic.Volo.MOBILE_ST.Data.Services;
//using Mic.Volo.MOBILE_ST.Data.Smod;
//using Mic.Volo.MOBILE_ST.Data.ViewModel;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Mic.Volo.MOBILE_ST.Data
//{
//    public class OrderService : IOrderService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IShoppingCartService _shoppingCartService;

//        public OrderService(
//            ApplicationDbContext context,
//            IShoppingCartService shoppingCartService)
//        {
//            _context = context;
//            _shoppingCartService = shoppingCartService;
//        }
//        public async Task CreateOrderAsync(Order order)
//        {
//            order.OrderPlacedTime = DateTime.Now;
//            await _context.Orders.AddAsync(order);

//            var shoppingCartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
//            order.OrderTotal = (await _shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;
//            await _context.OrderDetails.AddRangeAsync(shoppingCartItems.Select(e => new OrderDetail
//            {
//                Qty = e.Qty,
//                PhoneName = e.Phone.Name,
//                OrderId = order.Id,
//                Price = e.Phone.Price
//            }));
//            await _context.SaveChangesAsync();
//        }

//        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync()
//        {
//            return await _context.Orders
//                .Include(e => e.OrderDetails)
//                .Select(e => new MyOrderViewModel
//                {
//                    OrderPlacedTime = e.OrderPlacedTime,
//                    OrderTotal = e.OrderTotal,
//                    OrderPlaceDetails = new OrderSMod
//                    {
//                        AddressLine1 = e.AddressLine1,
//                        AddressLine2 = e.AddressLine2,
//                        City = e.City,
//                        Country = e.Country,
//                        Email = e.Email,
//                        FirstName = e.FirstName,
//                        LastName = e.LastName,
//                        PhoneNumber = e.PhoneNumber,
//                        State = e.State,
//                        ZipCode = e.ZipCode
//                    },
//                    PhoneOrderInfos = e.OrderDetails.Select(o => new MyPhoneOrderInfo
//                    {
//                        Name = o.PhoneName,
//                        Price = o.Price,
//                        Qty = o.Qty
//                    })
//                }).ToListAsync();
//        }

//        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId)
//        {
//            return await _context.Orders
//                .Where(e => e.UserId == userId)
//                .Include(e => e.OrderDetails)
//                .Select(e => new MyOrderViewModel
//                {
//                    OrderPlacedTime = e.OrderPlacedTime,
//                    OrderTotal = e.OrderTotal,
//                    OrderPlaceDetails = new OrderSMod
//                    {
//                        AddressLine1 = e.AddressLine1,
//                        AddressLine2 = e.AddressLine2,
//                        City = e.City,
//                        Country = e.Country,
//                        Email = e.Email,
//                        FirstName = e.FirstName,
//                        LastName = e.LastName,
//                        PhoneNumber = e.PhoneNumber,
//                        State = e.State,
//                        ZipCode = e.ZipCode
//                    },
//                    PhoneOrderInfos = e.OrderDetails.Select(o => new MyPhoneOrderInfo
//                    {
//                        Price = o.Price,
//                        Qty = o.Qty,
//                        Name = o.PhoneName
//                    })
//                }).ToListAsync();
//        }
//    }
//}
