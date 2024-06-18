﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
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
    public class UserRL:IUserRL
    {
        private readonly ProjectContext projectContext;
        private readonly PasswordHashing passwordHashing;
        private readonly TokenGenerator tokenGenerator;
        public UserRL(ProjectContext projectContext, PasswordHashing passwordHashing, TokenGenerator tokenGenerator)
        {
            this.projectContext = projectContext;
            this.passwordHashing = passwordHashing;
            this.tokenGenerator = tokenGenerator;
        }
        public UserEntity RegisterUser(UserML userMl)
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
        public string LoginUser(LoginML loginMl)
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
                    return tokenGenerator.GenerateToken(result);
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
            UserEntity userToRemove = projectContext.Users.FirstOrDefault(u => u.Id == id);
            if (userToRemove == null)
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
        public UserEntity ResetPassword(string email,string newPassword)
        {
            var user= projectContext.Users.FirstOrDefault(x=>x.Email== email);
            user.Password = passwordHashing.Hasher(newPassword);
            projectContext.Users.Update(user);
            projectContext.SaveChanges();
            return user;
        }
        public UserEntity UpdateUser(int id,UserML user)
        {
            var userToUpdate=projectContext.Users.FirstOrDefault(y => y.Id == id);
            if(userToUpdate == null)
            {
                throw new CustomException1("User doesn't exist");
            }
            userToUpdate.Email= user.Email;
            userToUpdate.Password= user.Password;
            userToUpdate.Name= user.Name;
            userToUpdate.PhoneNumber= user.PhoneNumber;
            projectContext.Users.Update(userToUpdate);
            projectContext.SaveChanges();
            return userToUpdate;
        }
    }
}
