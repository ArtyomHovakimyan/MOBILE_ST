using Mic.Volo.MOBILE_ST.Data.AppDbCont;
using Mic.Volo.MOBILE_ST.Data.Models;
using Mic.Volo.MOBILE_ST.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }
        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<ApplicationDbContext>();

            var request = httpContext.Request;
            var response = httpContext.Response;

            var cardId = request.Cookies["CardId-cookie"] ?? Guid.NewGuid().ToString();
            response.Cookies.Append("CardId-cookie", cardId, new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(2)
            });

            return new ShoppingCartService(context)
            {
                Id = cardId
            };

        }
        public async Task<int> AddToCartAsync(Phone phone, int qty = 1)
        {
            return await AddOrRemoveCart(phone, qty);
        }

        private async Task<int> AddOrRemoveCart(Phone phone, int qty)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.
                SingleOrDefaultAsync(s => s.PhoneId == phone.Id && s.ShoppingCartId == Id);
            if(shoppingCartItem==null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Phone = phone,
                    Qty = 0
                };
                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            shoppingCartItem.Qty += qty;
            if(shoppingCartItem.Qty<=0)
            {
                shoppingCartItem.Qty = 0;
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
            await _context.SaveChangesAsync();
            ShoppingCartItems = null;
            return await Task.FromResult(shoppingCartItem.Qty);
        }

        public async Task ClearCartAsync()
        {
            var shoppingCartItems = _context
                .ShoppingCartItems
                .Where(s => s.ShoppingCartId == Id);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);
            ShoppingCartItems = null;
            await _context.SaveChangesAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
        {
            var subTotal = ShoppingCartItems?
                .Select(c => c.Phone.Price * c.Qty) ??
                await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Phone.Price * c.Qty)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());

        }

        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveFromCartAsync(Phone phone)
        {
            return await AddOrRemoveCart(phone, -1);
        }
    }
}
