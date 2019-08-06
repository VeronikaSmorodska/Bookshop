using System;

namespace BookshopBLL.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleDTO Role { get; set; }
        public UserDTO()
        {
            UserId = Guid.NewGuid();
        }
    }
}

