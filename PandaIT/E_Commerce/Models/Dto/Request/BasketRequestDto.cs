using E_Commerce.Models.Domain;

namespace E_Commerce.Models.Dto.Request
{
    public class BasketRequestDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public ICollection<BasketItem>? BasketItem { get; set; }
//        public string PaymentIntentId { get; set; }
//        public string ClientSecret { get; set; }

    }
}
