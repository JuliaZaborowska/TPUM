using System.ComponentModel.DataAnnotations;

namespace DataLayer.Model
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
