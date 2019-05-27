﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CrowDoCore.Models
{
    public class Pledges
    {
        public int UserId { get; set; }
        public int PledgeOptionId { get; set; }
        public PledgeOptions PledgeOptions { get; set; }
        public User User { get; set; }
    }
}