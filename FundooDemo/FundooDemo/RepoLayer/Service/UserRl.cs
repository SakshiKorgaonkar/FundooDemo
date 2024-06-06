using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class UserRl:IUserRl
    {
        private readonly ProjectContext projectContext;
        private readonly PasswordHashing passwordHashing;
        public UserRl(ProjectContext projectContext, PasswordHashing passwordHashing)
        {
            this.projectContext = projectContext;
            this.passwordHashing = passwordHashing;
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
                var hashedPassword=passwordHashing.Hasher(userMl.Password);
                userEntity.Password = hashedPassword;
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
        public UserEntity LoginUser(LoginMl loginMl)
        {
            var result=projectContext.Users.FirstOrDefault(u=>u.Email== loginMl.Email);
            if(result == null)
            {
                throw new CustomException1("Invalid email or password");
            }
            try
            {
                if (passwordHashing.VerifyPassword(result.Password)==loginMl.Password)
                {
                    return result;
                }
                else
                {
                    throw new CustomException1("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
        public List<UserEntity> GetUsers()
        {
            List<UserEntity> userEntities=projectContext.Users.ToList();
            if (userEntities == null)
            {
                throw new CustomException1("No users registered");
            }
            try
            {
                if (userEntities!=null)
                {
                    return userEntities;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
            return userEntities;
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
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong");
            }
            return userToRemove;
        }
    }
}
