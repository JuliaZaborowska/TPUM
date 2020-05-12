using DataLayer.Model;

namespace DataLayer.Repositories.DiscountCodes
{
    public interface IDiscountCodeRepository : ICrudRepository<DiscountCode>
    {
        DiscountCode GetRandomDiscountCode();
    }
}