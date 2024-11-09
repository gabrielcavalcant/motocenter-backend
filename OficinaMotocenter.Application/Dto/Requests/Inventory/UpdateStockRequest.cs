namespace OficinaMotocenter.Application.Dto.Requests.Inventory
{
    public class UpdateStockRequest
    {
        public Guid ItemId { get; set; } 
        public int QuantityChange { get; set; }
    }
}
