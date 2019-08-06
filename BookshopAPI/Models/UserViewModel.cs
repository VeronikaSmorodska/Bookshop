using System;

namespace BookshopAPI.Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
