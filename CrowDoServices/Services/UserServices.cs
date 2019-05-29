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
        public Result<List<Project>> resultListProject = new Result<List<Project>>();
        public Result<List<Pledges>> resultPledges = new Result<List<Pledges>>();
        public Result<List<Project>> resultListOfMyProjects = new Result<List<Project>>();
        public Result<List<Comment>> resultListOfMyComments = new Result<List<Comment>>();

        public UserServices(CrowDoDbContext contexts)
        {
            context = contexts;
        }

        public UserServices()
        {
        }
        private bool IsvalidEmail(string email)
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

        private bool IsValidateUser(string email)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        //done - checked
        public Result<User> UserRegister(string email, string name, string surname, string address,
                                         string country, string state, string zipcode, DateTime dateofbirth)
        {
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                resultUser.ErrorCode = 1;
                resultUser.ErrorText = "not valid email";
                return resultUser;
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

            //check if the result has been saved successfully 
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
        //done - checked
        public Result<bool> UserDelete(string email)
        {
            if (!IsvalidEmail(email))
            {
                this.resultbool.ErrorCode = 1;
                this.resultbool.ErrorText = "not valid email";
                return this.resultbool;
            }

            //check user's email
            var user = context.Set<User>().SingleOrDefault(b => b.Email == email);
            if (user == null)
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email!";
                return resultbool;
            }
            if (user != null)
            {
                user.IsActive = false;
                //context.SaveChanges();
                //return resultbool;
            }
            //check if the result has been saved successfully 
            if (context.SaveChanges() >= 1)
            {
                resultbool.ErrorCode = 0;
                resultbool.ErrorText = "Successfull";
                resultbool.Data = true;
                return resultbool;
            }

            resultbool.ErrorCode = 4;
            resultbool.ErrorText = "couldnt save in db";
            resultbool.Data = false;
            return resultbool;

        }
        //done - checked
        public Result<bool> UserUpdate(string email, string name, string surname, string address,
                                       string country, string state, string zipcode, DateTime dateofbirth, string newEmail)
        {
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //check user's email
            var user = context.Set<User>().SingleOrDefault(b => b.Email == email);
            if (user == null)
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email!";
                return resultbool;
            }

            //update user's info

            user.Name = name;
            user.Surname = surname;
            user.Address = address;
            user.Country = country;
            user.State = state;
            user.ZipCode = zipcode;
            user.DateOfBirth = dateofbirth;
            //user.Email = newEmail;
            


            //check if the result has been saved successfully
            if (context.SaveChanges() >= 1)
            {
                resultbool.ErrorCode = 0;
                resultbool.ErrorText = "Successfull";
                resultbool.Data = true;
                return resultbool;
            }

            resultbool.ErrorCode = 4;
            resultbool.ErrorText = "couldnt save in db";
            resultbool.Data = false;
            return resultbool;

        }
        //done -> problem
        public Result<bool> CreatePledge(string email, int pledgeOptionId)
        {
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            if (!IsValidateUser(email))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "not authorize access";
                resultbool.Data = false;
                return resultbool;
            }

            //check if a pledge exists?
            var pledgeoption = context.Set<PledgeOptions>().SingleOrDefault(po => po.PledgeOptionsId == pledgeOptionId);
            if (pledgeoption == null)
            {
                resultbool.ErrorCode = 10;
                resultbool.ErrorText = "no pledge options";
                resultbool.Data = false;
                return resultbool;

            }
            if (pledgeoption != null)
            {
                var newpledge = new Pledges();
                newpledge.PledgeOptionId = pledgeOptionId;
                context.Add(newpledge);
            }



            //check if the result has been saved successfully
            if (context.SaveChanges() >= 1)
            {
                resultbool.ErrorCode = 0;
                resultbool.ErrorText = "Successfull";
                resultbool.Data = true;
                return resultbool;
            }

            resultbool.ErrorCode = 4;
            resultbool.ErrorText = "couldnt save in db";
            resultbool.Data = false;
            return resultbool;

        }
        ////done-checked
        public Result<List<Project>> BackedProjects(string email)
        {
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                resultUser.ErrorCode = 1;
                resultUser.ErrorText = "not valid email";

            }
            //check if a project is backed
            var listOfBackedProjects = context.Set<Project>().Where(p => p.ProjectSuccess == true).ToList();
            if (listOfBackedProjects.Count() == 0)
            {
                resultListProject.ErrorCode = 20;
                resultListProject.ErrorText = "there are not backed projects";
                return resultListProject;

            }

            //Print the list
            resultListProject.ErrorCode = 0;
            resultListProject.ErrorText = "Successfull";
            resultListProject.Data = listOfBackedProjects;
            return resultListProject;

        }

        //done-checked
        public Result<List<Pledges>> MyPledges(string email)
        {
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                resultUser.ErrorCode = 1;
                resultUser.ErrorText = "not valid email";

            }
            //check user's email
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultPledges.ErrorCode = 1;
                resultPledges.ErrorText = "not valid email!";
                return resultPledges;
            }

            //check if a user has pledges
            var listOfPledges = context.Set<Pledges>().Where(l => l.UserId == user.UserId).ToList();
            if (listOfPledges.Count() == 0)
            {
                resultPledges.ErrorCode = 21;
                resultPledges.ErrorText = "there are not pledges!";
                return resultPledges;
            }


            //Print the list
            resultPledges.ErrorCode = 0;
            resultPledges.ErrorText = "Successfull";
            resultPledges.Data = listOfPledges;
            return resultPledges;



        }


        //done-checked
        public Result<List<Project>> MyProjects(string email)
        {
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                resultUser.ErrorCode = 1;
                resultUser.ErrorText = "not valid email";

            }

            //check user's email
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultListOfMyProjects.ErrorCode = 1;
                resultListOfMyProjects.ErrorText = "not valid email!";
                return resultListOfMyProjects;
            }
            var listOfMyProjects = context.Set<Project>().Where(lop => lop.UserId == user.UserId).ToList();
            if (listOfMyProjects.Count() == 0)
            {
                resultListOfMyProjects.ErrorCode = 22;
                resultListOfMyProjects.ErrorText = "there are not projects!";
                return resultListOfMyProjects;
            }

            //Print the list
            resultListOfMyProjects.ErrorCode = 0;
            resultListOfMyProjects.ErrorText = "Successfull";
            resultListOfMyProjects.Data = listOfMyProjects;
            return resultListOfMyProjects;



        }

        //done-checked
        public Result<List<Comment>> UserComments(string email)
        {
            //check if the email is valid
            if (!IsvalidEmail(email))
            {
                resultUser.ErrorCode = 1;
                resultUser.ErrorText = "not valid email";

            }

            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultListOfMyComments.ErrorCode = 1;
                resultListOfMyComments.ErrorText = "not valid email!";
                return resultListOfMyComments;
            }
            var listoOfMyComments = context.Set<Comment>().Where(loc => loc.UserId == user.UserId).ToList();
            if (listoOfMyComments.Count() == 0)
            {
                resultListOfMyComments.ErrorCode = 23;
                resultListOfMyComments.ErrorText = "there are not comments!";
                return resultListOfMyComments;
            }

            //Print the list
            resultListOfMyComments.ErrorCode = 0;
            resultListOfMyComments.ErrorText = "Successfull";
            resultListOfMyComments.Data = listoOfMyComments;
            return resultListOfMyComments;

        }
    }
}
