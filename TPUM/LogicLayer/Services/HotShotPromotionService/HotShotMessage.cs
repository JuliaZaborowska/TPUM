using System;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.HotShotPromotionService
{
    public class HotShotMessage : EventArgs
    {
        public HotShotMessage(DiscountCodeDTO discountCode)
        {
            DiscountCode = discountCode;
        }
        public DiscountCodeDTO DiscountCode { get; private set; }
    }
}
