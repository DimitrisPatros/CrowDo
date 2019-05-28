using CrowDoCore.Interfaces;
using CrowDoCore.Models;
using CrowDoServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoCore.Services
{
    public class SearchServices : ISearchServices
    {
        public Result(Project) Result = new Result(Project);

        public Result<Project> SearchPoject(string title)
        {
            var context = new CrowDoDbContext();
           
                
                var project = context.Set<Project>()
                   .SingleOrDefault(p => p.ProjectTitle == title);

                var projectsList = new Result<Project>()
                {
                    Data = project
                };

                if (project == null)
                {
                    projectsList.ErrorCode = 0;
                    projectsList.ErrorText = "Project not found";

                    return projectsList;
                }

                if (context.SaveChanges() < 1)

                {
                    projectsList.ErrorCode = 0;
                    projectsList.ErrorText = "";

                    return projectsList;
                }
                return projectsList;
            
        }

        public Result<List<Project>> AvailableProjects()
        {
            var context = new CrowDoDbContext();
            {
                var availableProjectsList = new Result<List<Project>>();
                var availablelist = context.Set<Project>()
                    .Any(ap => ap.ProjectStatus == true);

                if (availablelist == false)
                {
                    availableProjectsList.ErrorCode = 0;
                    availableProjectsList.ErrorText = "No available projects";

                    return availableProjectsList;
                }

                return availableProjectsList;
            }
        }

        public Result<List<Project>> FundedProjects()
        {
            var context = new CrowDoDbContext();
            {
                var fundedProjectList = new Result<List<Project>>();
                var fundedlist = context.Set<Project>()
                    .Any(fd => fd.ProjectSuccess == true);

                if (fundedlist == false)
                {
                    fundedProjectList.ErrorCode = 0;
                    fundedProjectList.ErrorText = "No funded projects";

                    return fundedProjectList;
                }

                return fundedProjectList;
            }
        }

        public Result<List<Project>> RecentProjects()
        {
            var context = new CrowDoDbContext();
            {
                var recentListOfProjects = new Result<List<Project>>();
                var recentlist = context.Set<Project>()
                    .Where(rp => rp.CreationDate.AddDays(30) >= DateTime.Today)
                    .Where(rp => rp.ProjectStatus == true);

                if (recentlist == null)
                {
                    recentListOfProjects.ErrorCode = 0;
                    recentListOfProjects.ErrorText = "Recent list not found";

                    return recentListOfProjects;
                }

                return recentListOfProjects;


            }
        }

        public Result<List<Project>> MostVisitedProjects()
        {
            var context = new CrowDoDbContext();
            //{
            //    var mostVisitedProjectList = new Result<List<Project>>();
            //    var mostvisitedlist = context.Set<Project>()
            //        .OrderByDescending(mv => mv.Visits)
            //        .Take(10)
            //        .ToList();
            //}
            throw new NotImplementedException();
        }

        public Result<List<User>> TopProjectCreators(int number)
        {
            var context = new CrowDoDbContext();
            {
                //var topProjectCreators = new Result<List<User>>();
                //var topcreatorlist = context.Set<User>()
                //    .OrderByDescending(u => u.)
                //    .Take(20)
                //    .ToList();
            }
            throw new NotImplementedException();
        }

        public Result<List<Project>> LastWeekProjects()
        {
            var context = new CrowDoDbContext();
            {
                var lastWeekProjects = new Result<List<Project>>();
                var lastweekproject = context.Set<Project>()
                    .Where(w => w.CreationDate.AddDays(7) >= DateTime.Today);

                if (lastweekproject == null)
                {
                    lastWeekProjects.ErrorCode = 0;
                    lastWeekProjects.ErrorText = "Last week project not found";

                    return lastWeekProjects;

                }

                return lastWeekProjects;
            }
        }

        public Result<List<Project>> LastMonthProjects()
        {
            var context = new CrowDoDbContext();
            {
                var lastMonthProject = new Result<List<Project>>();
                var lastmonthproject = context.Set<Project>()
                    .Where(m => m.CreationDate.AddDays(30) >= DateTime.Today);

                if (lastmonthproject == null)
                {
                    lastMonthProject.ErrorCode = 0;
                    lastMonthProject.ErrorText = "Last month project not found";

                    return lastMonthProject;
                }

                return lastMonthProject;
            }
        }

        public Result<List<Project>> ProjectByCategory(string categoryName)
        {
            var context = new CrowDoDbContext();
            {
                var projectByCategory = new Result<List<Project>>();
                var projectbycategory = context.Set<ProjectCategories>()
                    .Where(pc => pc.Category.CategoryName == categoryName)
                    .ToList();

                if (projectbycategory == null)
                {
                    projectByCategory.ErrorCode = 0;
                    projectByCategory.ErrorText = "Not found";

                    return projectByCategory;
                }

                return projectByCategory;
            }
        }

        public Result<List<Project>> ProjectByCreator(string creatorName)
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> MostFunded()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> AlmostExpireProjects()
        {
            throw new NotImplementedException();
        }

    }
}
