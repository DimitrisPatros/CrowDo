using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool ProjectSuccess { get; set; }
        public int ProjectViews { get; set; }
        public List<ProjectInfo> ProjectInfo { get; set; }
        public List<PledgeOptions> PledgeOptions { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
