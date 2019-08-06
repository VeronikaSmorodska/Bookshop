using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using BookshopBLL.DTO;
using BookshopBLL.Interfaces;
using BookshopPersistenceLayer.Interfaces;
using BookshopPersistenceLayer.Entities;
using System;
using BookshopBLL.Infrastructure;

namespace BookshopBLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper mapper;
        public UserService(IUnitOfWork uow, IMapper _mapper)
        {
            Database = uow;
            mapper = _mapper;
        }
        public IEnumerable<UserDTO> GetAll()
        {
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }
        public UserDTO Get(Guid id)
        {
            return mapper.Map<User, UserDTO>(Database.Users.Get(id));
        }
        public void Create(UserDTO userDTO)
        {
            UserDTO userHashed = new UserDTO
            {
                UserId = userDTO.UserId,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Login = userDTO.Login,
                Password = HashData.GenerateSHA512(userDTO.Password),
                Role = userDTO.Role 
            };
            Database.Users.Create(mapper.Map<User>(userHashed));
            Database.Save();
        }
        public UserDTO Delete(Guid id)
        {
            UserDTO userDto = mapper.Map<User, UserDTO>(Database.Users.Delete(id));
            Database.Save();
            return userDto;
        }
        public void Update(UserDTO userDTO)
        {
            Database.Users.Update(mapper.Map<User>(userDTO));
            Database.Save();
        }
        public bool TestLogin(String login)
        {
            IEnumerable<User> users = Database.Users.GetAll();
            bool isLoginTaken = users.Any(u => u.Login == login);
            return isLoginTaken;
        }
        public bool TestUser(String login, String password)
        {
            String passwordHashed = HashData.GenerateSHA512(password);
            IEnumerable<User> users = Database.Users.GetAll();

            bool isARealUser = users.Any(u => u.Login == login && u.Password == passwordHashed);
            return isARealUser;
        }
        public UserDTO GetByLogin(string login)
        {
            IEnumerable<UserDTO> users = mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
            UserDTO userByLogin = users.Where(u => u.Login == login).FirstOrDefault();
            return userByLogin;
        }
        public IEnumerable<UserDTO> GetByName(string name)
        {
            IEnumerable<UserDTO> users = mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
            IEnumerable<UserDTO> searchInNames = users.Where(u => u.Name == name);
            IEnumerable<UserDTO> searchInSurnames = users.Where(u => u.Surname == name);
            IEnumerable<UserDTO> userByName=searchInNames.Union(searchInSurnames).ToList();
            return userByName;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

   
