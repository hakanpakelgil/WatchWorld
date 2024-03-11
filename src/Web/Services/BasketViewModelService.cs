using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using System.Security.Claims;
using Web.Extensions;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext HttpContext => _httpContextAccessor.HttpContext!;
        private string? UserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string? AnonId => HttpContext.Request.Cookies[Constants.BASKET_COOKIE];
        private string BuyerId => UserId ?? AnonId ?? CreateAnonymousId();

        private string _createdAnonId = null!;

        private string CreateAnonymousId()
        {
            //benzersiz bir geçici id oluştur, cookilerde depola,gizli bir field'a aktar ve döndür
            if(_createdAnonId != null) return _createdAnonId;

            _createdAnonId = Guid.NewGuid().ToString(); 

            HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIE, _createdAnonId,new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true
            });

            return _createdAnonId;
        }

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity)
        {
            var basket = await _basketService.AddItemToBasketAsync(BuyerId, productId, quantity);

            return basket.ToBasketViewModel();
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basket = await _basketService.GetOrCreateBasketAsync(BuyerId);
            return basket.ToBasketViewModel();
        }

        public async Task EmptyBasketAsync()
        {
            await _basketService.EmptyBasketAsync(BuyerId);
        }
    }
}
