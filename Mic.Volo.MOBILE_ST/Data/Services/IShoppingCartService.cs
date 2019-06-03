using Mic.Volo.MOBILE_ST.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.Services
{
    public interface IShoppingCartService
    {
        string Id { get; set; }
        IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> AddToCartAsync(Phone phone, int qty = 1);
        Task ClearCartAsync();
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task<int> RemoveFromCartAsync(Phone phone);
        Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync();
    }
}
