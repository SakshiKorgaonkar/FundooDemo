﻿using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBl
    {
        public UserEntity RegisterUser(UserMl userMl);

        public UserEntity LoginUser(LoginMl loginMl);
        public List<UserEntity> GetUsers();

        public UserEntity DeleteUser(int id);
    }
}
