using DataLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories.DiscountCodeRepository
{
    public class DiscountCodeRepository : CrudRepository<DiscountCode>, IDiscountCodeRepository
    {
        public DiscountCodeRepository(IList<DiscountCode> discountCodes) : base(discountCodes)
        {
        }
    }
}