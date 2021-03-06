﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CrowDoServices.Models
{
    public class ProjectCategories
    {
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        public Project Project { get; set; }
        public Category Category { get; set; }
    }
}