using AuthJwt.Models;

namespace AuthJwt.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new()
        {
            new UserModel() { UserName = "GioAlzate", Password = "12345", Rol = "Administrador", Email = "gio@gmail.com", FirstName = "Giovanni", LastName = "Andres"},
            new UserModel() { UserName = "Juli", Password = "12345", Rol = "Auxiliar", Email = "juli@gmail.com", FirstName = "Julieta", LastName = "Alzate"}

        };
    }
}
