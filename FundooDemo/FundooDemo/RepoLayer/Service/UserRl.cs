using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
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
        public UserRl(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
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
                userEntity.Password = userMl.Password;
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
    }
}
