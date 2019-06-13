using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseContext.Models
{
    public class SearchObject
    {
        public string Title { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? FundedProjects { get; set; }
        public bool?  ActiveOrInactiveProjects { get; set; }
        public bool? EndingSoon { get; set; }
    }
}
