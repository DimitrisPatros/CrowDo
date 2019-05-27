using CrowDo.Interfaces;
using CrowDo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDo.services
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

        public ProjectServices()
        {
        }
        private CrowDoDbContext _context = new CrowDoDbContext();
        private Result<bool> _resultbool = new Result<bool>();


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
            var user = _context.Set<User>().SingleOrDefault(u => u.Email == email);
            Project project = _context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId);
            if (user.UserId == project.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //done
        public Result<bool> AddCategoryToProject(string email, int projectId, string categoryName)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                _resultbool.ErrorCode = 1;
                _resultbool.ErrorText = "not valid email";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the project has already this category
            if (_context.Set<ProjectCategories>().Any(p => p.ProjectId == projectId)
                && _context.Set<ProjectCategories>().Any(p => p.Category.CategoryName == categoryName))
            {
                _resultbool.ErrorCode = 2;
                _resultbool.ErrorText = "the project has already this category";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                _resultbool.ErrorCode = 3;
                _resultbool.ErrorText = "This user can't modify this project";
                _resultbool.Data = false;
                return _resultbool;
            }

            //create the new category
            var newCategory = new Category()
            {
                CategoryName = categoryName,
                ProjectCategoriesID = projectId
            };
            _context.Add(newCategory);
            _context.SaveChanges();
            if (_context.SaveChanges() < 1)
            {
                _resultbool.ErrorCode = 4;
                _resultbool.ErrorText = "couldnt save in db";
                _resultbool.Data = false;
                return _resultbool;
            }

            //create the projectcategory
            var projectCategory = new ProjectCategories()
            {
                ProjectId = projectId,
                CategoryId = newCategory.CategoryId,
                Project = _context.Set<Project>().SingleOrDefault(p => p.ProjectId == projectId),
                Category = newCategory
            };
            _context.Add(projectCategory);
            if (_context.SaveChanges() >= 1)
            {
                _resultbool.ErrorCode = 0;
                _resultbool.ErrorText = "Successfull";
                _resultbool.Data = true;
                return _resultbool;
            }
            else
            {
                _resultbool.ErrorCode = 4;
                _resultbool.ErrorText = "couldnt save in db";
                _resultbool.Data = false;
                return _resultbool;
            }
        }

        //done
        public Result<bool> AddPledgeOptionToProject(string email, int projectId,
            string titleOfPledge,double priceOfPledge, DateTime estimateDelivery,
            DateTime durationOfPldege,int numberOfAvailablePledges, string description)
        {
            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                _resultbool.ErrorCode = 1;
                _resultbool.ErrorText = "not valid email";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                _resultbool.ErrorCode = 3;
                _resultbool.ErrorText = "This user can't modify this project";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the project has already this pledge option
            if (_context.Set<PledgeOptions>().Any(po => po.TitleOfPledge == titleOfPledge)
                && _context.Set<PledgeOptions>().Any(p => p.ProjectId == projectId))
            {
                _resultbool.ErrorCode = 2;
                _resultbool.ErrorText = "this pledge option is already in this project";
                _resultbool.Data = false;
                return _resultbool;
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

            _context.Add(newPledgeOption);
            _context.SaveChanges();
            if (_context.SaveChanges() >= 1)
            {
                _resultbool.ErrorCode = 0;
                _resultbool.ErrorText = "Successfull";
                _resultbool.Data = true;
                return _resultbool;
            }
            else
            {
                _resultbool.ErrorCode = 4;
                _resultbool.ErrorText = "couldnt save in db";
                _resultbool.Data = false;
                return _resultbool;
            }
        }

        //done
        public Result<bool> AddProjectInfo(string email, int projectId, string title, string description, string fileName)
        {

            //cheking if the email is valid
            if (!IsvalidEmail(email))
            {
                _resultbool.ErrorCode = 1;
                _resultbool.ErrorText = "not valid email";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the specific user can modify this projet
            if (!IsValidateUser(email, projectId))
            {
                _resultbool.ErrorCode = 3;
                _resultbool.ErrorText = "This user can't modify this project";
                _resultbool.Data = false;
                return _resultbool;
            }

            //cheking if the project has already this project info
            if (_context.Set<ProjectInfo>().Any(pi => pi.Title == title)
                && _context.Set<ProjectInfo>().Any(pi => pi.ProjectId == projectId))
            {
                _resultbool.ErrorCode = 2;
                _resultbool.ErrorText = "this project info is already in this project";
                _resultbool.Data = false;
                return _resultbool;
            }

            //create new pledge option
            var newProjectInfo = new ProjectInfo();
            newProjectInfo.Title= title;
            newProjectInfo.ProjectId = projectId;
            newProjectInfo.Description= description;
            newProjectInfo.FileName = fileName;

            _context.Add(newProjectInfo);
            _context.SaveChanges();
            if (_context.SaveChanges() >= 1)
            {
                _resultbool.ErrorCode = 0;
                _resultbool.ErrorText = "Successfull";
                _resultbool.Data = true;
                return _resultbool;
            }
            else
            {
                _resultbool.ErrorCode = 4;
                _resultbool.ErrorText = "couldnt save in db";
                _resultbool.Data = false;
                return _resultbool;
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
