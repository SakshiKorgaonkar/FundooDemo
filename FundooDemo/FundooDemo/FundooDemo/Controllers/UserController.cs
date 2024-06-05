using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepoLayer.CustomException;
using RepoLayer.Entity;

namespace FundooDemo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBl userBl;

        public UserController(IUserBl userBl)
        {
            this.userBl = userBl;
        }
        [HttpPost]
        public IActionResult RegisterUser(UserMl userMl)
        {
            try
            {
                var result = userBl.RegisterUser(userMl);
                return Ok(result);
            }        
            catch(CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }     
            catch(Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500,result);
            }
        }
    }
}
