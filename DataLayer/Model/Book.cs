using System.ComponentModel.DataAnnotations;

namespace DataLayer.Model
{
    public class Book : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Publisher { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        public int ReleaseYear { get; set; }

    }
}
