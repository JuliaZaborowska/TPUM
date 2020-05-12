using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Model;
using DataLayer.Repositories.Users;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly DTOModelMapper _modelMapper;

        public UserService()
        {
            _userRepository = new UsersRepository(DataStore.Instance.State.Users);
            _modelMapper = new DTOModelMapper();
        }

        public UserService(UsersRepository userRepository, DTOModelMapper modelMapper)
        {
            _userRepository = userRepository;
            _modelMapper = modelMapper;
        }

        public UserDTO GetUserById(Guid id)
        {
            User user = _userRepository.Find(user => user.Id.Equals(id));
            return _modelMapper.ToUserDTO(user);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _userRepository.Items.Select(_modelMapper.ToUserDTO);
        }

        public UserDTO AddUser(UserDTO dto)
        {
            User user = _modelMapper.FromUserDTO(dto);
            User created = _userRepository.Create(user);
            return _modelMapper.ToUserDTO(created);
        }

        public void DeleteUser(Guid user)
        {
            _userRepository.Delete(user);
        }

        public UserDTO UpdateUser(UserDTO dto)
        {
            User user = _modelMapper.FromUserDTO(dto);
            User updated = _userRepository.Update(user);
            return _modelMapper.ToUserDTO(updated);
        }

        public UserDTO Save(UserDTO userDTO)
        {
            User user = _modelMapper.FromUserDTO(userDTO);
            User updated = _userRepository.CreateOrUpdate(user);
            return _modelMapper.ToUserDTO(updated);
        }
    }
}