using System;
using System.Collections.Generic;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.UserService
{
    interface IUserService
    {
        UserDTO GetUserById(Guid id);
        IEnumerable<User> GetAllUsers(Guid id);
        UserDTO AddUser(UserDTO dto);
        void DeleteUser(Guid user);
        UserDTO UpdateUser(UserDTO dto);
        UserDTO Save(UserDTO user);
    }
}
