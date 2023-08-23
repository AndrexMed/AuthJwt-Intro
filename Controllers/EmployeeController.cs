using AuthJwt.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = ("Administrador"))]
        public IActionResult Get()
        {
            var listEmployee = EmployeeConstants.Employees;

            return Ok(listEmployee);
        }
    }
}
