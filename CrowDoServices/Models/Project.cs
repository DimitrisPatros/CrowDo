using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace CrowDoServices.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string ProjectTitle { get; set; }
        public double PledgeOfFunding { get; set; }
        public double PledgeProgress { get; set; }
        public bool ProjectStatus { get; set; }
        public bool ProjectSuccess { get; set; }      
        public int ProjectViews { get; set; }
        public List<ProjectInfo> ProjectInfo { get; set; }
        public List<PledgeOptions> PledgeOptions { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ProjectCategories> ProjectCategories { get; set; }
        public User User { get; set; }
        public List<Comment> comments { get; set; }
    }
}
