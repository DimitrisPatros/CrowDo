using System.Collections.Generic;

namespace CrowDo.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int ProjectCategoriesID { get; set; }
        public string CategoryName { get; set; }
    }
}