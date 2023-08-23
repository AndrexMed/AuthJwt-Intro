using AuthJwt.Models;

namespace AuthJwt.Constants
{
    public class EmployeeConstants
    {
        public static List<EmployeeModel> Employees = new()
        {
            new EmployeeModel() {FirstName = "Tomas", LastName = "Aliaga", Email = "taliaga@gmail.com" },
            new EmployeeModel() {FirstName = "Marcos", LastName = "Gonzalez", Email = "mgonzalez@gmail.com" },
        };
    }
}
