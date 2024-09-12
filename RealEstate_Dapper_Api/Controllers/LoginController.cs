using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.LoginDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(CreateLoginDto createLoginDto)
        {
            string query = "Select * From AppUser where Username=@username and Password=@password";
            var parameters = new DynamicParameters();
            parameters.Add("@username", createLoginDto.Username);
            parameters.Add("@password",createLoginDto.Password);
            
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query,parameters);
                if (values != null)
                {
                    return Ok("Başarılı");
                }
                else 
                { 
                    return BadRequest("Başarısız");
                }

            }
        }
    }
}
