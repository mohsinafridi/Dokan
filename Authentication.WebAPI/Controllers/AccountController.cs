using JWTManager;
using JWTManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK");
        }

        [HttpPost]
        public IActionResult Auhenticate([FromBody] AuthenticationRequest authenticateRequest)
        {
            var response = _jwtTokenHandler.GenerateToken(authenticateRequest);
            if (response is null)
                return Unauthorized();

            return Ok(response);
        }
        
    }
}
