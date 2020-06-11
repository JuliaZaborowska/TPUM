using DataLayer.Model;

namespace DataLayer.Observer
{
    public class PromotionEvent
    {
        public PromotionEvent(DiscountCode discountCode)
        {
            DiscountCode = discountCode;
        }

        public DiscountCode DiscountCode { get; private set; }
    }
}
