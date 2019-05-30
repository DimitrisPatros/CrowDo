using CrowDoServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices.Interfaces
{
    public interface ISearchServices
    {
        //main search
        Result<List<ProjectViewModel>> SearchProjects(string q);        
        Result<Project> SearchPoject(string title);
        Result<List<Project>> AvailableProjects();
        Result<List<Project>> FundedProjects();
        Result<List<Project>> RecentProjects();
        Result<List<Project>> MostVisitedProjects();
        Result<List<UserViewModel>> TopProjectCreators(int number);

        Result<List<Project>> LastWeekProjects();
        Result<List<Project>> LastMonthProjects();
        Result<List<Project>> ProjectByCategory(int categoryId);
        Result<List<Project>> ProjectByCreator(int userId);
        Result<List<Project>> MostFunded();
        Result<List<Project>> AlmostExpireProjects();
    }
}
