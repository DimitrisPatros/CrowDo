using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowDo.Models

{
    public class ProjectInfo
    {
        public int ProjectInfoId { get; set; }
        public int ProjectId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string MemoryPath { get; set; }//pws apothikeuoume photos and videos
    }
}