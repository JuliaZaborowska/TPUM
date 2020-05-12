using System;
using DataLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace LogicLayer.DataTransferObjects
{
    public class UserDTO
    {
        public Guid? Id { get; set; }

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

        public Cart Cart { get; set; }
    }
}