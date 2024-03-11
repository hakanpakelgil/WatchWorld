using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        public Task<Basket> GetOrCreateBasketAsync(string buyerId);
        public Task<Basket> AddItemToBasketAsync(string buyerId,int productId,int quantity);
        public Task<Basket> SetQuantitiesAsync(string buyerId,Dictionary<int,int> quantities);
        public Task DeleteBasketItemAsync(string buyerId,int productId);
        public Task EmptyBasketAsync(string buyerId);
        public Task TransferBasketAsync(string sourceBuyerId, string destinationBuyerId);        
    }
}
