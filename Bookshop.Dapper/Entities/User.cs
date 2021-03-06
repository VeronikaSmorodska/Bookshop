﻿using System;

namespace Bookshop.Dapper.Entities
{
    public class User 
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public User()
        {
            UserId = Guid.NewGuid();
        }
    }
}
