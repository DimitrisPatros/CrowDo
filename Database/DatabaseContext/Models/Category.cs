﻿using System.Collections.Generic;

namespace DatabaseContext.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        //public int ProjectCategoriesID { get; set; }
        public string CategoryName { get; set; }
        public List<ProjectCategories> ProjectCategories { get; set; }
    }
}