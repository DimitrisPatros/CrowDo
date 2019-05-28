using CrowDoServices.Interfaces;
using CrowDoServices.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoServices.Services
{
    public class ProjectServices : IProjectServices
    {
        private CrowDoDbContext context;

        public ProjectServices(CrowDoDbContext crowDoDbContext)
        {
            context = crowDoDbContext;

        }

        public Result<bool> resultbool = new Result<bool>();
        public Result<List<string>> resultList = new Result<List<string>>();

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
        public bool IfProjectExist(int projectId)
        {
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if (project == null)
            {
                return false;
            }
            return true;
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

            //chek if the projce exist
            if (!IfProjectExist(projectId))
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
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
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

        //done
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

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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


            //cheking if the project has already this pledge option
            var projectPledgeOptions = context.Set<PledgeOptions>().Where(po => po.ProjectId == projectId).ToList();
            if (projectPledgeOptions.Any(pc => pc.TitleOfPledge == titleOfPledge))
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

        //done
        public Result<bool> AddProjectInfo(string email, int projectId,
                      string title, string description, string fileName)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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

            //cheking if the project has already this project info
            if (context.Set<ProjectInfo>().Any(pi => pi.Title == title)
                && context.Set<ProjectInfo>().Any(pi => pi.ProjectId == projectId))
            {
                resultbool.ErrorCode = 2;
                resultbool.ErrorText = "this project info is already in this project";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the project has already this pledge option
            var InfoOptions = context.Set<ProjectInfo>().Where(pi => pi.ProjectId == projectId).ToList();
            if (InfoOptions.Any(pc => pc.Title == title))
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

        //done
        public Result<bool> AutoProjectStatusUpdate()
        {
            var projectList = context.Set<Project>().Where(pl => pl.CreationDate.AddDays(30) < DateTime.Today).ToList();

            foreach (var p in projectList)
            {
                p.ProjectStatus = false;
            }
            if (context.SaveChanges() >= projectList.Count())
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
 
        //done
        public Result<bool> CreateProject(string email, string projectTitle, double fundingBudjet)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet title exist
            if (context.Set<Project>().Any(p => p.ProjectTitle == projectTitle))
            {
                resultbool.ErrorCode = 9;
                resultbool.ErrorText = "already project with this name";
                resultbool.Data = false;
                return resultbool;
            }
            //chek if the user exist
            var user = context.Set<User>().SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                resultbool.ErrorCode = 10;
                resultbool.ErrorText = "user didnt exist";
                resultbool.Data = false;
                return resultbool;
            }

            var project = new Project();
            project.UserId = user.UserId;
            project.ProjectTitle = projectTitle;
            project.PledgeOfFunding = fundingBudjet;
            project.ProjectStatus = true;
            project.CreationDate = DateTime.Now;
            context.Add(project);
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

        //done
        public Result<bool> DeleteCategoryFromProject(string email, int projectId, string categoryName)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projce exist
            if (!IfProjectExist(projectId))
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

            //cheking if the project has this category
            var projectCategory = context.Set<ProjectCategories>().Where(p => p.ProjectId == projectId).ToList();
            if (!projectCategory.Any(pc => pc.Category.CategoryName == categoryName))
            {
                resultbool.ErrorCode = 11;
                resultbool.ErrorText = "category doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }

            context.Remove(projectCategory.SingleOrDefault(pc => pc.Category.CategoryName == categoryName));

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

        //done
        public Result<bool> DeletePledgeOptionFromProject(string email, int projectId, int pledgeOptionsId)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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

            //check if the pledge exist
            var pledge = context.Set<PledgeOptions>().SingleOrDefault(po => po.PledgeOptionsId == pledgeOptionsId);
            if (pledge == null)
            {
                resultbool.ErrorCode = 12;
                resultbool.ErrorText = "pledge doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }

            context.Remove(pledge);

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

        //done
        public Result<bool> DeleteProject(string email, int projectId)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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

            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            project.ProjectStatus = false;
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

        //done
        public Result<bool> DeleteProjectInfo(string email, int projectinfoId)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }
            var projectInfo = context.Set<ProjectInfo>().SingleOrDefault(pi => pi.ProjectInfoId == projectinfoId);
            //chek if the projcet exist
            if (!IfProjectExist(projectInfo.ProjectId))
            {
                resultbool.ErrorCode = 8;
                resultbool.ErrorText = "projet doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectInfo.ProjectId))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "This user can't modify this project";
                resultbool.Data = false;
                return resultbool;
            }

            context.Remove(projectInfo);
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

        //done
        public Result<bool> ProgressOfFunding(int projectId)
        {
            //chek if the projcet exist
            if (!IfProjectExist(projectId))
            {
                resultbool.ErrorCode = 8;
                resultbool.ErrorText = "projet doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if(project.ProjectStatus==false)
            {
                resultbool.ErrorCode = 8;
                resultbool.ErrorText = "projet is inactive";
                resultbool.Data = false;
                return resultbool;
            }


            if (project.PledgeProgress >= project.PledgeOfFunding)
            {
                project.ProjectSuccess = true;
            }
            project.ProjectSuccess = false;
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

        //done
        public Result<List<string>> ProjectComments(int projectId)
        {
            if (!IfProjectExist(projectId))
            {
                resultList.ErrorCode = 8;
                resultList.ErrorText = "projet doesn’t exist";
                return resultList;
            }
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if (project.ProjectStatus == false)
            {
                resultList.ErrorCode = 8;
                resultList.ErrorText = "projet is inactive";
                return resultList;
            }
            var commentList = context.Set<Comment>().Where(c => c.ProjectId == projectId).ToList();
            var comments = new List<string>();
            foreach (var c in commentList)
            {
                comments.Add(c.CommentText);
            }
            if (comments == null)
            {
                resultList.ErrorCode = 13;
                resultList.ErrorText = "no comment";
                return resultList;
            }
            resultList.ErrorCode = 0;
            resultList.ErrorText = "successfull";
            resultList.Data = comments;
            return resultList;
        }

        //done
        public Result<bool> UpdatePledgeOptionOfProject(string email, int projectId, int pledgeOptionsId, string titleOfPledge, double priceOfPledge, DateTime estimateDelivery, DateTime durationOfPldege, int numberOfAvailablePledges, string description)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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


            //cheking if the project has already this pledge option
            var projectPledgeOptions = context.Set<PledgeOptions>().Where(po => po.ProjectId == projectId).ToList();
            var pledgeOption = projectPledgeOptions.SingleOrDefault(pc => pc.TitleOfPledge == titleOfPledge);
            if (pledgeOption == null)
            {
                resultbool.ErrorCode = 12;
                resultbool.ErrorText = "no pledge option with this id";
                resultbool.Data = false;
                return resultbool;
            }

            pledgeOption.TitleOfPledge = titleOfPledge;
            pledgeOption.PriceOfPlege = priceOfPledge;
            pledgeOption.NumberOfAvailablePledges = numberOfAvailablePledges;
            pledgeOption.EstimateDelivery = estimateDelivery;
            pledgeOption.DurationOfPledge = durationOfPldege;
            pledgeOption.Description = description;

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

        
        public Result<bool> UpdateProject(string email, int projectId,string title, bool status)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }

            //chek if the projcet exist
            if (!IfProjectExist(projectId))
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


            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            project.ProjectTitle = title;
            if (context.Set<Project>().Any(p => p.ProjectTitle == title))
            {
                resultbool.ErrorCode = 9;
                resultbool.ErrorText = "already project with this name";
                resultbool.Data = false;
                return resultbool;
            }
            project.ProjectStatus=status;
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

        public Result<bool> UpdateProjectInfo(string email, int projectinfoId, string title, string description, string filePath)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                resultbool.ErrorCode = 1;
                resultbool.ErrorText = "not valid email";
                resultbool.Data = false;
                return resultbool;
            }
            var projectInfo = context.Set<ProjectInfo>().SingleOrDefault(pi => pi.ProjectInfoId == projectinfoId);
            //chek if the projcet exist
            if (!IfProjectExist(projectInfo.ProjectId))
            {
                resultbool.ErrorCode = 8;
                resultbool.ErrorText = "projet doesn’t exist";
                resultbool.Data = false;
                return resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectInfo.ProjectId))
            {
                resultbool.ErrorCode = 3;
                resultbool.ErrorText = "This user can't modify this project";
                resultbool.Data = false;
                return resultbool;
            }

            projectInfo.Title = title;
            projectInfo.Description = description;
            projectInfo.FileName = filePath;
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
    }
}


////this method can be used to save a collum in the project table
//public Result<bool> AutoProjectProgressUpdate()
//{
//    var projectList = context.Set<Project>().Where(pl => pl.ProjectStatus==true).ToList();
//    //run for all the active project
//    foreach (var p in projectList)
//    {
//        //bring a list with all pledge option of this project
//        var pledgeOptionCount = context.Set<PledgeOptions>().Where(po => po.ProjectId == p.ProjectId).ToList();

//        foreach (var po in pledgeOptionCount)
//        {
//            p.PledgeProgress += po.PriceOfPlege * po.NumberOfBacker;
//        }
//    }
//    if (context.SaveChanges() >= projectList.Count())
//    {
//        resultbool.ErrorCode = 0;
//        resultbool.ErrorText = "Successfull";
//        resultbool.Data = true;
//        return resultbool;
//    }
//    else
//    {
//        resultbool.ErrorCode = 4;
//        resultbool.ErrorText = "couldnt save in db";
//        resultbool.Data = false;
//        return resultbool;
//    }
//}