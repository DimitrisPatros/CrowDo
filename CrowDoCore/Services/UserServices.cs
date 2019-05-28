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
        public Result<List<Project>> resultListProject = new Result<List<Project>>();
        public Result<List<Pledges>> resultPledges = new Result<List<Pledges>>();
        public Result<List<Project>> resultListOfMyProjects = new Result<List<Project>>();
        public Result<List<Comment>> resultListOfMyComments = new Result<List<Comment>>();


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

        public bool IsValidateUser(string email, int projectId)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if (project == null)
            {
                return false;
            }
            if (user == null)
            {
                return false;
            }
            if (user.UserId == project.UserId)
            {
                return true;
            }
            return false;
        }

        //done
        public Result<User> UserRegister(string email, string name, string surname, string address,
                                         string country, string state, string zipcode, DateTime dateofbirth)
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

            resultUser.ErrorCode = 4;
            resultUser.ErrorText = "couldnt save in db";
            return resultUser;

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

            resultbool.ErrorCode = 4;
            resultbool.ErrorText = "couldnt save in db";
            return resultbool;

        }
        //not
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


            var user = context.Set<User>().SingleOrDefault(b => b.Email == email);
            if (user == null)
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email!";
                return resultbool;
            }


            user.Name = name;
            user.Surname = surname;
            user.Address = address;
            user.Country = country;
            user.State = state;
            user.ZipCode = zipcode;
            user.DateOfBirth = dateofbirth;
            //user.Email = newEmail;
            //context.SaveChanges();





            if (context.SaveChanges() >= 1)
            {
                resultbool.ErrorCode = 0;
                resultbool.ErrorText = "Successfull";
                resultbool.Data = true;
                return resultbool;
            }

            resultbool.ErrorCode = 4;
            resultbool.ErrorText = "couldnt save in db";
            return resultbool;

        }
        //not
        public Result<bool> CreatePledge(string email, int pledgeOptionId)
        {
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

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
        //done
        public Result<List<Project>> BackedProjects(string email)
        {
            var listOfBackedProjects = context.Set<Project>().Where(p => p.ProjectSuccess == true).ToList();
            if (listOfBackedProjects.Count() == 0)
            {
                resultListProject.ErrorCode = 20;
                resultListProject.ErrorText = "there are not backed projects";
                return resultListProject;

            }
            return resultListProject;


        }

        //done
        public Result<List<Pledges>> MyPledges(string email)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultPledges.ErrorCode = 1;
                resultPledges.ErrorText = "not valid email!";
                return resultPledges;
            }

            var listOfPledges = context.Set<Pledges>().Where(l => l.UserId == user.UserId).ToList();
            if (listOfPledges == null)
            {
                resultPledges.ErrorCode = 21;
                resultPledges.ErrorText = "there are not pledges!";
                return resultPledges;
            }

            if (context.SaveChanges() >= 1)
            {
                resultPledges.ErrorCode = 0;
                resultPledges.ErrorText = "Successfull";
                resultPledges.Data = null;
                return resultPledges;
            }

            resultPledges.ErrorCode = 4;
            resultPledges.ErrorText = "couldnt save in db";
            resultPledges.Data = null;
            return resultPledges;


        }


        //done
        public Result<List<Project>> MyProjects(string email)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultListOfMyProjects.ErrorCode = 1;
                resultListOfMyProjects.ErrorText = "not valid email!";
                return resultListOfMyProjects;
            }
            var listOfMyProjects = context.Set<Project>().Where(lop => lop.UserId == user.UserId).ToList();
            if (listOfMyProjects == null)
            {
                resultListOfMyProjects.ErrorCode = 22;
                resultListOfMyProjects.ErrorText = "there are not projects!";
                return resultListOfMyProjects;
            }
            if (context.SaveChanges() >= 1)
            {
                resultListOfMyProjects.ErrorCode = 0;
                resultListOfMyProjects.ErrorText = "Successfull";
                resultListOfMyProjects.Data = null;
                return resultListOfMyProjects;
            }

            resultListOfMyProjects.ErrorCode = 4;
            resultListOfMyProjects.ErrorText = "couldnt save in db";
            resultListOfMyProjects.Data = null;
            return resultListOfMyProjects;
        }

        //done
        public Result<List<Comment>> UserComments(string email)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultListOfMyComments.ErrorCode = 1;
                resultListOfMyComments.ErrorText = "not valid email!";
                return resultListOfMyComments;
            }
            var listoOfMyComments = context.Set<Comment>().Where(loc => loc.UserId == user.UserId);
            if (listoOfMyComments == null)
            {
                resultListOfMyComments.ErrorCode = 23;
                resultListOfMyComments.ErrorText = "there are not comments!";
                return resultListOfMyComments;
            }
            if (context.SaveChanges() >= 1)
            {
                resultListOfMyComments.ErrorCode = 0;
                resultListOfMyComments.ErrorText = "Successfull";
                resultListOfMyComments.Data = null;
                return resultListOfMyComments;
            }

            resultListOfMyComments.ErrorCode = 4;
            resultListOfMyComments.ErrorText = "couldnt save in db";
            resultListOfMyComments.Data = null;
            return resultListOfMyComments;
        }
    }
}
