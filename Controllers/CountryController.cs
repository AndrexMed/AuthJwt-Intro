using AuthJwt.Constants;
using AuthJwt.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var countrys = CountryConstants.countryModels;

            return Ok(countrys);
        }
    }
}
