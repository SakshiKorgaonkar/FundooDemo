using BusinessLayer.Interface;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRl;

        public UserBL(IUserRL userRl)
        {
            this.userRl = userRl;
        }
        public UserEntity RegisterUser(UserML userMl)
        {
            try
            {
                return userRl.RegisterUser(userMl);
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        public string LoginUser(LoginML loginMl)
        {
            try
            {
                return userRl.LoginUser(loginMl);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<UserEntity> GetUsers()
        {
            try
            {
                return userRl.GetUsers();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public UserEntity DeleteUser(int id)
        {
            try
            {
                return userRl.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public UserEntity ResetPassword(string email,string newPassword)
        {
            try
            {
                return userRl.ResetPassword(email, newPassword);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public UserEntity UpdateUser(int id,UserML userMl)
        {
            try
            {
                return userRl.UpdateUser(id, userMl);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
