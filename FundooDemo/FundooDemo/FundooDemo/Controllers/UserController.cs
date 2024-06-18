using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Utility;
using MailKit.Net.Smtp;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FundooDemo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBl;
        private readonly ProjectContext projectContext;
        private readonly TokenGenerator tokenGenerator;
        public UserController(IUserBL userBl, ProjectContext projectContext, TokenGenerator tokenGenerator)
        {
            this.userBl = userBl;
            this.projectContext = projectContext;
            this.tokenGenerator = tokenGenerator;
        }
        [HttpPost("register")]
        public IActionResult RegisterUser(UserML userMl)
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
        [HttpPost("login")]
        public IActionResult LoginUser(LoginML loginMl)
        {
            try
            {
                var result = userBl.LoginUser(loginMl);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var result = userBl.GetUsers();
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var result = userBl.DeleteUser(id);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpPost("forgot")]
        public IActionResult ForgotPassword(string emailId)
        {
            try
            {
                if (string.IsNullOrEmpty(emailId))
                    throw new ArgumentException("EmailId cannot be null or empty", nameof(emailId));

                var user = projectContext.Users.FirstOrDefault(x => x.Email == emailId);
                if (user == null)
                {
                    throw new CustomException1("Email doesn't exist! Register first");
                }

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("korgaonkarsakshi23@gmail.com"));
                email.To.Add(MailboxAddress.Parse(emailId));
                email.Subject = "Password reset link";

                string token = tokenGenerator.GenerateToken(user);
                string resetLink = "http://localhost:5240/api/users/reset?token=" + token;
                string body = $"<p>Please reset your password by clicking on the following link:</p><p><a href='{resetLink}'>Reset Password</a></p>";

                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
                
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("korgaonkarsakshi23@gmail.com", "oakyszygvkzsqevu");
                smtp.Send(email);
                smtp.Disconnect(true);                
                
                return Ok(token);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpPost("reset")]
        public IActionResult ResetPassword(string token, [FromBody] string newPassword)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
                var result = userBl.ResetPassword(email, newPassword);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpPost("update")]
        public IActionResult UpdateUser(int id,UserML user)
        {
            try
            {
                var result=userBl.UpdateUser(id, user);
                return Ok(result);
            }
            catch (CustomException1 ex)
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
                return StatusCode(500, result);
            }
        }
    }
}
