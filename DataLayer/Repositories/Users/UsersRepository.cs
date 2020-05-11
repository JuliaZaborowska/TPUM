using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Repositories.Users
{
    public class UsersRepository : CrudRepository<User>, IUserRepository
    {
        public UsersRepository(IList<User> users) : base(users)
        {
        }
    }
}