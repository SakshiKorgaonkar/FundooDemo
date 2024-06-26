using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity RegisterUser(UserML userMl);
        public string LoginUser(LoginML loginMl);
        public List<UserEntity> GetUsers();
        public UserEntity DeleteUser(int id);
        public UserEntity ResetPassword(string email,string newPassword);
        public UserEntity UpdateUser(int id,UserML userMl);
        public void SetSession(string key, string value);
        public string GetSession(string key);
    }
}
