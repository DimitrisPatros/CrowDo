using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices.Services
{

    public class SearchServices : ISearchServices
    {
        public CrowDoDbContext context;
        public SearchServices(CrowDoDbContext contexts)
        {
            context = contexts;
        }
        public Result<List<Project>> AlmostExpireProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> AvailableProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> FundedProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> LastMonthProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> LastWeekProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> MostFunded()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> MostVisitedProjects()
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> ProjectByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> ProjectByCreator(string creatorName)
        {
            throw new NotImplementedException();
        }

        public Result<List<Project>> RecentProjects()
        {
            throw new NotImplementedException();
        }

        public Result<Project> SearchPoject(string title)
        {
            throw new NotImplementedException();
        }

        public Result<List<User>> TopProjectCreators(int number)
        {
            throw new NotImplementedException();
        }
    }
}
