using CrowDoCore.Interfaces;
using CrowDoCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoCore.Services
{
    public class UserServices : IUserServices

    {
        public CrowDoDbContext context = new CrowDoDbContext();
        public Result<bool> resultbool = new Result<bool>();
        public Result<User> resultUser = new Result<User>();

        public UserServices()
        {
        }
        public bool IsvalidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            if (!email.Contains("@"))
            {
                return false;
            }
            return true;
        }

        public Result<List<Project>> BackedProjects(string email)
        {
            throw new NotImplementedException();
        }

        public Result<bool> CreatePledge(string email, int pledgeOptionId)
        {
            throw new NotImplementedException();
        }

        public Result<List<Pledges>> MyPledges(string email)
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> MyProjects(string email)
        {
            throw new NotImplementedException();
        }

        public Result<List<Comment>> UserComments(string email)
        {
            throw new NotImplementedException();
        }

        //done
        public Result<bool> UserDelete(string email)
        {
            if (!IsvalidEmail(email))
            {
                this.resultbool.ErrorCode = 1;
                this.resultbool.ErrorText = "not valid email";
                return this.resultbool;
            }
            var user = context.Set<User>().SingleOrDefault(b => b.Email == email);
            if (user == null)
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "Wrong Email!";
                return resultbool;
            }
            if (user != null)
            {
                user.IsActive = false;
                //context.SaveChanges();
                //return resultbool;
            }
            if (context.SaveChanges() >= 1)
            {
                resultbool.ErrorCode = 0;
                resultbool.ErrorText = "Successfull";
                resultbool.Data = true;
                return resultbool;
            }
            else
            {
                resultbool.ErrorCode = 4;
                resultbool.ErrorText = "couldnt save in db";
                return resultbool;
            }
        }

        //done
        public Result<User> UserRegister(string email, string name, string surname, string address, string country, string state, string zipcode, DateTime dateofbirth)
        {
            if (!IsvalidEmail(email))
            {
                this.resultUser.ErrorCode = 1;
                this.resultUser.ErrorText = "not valid email";                
                return this.resultUser;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                resultUser.ErrorCode = 5;
                resultUser.ErrorText = "Wrong name!";
                return resultUser;
            }

            if (dateofbirth.AddYears(18) > DateTime.Now)
            {
                resultUser.ErrorCode = 6;
                resultUser.ErrorText = "You are no over 18 years old !";
                return resultUser;
            }

            if (context.Set<User>().Any(e => e.Email == email))
            {
                resultUser.ErrorCode = 5;
                resultUser.ErrorText = "A user with that email is already exists !";
                return resultUser;
            }

            User user = new User()
            {
                Name = name,
                Surname = surname,
                Email = email,
                DateOfBirth = dateofbirth,
                Address = address,
                Country = country,
                State = state,
                ZipCode = zipcode,
                IsActive = true
                
            };
            context.Add(user);
            if (context.SaveChanges() >= 1)
            {
                resultUser.ErrorCode = 0;
                resultUser.ErrorText = "Successfull";
                resultUser.Data = user;
                return resultUser;
            }
            else
            {
                resultUser.ErrorCode = 4;
                resultUser.ErrorText = "couldnt save in db";
                return resultUser;
            }
        }

        public Result<bool> UserUpdate(string email, string name, string surname, string address, string country, string state, string zipcode, DateTime dateofbirth, string newEmail)
        {
            throw new NotImplementedException();
        }
    }
}
