using System.ComponentModel.DataAnnotations;

namespace LogicLayer.DataTransferObjects
{
    public class DiscountCodeDTO
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Range(0.0, 20.0)]
        public decimal Amount { get; set; }
    }
}