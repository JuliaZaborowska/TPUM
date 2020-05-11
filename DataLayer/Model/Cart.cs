using System.Collections.Generic;

namespace DataLayer.Model
{
    public class Cart : BaseEntity
    {
        public User User { get; set; }

        public IList<Book> Books { get; set; }
    }
}