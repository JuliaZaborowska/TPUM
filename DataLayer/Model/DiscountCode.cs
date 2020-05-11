namespace DataLayer.Model
{
    public class DiscountCode : BaseEntity
    {
        public string Code { get; set; }
        
        public decimal Amount { get; set; }
    }
}
