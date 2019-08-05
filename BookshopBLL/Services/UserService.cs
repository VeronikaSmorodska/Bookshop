using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookshopBLL.DTO;
using BookshopBLL.Interfaces;
using BookshopPersistenceLayer.Interfaces;
using BookshopPersistenceLayer.Entities;
using System;
//using Bookshop.Dapper.Entities;
//using Bookshop.Dapper.Interfaces;

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
            
            Database.Users.Create(mapper.Map<User>(userDTO));
            Database.Save();
        }

        public void Delete(Guid id)
        {
            Database.Users.Delete(id);
            Database.Save();
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
            IEnumerable<User> users = Database.Users.GetAll();
            bool isARealUser = users.Any(u => u.Login == login && u.Password == password);
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

   
