using System;
using System.Collections.Generic;
using System.Text;

namespace BookshopPersistenceLayer.Entities
{
    public class User 
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public User()
        {
            UserId = Guid.NewGuid();
        }
    }
}
