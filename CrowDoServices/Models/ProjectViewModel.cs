using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double RequestBudjet { get; set; }//name change
        public double Progress { get; set; }
        public bool Status { get; set; }
        public bool Success { get; set; }
        public int visits { get; set; }
        public List<ProjectInfo> Info { get; set; }
        public List<PledgeOptions> AvailablePledges { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ProjectCategories> Categories { get; set; }
        public User Creator { get; set; }
        public ProjectViewModel(Project project)
        {
            Id = project.ProjectId;
            Title = project.ProjectTitle;
            Status = project.ProjectStatus;
            Success = project.ProjectSuccess;
            AvailablePledges = project.PledgeOptions;
            Categories = project.ProjectCategories;
            visits = project.ProjectViews;
            Info = project.ProjectInfo;
            CreationDate = project.CreationDate;
            RequestBudjet = project.PledgeOfFunding;
            Progress = project.PledgeProgress;
        }
    }
}
