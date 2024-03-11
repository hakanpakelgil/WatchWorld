using Web.Models;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        public Task<BasketViewModel> GetBasketViewModelAsync();
        public Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);
        public Task EmptyBasketAsync();
        public Task RemoveItemAsync(int productId);
        public Task<BasketViewModel> SetQuantitiesAsync(Dictionary<int, int> quantities);

        public Task TransferBasketAsync();
    }
}
