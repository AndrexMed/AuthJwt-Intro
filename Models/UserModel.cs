﻿namespace AuthJwt.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
