namespace E_Commerce.Models.Dto.Response
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> BasketItem { get; set; }
        // public string PaymentIntentId { get; set; }
        // public string ClientSecret { get; set; }
    }
}
