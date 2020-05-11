using System;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.DiscountCodeService
{
    public interface IDiscountCodeService
    {
        DiscountCodeDTO AddDiscountCode(DiscountCodeDTO dto);
        void RemoveDiscountCode(Guid discountCodeId);
    }
}