using System;


namespace CrowDo.Models
{
    public class Comments
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        public string CommentText { get; set; }
        public DateTime DateOfComment { get; set; }
    }
}