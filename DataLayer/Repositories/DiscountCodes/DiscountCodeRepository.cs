using System;
using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Repositories.DiscountCodes
{
    public class DiscountCodeRepository : CrudRepository<DiscountCode>, IDiscountCodeRepository
    {
        public DiscountCodeRepository(IList<DiscountCode> discountCodes) : base(discountCodes)
        {
        }

        public DiscountCode GetRandomDiscountCode()
        {
            Random rnd = new Random();
            int index = rnd.Next(Items.Count);
            return Items[index];
        }
    }
}