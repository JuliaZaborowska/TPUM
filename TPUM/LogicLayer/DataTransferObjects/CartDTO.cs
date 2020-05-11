using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogicLayer.DataTransferObjects
{
    public class CartDTO
    {
        [Required]
        public UserDTO User { get; set; }
        public IEnumerable<BookDTO> Books { get; set; }
    }
}