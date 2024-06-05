﻿using BusinessLayer.Interface;
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
    public class UserBl:IUserBl
    {
        private readonly IUserRl userRl;

        public UserBl(IUserRl userRl)
        {
            this.userRl = userRl;
        }
        public UserEntity RegisterUser(UserMl userMl)
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
    }
}
