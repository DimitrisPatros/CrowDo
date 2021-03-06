﻿using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowDoServices.Services
{
    public class SearchServices : ISearchServices
    {
        private CrowDoDbContext context;

        public SearchServices(CrowDoDbContext crowDoDbContext)
        {
            context = crowDoDbContext;

        }

        //search project by title
        public Result<Project> SearchPoject(string title)
        {
            var result = new Result<Project>();
            var project = context.Set<Project>().SingleOrDefault(p => p.ProjectTitle == title);

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

        //done
        public async Task<Result<List<Project>>> AvailableProjects()
        {

            var result = new  Result<List<Project>>();
            result.Data = await context.Set<Project>().Where(ap => ap.ProjectStatus == true).ToListAsync();

            if (result.Data.Count == 0)
            {
                result.ErrorCode = 0;
                result.ErrorText = "No available projects";

                return result;
            }
            return result;
        }
        //done
        public Result<List<Project>> FundedProjects()
        {

            var result = new Result<List<Project>>();
            var fundedlist = context.Set<Project>()
                .Where(fd => fd.ProjectSuccess == true).ToList();

            if (!fundedlist.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "No funded projects";

                return result;
            }
            result.Data = fundedlist;
            return result;
        }

        //done
        public Result<List<Project>> RecentProjects()
        {
            var result = new Result<List<Project>>();
            result.Data = context.Set<Project>()
                .Where(rp => rp.ProjectStatus == true && rp.CreationDate >= DateTime.Today.AddDays(-5)).ToList();
            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "Recent list not found";
            }
            return result;

        }

        //done
        public Result<List<Project>> MostVisitedProjects()
        {
            var result = new Result<List<Project>>();
            result.Data = context.Set<Project>()
                .OrderByDescending(mv => mv.ProjectViews)
                .Take(10)
                .ToList();
            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "Recent list not found";
            }
            return result;
        }

        //done
        public Result<List<UserViewModel>> TopProjectCreators(int number)
        {
            var resulttopusers = new Result<List<UserViewModel>>();
            var result = new List<User>();
            var users = context.Set<User>().Include(u => u.Projects);
            result = users.OrderByDescending(t => t.Projects.Where(p => p.ProjectSuccess).Count()).Take(number).ToList();

            var topUsers = new List<UserViewModel>();
            foreach (var u in result)
            {
                var topuser = new UserViewModel(u);
                topUsers.Add(topuser);
            }
            resulttopusers.Data = topUsers;
            if (resulttopusers.Data == null || !resulttopusers.Data.Any())
            {
                resulttopusers.ErrorCode = 0;
                resulttopusers.ErrorText = "Recent list not found";
            }
            return resulttopusers;
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private Result<List<Project>> LastnumberProjects(int number)
        {

            var result = new Result<List<Project>>();
            result.Data = context.Set<Project>()
               .Where(w => w.CreationDate > DateTime.Now.AddDays(-number))
               .ToList();

            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 15;
                result.ErrorText = "no projects";
            }
            return result;
        }

        public Result<List<Project>> LastWeekProjects()
        {
            return LastnumberProjects(7);
        }

        public Result<List<Project>> LastMonthProjects()
        {
            return LastnumberProjects(30);
        }

        public Result<List<Project>> AlmostExpireProjects()
        {
            return LastnumberProjects(28);
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        public Result<List<Project>> ProjectByCategory(int categoryId)
        {
            var result = new Result<List<Project>>();
            var queryData = context.Set<ProjectCategories>().Include(pc => pc.Project)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            result.Data = new List<Project>();


            var projectsids = new List<int>();


            foreach (var projectCategory in queryData)
            {
                projectsids.Add(projectCategory.ProjectId);
            }

            foreach (var pi in projectsids)
            {
                result.Data.Add(context.Project.SingleOrDefault(p => p.ProjectId == pi));
            }
            //foreach (var projectCategory in queryData)
            //{
            //    result.Data.Add(projectCategory.Project);
            //}
            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "Not found";                
            }
            return result;
        }
        //done
        public Result<List<Project>> ProjectByCreator(int userId)
        {
            var result = new Result<List<Project>>();
            var queryData = context.Set<User>().Include(u => u.Projects).SingleOrDefault(u => u.UserId == userId);
            result.Data = queryData.Projects;

            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "Not found";
            }
            return result;
        }

        public Result<List<ProjectViewModel>> SearchProjects(string q)
        {
            Result<List<ProjectViewModel>> SearchResult = new Result<List<ProjectViewModel>>();
            List<Project> projects = context.Project.Include(project => project.ProjectCategories)
                                    .ThenInclude(projectCategories => projectCategories.Category)
                                    .Where(p => p.ProjectTitle.Contains(q)
                                    ||
                                    p.ProjectCategories.Any(pc => pc.Category.CategoryName.Contains(q))).ToList();
            SearchResult.Data = new List<ProjectViewModel>();
            foreach (var p in projects)
            {
                SearchResult.Data.Add(new ProjectViewModel(p));
            }
            return SearchResult;
        }
        public Result<List<Project>> MostFunded()
        {
            var result = new Result<List<Project>>();
            result.Data = context.Project.OrderByDescending(p => p.PledgeProgress).ToList();
            return ReturnData(result);
        }

        private Result<List<T>> ReturnData<T>(Result<List<T>> result)
        {
            if (result.Data == null || !result.Data.Any())
            {
                result.ErrorCode = 0;
                result.ErrorText = "Not found";
            }
            return result;
        }
    }
}