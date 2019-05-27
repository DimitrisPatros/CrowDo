using CrowDoCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoCore.Interfaces
{
    interface ISearchServices
    {
        Result<Project> SearchPoject(string title);
        Result<List<Project>> AvailableProjects();
        Result<List<Project>> FundedProjects();
        Result<List<Project>> RecentProjects();
        Result<List<Project>> MostVisitedProjects();
        Result<List<User>> TopProjectCreators(int number);
        //Must do mpliax
        Result<List<Project>> LastWeekProjects();
        Result<List<Project>> LastMonthProjects();
        Result<List<Project>> ProjectByCategory(string categoryName);
        Result<List<Project>> ProjectByCreator(string creatorName);
        Result<List<Project>> MpstFunder();
        Result<List<Project>> AlmostExpireProjects();
    }
}
