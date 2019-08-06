using BookshopBLL.DTO;
using System;
using System.Collections.Generic;

namespace BookshopBLL.Interfaces
{
    public interface IUserService
    {
        void Create(UserDTO userDto);
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(Guid id);
        void Update(UserDTO userDto);
        UserDTO Delete(Guid id);
        bool TestLogin(string login);
        bool TestUser(String login,string password);
        UserDTO GetByLogin(string login);
        IEnumerable<UserDTO> GetByName(string name);
        void Dispose();
    }
}
