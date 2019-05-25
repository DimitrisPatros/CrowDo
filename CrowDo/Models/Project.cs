using System;
using System.Collections.Generic;
using System.Text;


namespace CrowDo.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public double PledgeOfFunding { get; set; }//name change
        public double PledgeProgress { get; set; }
        public bool ProjectStatus { get; set; }
        public List<Comments> Comments { get; set; }
        public List<ProjectInfo> ProjectInfo { get; set; }
        public List<ProjectCategories> ProjectCategories { get; set; }
        public List<PledgeOptions> PledgeOptions { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
