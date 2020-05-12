using System;
using System.Collections;
using System.Collections.Generic;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.DiscountCodeService
{
    public interface IDiscountCodeService
    {
        DiscountCodeDTO AddDiscountCode(DiscountCodeDTO dto);

        IEnumerable<DiscountCodeDTO> GetAllDiscountCodes();

        void RemoveDiscountCode(Guid discountCodeId);
    }
}