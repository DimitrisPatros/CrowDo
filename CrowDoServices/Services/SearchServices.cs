using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowDoServices.Services
{

    public class SearchServices : ISearchServices
    {
        public Result<Project> SearchPoject(string title)
        {
            var context = new CrowDoDbContext();

            var result = new Result<Project>();
            var project = context.Set<Project>()
                .SingleOrDefault(p => p.ProjectTitle == title);

            project.ProjectViews++;

            if (project == null)
            {
                result.ErrorCode = 0;
                result.ErrorText = "Project not found";

                return result;
            }

            if (context.SaveChanges() < 1)

            {
                result.ErrorCode = 0;
                result.ErrorText = "";

                return result;
            }

            return result;
        }

        public Result<List<Project>> AvailableProjects()
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var availablelist = context.Set<Project>()
                .Any(ap => ap.ProjectStatus == true);

            if (availablelist == false)
            {
                result.ErrorCode = 0;
                result.ErrorText = "No available projects";

                return result;
            }

            return result;
        }

        public Result<List<Project>> FundedProjects()
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var fundedlist = context.Set<Project>()
                .Any(fd => fd.ProjectSuccess == true);

            if (fundedlist == false)
            {
                result.ErrorCode = 0;
                result.ErrorText = "No funded projects";

                return result;
            }

            return result;
        }

        public Result<List<Project>> RecentProjects()
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var recentlist = context.Set<Project>()
                .Where(rp => rp.CreationDate.AddDays(30) >= DateTime.Today)
                .Where(rp => rp.ProjectStatus == true);

            if (recentlist == null)
            {
                result.ErrorCode = 0;
                result.ErrorText = "Recent list not found";

                return result;
            }

            return result;

        }

        public Result<List<Project>> MostVisitedProjects()
        {
            var context = new CrowDoDbContext();
            {
                var result = new Result<List<Project>>();
                var mostvisitedlist = context.Set<Project>()
                    .OrderByDescending(mv => mv.ProjectViews)
                    .Take(10)
                    .ToList();

                if (context.SaveChanges() < 1)

                {
                    result.ErrorCode = 0;
                    result.ErrorText = "";

                    return result;
                }

                return result;
            }            
        }

        public Result<List<User>> TopProjectCreators(int number)
        {
            var context = new CrowDoDbContext();

            //var result = new Result<List<User>>();
            //var topcreatorlist = context.Set<User>()
            //    .OrderByDescending(u => u.)
            //    .Take(20)
            //    .ToList();

            throw new NotImplementedException();
        }

        public Result<List<Project>> LastWeekProjects()
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var lastweekproject = context.Set<Project>()
                .Where(w => w.CreationDate.AddDays(7) >= DateTime.Today)
                .ToList();

            if (lastweekproject == null)
            {
                result.ErrorCode = 0;
                result.ErrorText = "Last week project not found";

                return result;
            }         

            return result;
        }

        public Result<List<Project>> LastMonthProjects()
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var lastmonthproject = context.Set<Project>()
                .Where(m => m.CreationDate.AddDays(30) >= DateTime.Today);

            if (lastmonthproject == null)
            {
                result.ErrorCode = 0;
                result.ErrorText = "Last month project not found";

                return result;
            }

            return result;
        }

        public Result<List<Project>> ProjectByCategory(string categoryName)
        {
            var context = new CrowDoDbContext();

            var result = new Result<List<Project>>();
            var projectbycategory = context.Set<ProjectCategories>()
                .Where(pc => pc.Category.CategoryName == categoryName)
                .ToList();

            if (projectbycategory == null)
            {
                result.ErrorCode = 0;
                result.ErrorText = "Not found";

                return result;
            }

            return result;
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
