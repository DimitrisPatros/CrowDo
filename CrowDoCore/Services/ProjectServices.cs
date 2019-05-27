using CrowDoCore.Interfaces;
using CrowDoCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoCore.Services
{
    public class ProjectServices : IProjectServices
    {
        //private CrowDoDbContext _context;
        //private Result<bool> _resultbool;
        //public ProjectServices(CrowDoDbContext crowDoDbContext, Result<bool> result)
        //{
        //    _context = crowDoDbContext;
        //    _resultbool = result;
        //}
        public CrowDoDbContext context = new CrowDoDbContext();
        public Result<bool> resultbool = new Result<bool>();

        public ProjectServices()
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
        public Result<bool> AddCategoryToProject(string email, int projectId, string categoryName)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if (project == null)
            {
                resultbool.ErrorCode = 8;
                resultbool.ErrorText = "projet doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "This user can't modify this project";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the project has already this category
            var projectCategory = context.Set<ProjectCategories>().Where(p => p.ProjectId == projectId).ToList();
            if (projectCategory.Any(pc => pc.Category.CategoryName == categoryName))
            {
                resultbool.ErrorCode = 2;
                resultbool.ErrorText = "the project has already this category";
                resultbool.Data = false;
                return resultbool;
            }

            //create a new category
            var category = context.Set<Category>().SingleOrDefault(c => c.CategoryName == categoryName);
            if (category == null)
            {
                category = new Category();
                category.CategoryName = categoryName;
            }
            context.Add(category);
            if (context.SaveChanges() < 1)
            {
                resultbool.ErrorCode = 4;
                resultbool.ErrorText = "couldnt save in db";
                resultbool.Data = false;
                return resultbool;
            }


            //create the projectcategory
            
            var newProjectCategory = new ProjectCategories();
            newProjectCategory.Project = project;
            newProjectCategory.ProjectId = project.ProjectId;
            newProjectCategory.Category = category;
            newProjectCategory.CategoryId = category.CategoryId;
            context.Add(newProjectCategory);
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
                resultbool.Data = false;
                return resultbool;
            }
        }


        public Result<bool> AddPledgeOptionToProject(string email, int projectId,
            string titleOfPledge, double priceOfPledge, DateTime estimateDelivery,
            DateTime durationOfPldege, int numberOfAvailablePledges, string description)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "This user can't modify this project";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the project has already this pledge option
            if (context.Set<PledgeOptions>().Any(po => po.TitleOfPledge == titleOfPledge)
                && context.Set<PledgeOptions>().Any(p => p.ProjectId == projectId))
            {
                resultbool.ErrorCode = 2;
                resultbool.ErrorText = "this pledge option is already in this project";
                resultbool.Data = false;
                return resultbool;
            }

            //create new pledge option
            var newPledgeOption = new PledgeOptions();
            newPledgeOption.TitleOfPledge = titleOfPledge;
            newPledgeOption.PriceOfPlege = priceOfPledge;
            newPledgeOption.NumberOfAvailablePledges = numberOfAvailablePledges;
            newPledgeOption.ProjectId = projectId;
            newPledgeOption.NumberOfBacker = 0;
            newPledgeOption.EstimateDelivery = estimateDelivery;
            newPledgeOption.DurationOfPledge = durationOfPldege;
            newPledgeOption.Description = description;

            context.Add(newPledgeOption);
            context.SaveChanges();
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
                resultbool.Data = false;
                return resultbool;
            }
        }


        public Result<bool> AddProjectInfo(string email, int projectId, string title, string description, string fileName)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "This user can't modify this project";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the project has already this project info
            if (context.Set<ProjectInfo>().Any(pi => pi.Title == title)
                && context.Set<ProjectInfo>().Any(pi => pi.ProjectId == projectId))
            {
                resultbool.ErrorCode = 2;
                resultbool.ErrorText = "this project info is already in this project";
                resultbool.Data = false;
                return resultbool;
            }

            //create new pledge option
            var newProjectInfo = new ProjectInfo();
            newProjectInfo.Title = title;
            newProjectInfo.ProjectId = projectId;
            newProjectInfo.Description = description;
            newProjectInfo.FileName = fileName;

            context.Add(newProjectInfo);
            context.SaveChanges();
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
                resultbool.Data = false;
                return resultbool;
            }
        }


        public Result<bool> AutoProjectProgressUpdate()
        {
            throw new NotImplementedException();
        }

        public Result<bool> AutoProjectStatusUpdate()
        {
            throw new NotImplementedException();
        }

        public Result<bool> CreateProject(string email, double fundingBudjet)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteCategoryFromProject(string email, int ProjectId, string CategoryName)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeletePledgeOptionFromProject(string email, int ProjectId, int pledgeOptionsId)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteProject(string email, int projectId)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteProjectInfo(string email, int projectinfoId)
        {
            throw new NotImplementedException();
        }

        public Result<double> ProgressOfFunding(string email, int projectId)
        {
            throw new NotImplementedException();
        }

        public Result<List<Comment>> ProjectComments(int projectId)
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> SuccessfullProjects()
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdateCategoryOfProject(string email, int ProjectId, string CategoryName, string NewCategoryName)
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdatePledgeOptionOfProject(string email, int ProjectId, int pledgeOptionsId, string titleOfPledge, double priceOfPledge, DateTime estimateDelivery, DateTime durationOfPldege, int numberOfAvailablePledges, string description)
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdateProject(string email, int projectId, double fundingBudjet)
        {
            throw new NotImplementedException();
        }

        public Result<bool> UpdateProjectInfo(string email, int ProjectinfoId, string title, string description, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
