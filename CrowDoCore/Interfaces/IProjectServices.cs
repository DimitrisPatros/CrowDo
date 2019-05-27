using CrowDoCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoCore.Interfaces
{
    public interface IProjectServices
    {
        //Project Create Update Delete
        Result<bool> CreateProject(string email,double fundingBudjet);
        Result<bool> DeleteProject(string email, int projectId);
        Result<bool> UpdateProject(string email,int projectId, double fundingBudjet);
        Result<double> ProgressOfFunding(string email, int projectId);

        //Projectinfo Create Update Delete
        Result<bool> AddProjectInfo(string email, int ProjectId,string title,string description,string filePath);
        Result<bool> DeleteProjectInfo(string email,int projectinfoId);
        Result<bool> UpdateProjectInfo(string email, int ProjectinfoId, string title, string description, string filePath);
        
        //ProjectCategories Create Update Delete
        Result<bool> AddCategoryToProject(string email, int ProjectId, string CategoryName);
        Result<bool> DeleteCategoryFromProject(string email, int ProjectId, string CategoryName);
        Result<bool> UpdateCategoryOfProject(string email, int ProjectId, string CategoryName,string NewCategoryName);

        //PledgeOptions Create Update Delete
        Result<bool> AddPledgeOptionToProject(string email, int ProjectId, string titleOfPledge,
                      double priceOfPledge, DateTime estimateDelivery, DateTime durationOfPldege,
                                                int numberOfAvailablePledges, string description);
        Result<bool> DeletePledgeOptionFromProject(string email, int ProjectId,int pledgeOptionsId);
        Result<bool> UpdatePledgeOptionOfProject(string email, int ProjectId, int pledgeOptionsId,
                             string titleOfPledge, double priceOfPledge,DateTime estimateDelivery,
                             DateTime durationOfPldege,int numberOfAvailablePledges,string description);

        Result<List<Comment>> ProjectComments(int projectId);
        Result<bool> AutoProjectStatusUpdate();
        Result<bool> AutoProjectProgressUpdate();
        Result<List<Project>> SuccessfullProjects();
    }
}
