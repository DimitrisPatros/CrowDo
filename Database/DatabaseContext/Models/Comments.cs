using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseContext.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        public string CommentText { get; set; }
        public DateTime DateOfComment { get; set; }
    }
}