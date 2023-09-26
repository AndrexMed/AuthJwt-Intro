using System;
using System.Collections.Generic;

namespace AuthJwt.Models.Entities;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public byte? Age { get; set; }

    public string? Rol { get; set; }
}
