using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoServices.Services
{
    public class UserServices : IUserServices
    {
        public CrowDoDbContext context;
        public Result<bool> resultbool = new Result<bool>();
        public Result<User> resultUser = new Result<User>();

        public UserServices(CrowDoDbContext contexts)
        {
            context = contexts;
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
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                this.resultUser.ErrorCode = 1;
                this.resultUser.ErrorText = "not valid email";
                return this.resultUser;
            }

            //check if there is a name 
            if (string.IsNullOrWhiteSpace(name))
            {
                resultUser.ErrorCode = 5;
                resultUser.ErrorText = "Wrong name!";
                return resultUser;
            }

            //check if the user is over 18 years old
            if (dateofbirth.AddYears(18) > DateTime.Now)
            {
                resultUser.ErrorCode = 6;
                resultUser.ErrorText = "You are no over 18 years old !";
                return resultUser;
            }

            //check if the user with this email already exist
            if (context.Set<User>().Any(e => e.Email == email))
            {
                resultUser.ErrorCode = 5;
                resultUser.ErrorText = "A user with that email is already exists !";
                return resultUser;
            }

            User user = new User();
            user.Name = name;
            user.Surname = surname;
            user.Email = email;
            user.DateOfBirth = dateofbirth;
            user.Address = address;
            user.Country = country;
            user.State = state;
            user.ZipCode = zipcode;
            user.IsActive = true;
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
