using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRl:IUserRl
    {
        private readonly ProjectContext projectContext;
        private readonly IConfiguration configuration;
        private readonly PasswordHashing passwordHashing;
        public UserRl(ProjectContext projectContext, PasswordHashing passwordHashing,IConfiguration configuration)
        {
            this.projectContext = projectContext;
            this.passwordHashing = passwordHashing;
            this.configuration = configuration;
        }
        public UserEntity RegisterUser(UserMl userMl)
        {
            var result=projectContext.Users.FirstOrDefault(u => u.Email == userMl.Email);
            if(result != null)
            {
                throw new CustomException1("Email already exists");
            }
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Name = userMl.Name;
                userEntity.Email = userMl.Email;
                userEntity.Password = passwordHashing.Hasher(userMl.Password);
                userEntity.PhoneNumber = userMl.PhoneNumber;
                projectContext.Users.Add(userEntity);
                projectContext.SaveChanges();
                return userEntity;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
        public string LoginUser(LoginMl loginMl)
        {
            var result=projectContext.Users.FirstOrDefault(u=>u.Email== loginMl.Email);
            if(result == null)
            {
                throw new CustomException1("Invalid email or password");
            }
            try
            {
                bool isPasswordValid = passwordHashing.VerifyPassword(loginMl.Password,result.Password);
                if (isPasswordValid)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,configuration["JWT:Subject"]),
                        new Claim("Id",result.Id.ToString()),
                        new Claim("Username", result.Name),
                        new Claim("Email",result.Email),
                        new Claim("Phone",result.PhoneNumber.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                    var signin=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token=new JwtSecurityToken(
                        issuer:configuration["JWT:Issuer"],
                        audience:configuration["JWT:Audience"],
                        claims:claims,
                        expires:DateTime.UtcNow.AddMinutes(10),
                        signingCredentials:signin);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new CustomException1("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong");
            }
        }
        public List<UserEntity> GetUsers()
        {
            List<UserEntity> userEntities=projectContext.Users.ToList();
            if (userEntities == null || userEntities.Count==0)
            {
                throw new CustomException1("No users registered");
            }
            try
            {
                return userEntities; 
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
        public UserEntity DeleteUser(int id)
        {
            UserEntity userToRemove=projectContext.Users.FirstOrDefault(u => u.Id == id);
            if(userToRemove == null)
            {
                throw new CustomException1("No such user exists");
            }
            try
            {
                projectContext.Users.Remove(userToRemove);
                projectContext.SaveChanges();
                return userToRemove;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
