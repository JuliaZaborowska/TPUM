using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer
{
    public class State
    {
        public State(IList<Book> books, IList<User> users)
        {
            Books = books;
            Users = users;
        }

        public IList<Book> Books { get; private set; }
        public IList<User> Users { get; private set; }
    }
}
