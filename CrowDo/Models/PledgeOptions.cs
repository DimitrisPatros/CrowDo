using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowDo.Models
{
    public class PledgeOptions
    {
        public int PledgeOptionsId { get; set; }
        [ForeignKey("project")]
        public int ProjectId { get; set; }
        //public Project Project { get; set; }
        public string TitleOfPledge { get; set; }
        public int NumberOfBacker { get; set; }
        public double PriceOfPlege { get; set; }
        public DateTime EstimateDelivery { get; set; }
        public DateTime Duration { get; set; }
        public int NumberOfAvailablePledge { get; set; }
        public string Description { get; set; }
        public List<Pledges> Pledges { get; set; }
    }
}