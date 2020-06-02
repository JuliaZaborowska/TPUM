using System;

namespace LogicLayer.DataTransferObjects
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
