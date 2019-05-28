using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using CrowDoServices.Services;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrowDoServices
{
    public class ExcelSerializer : ISerializer
    {
        public List<Project> LastWeekProjects = new List<Project>();
        public Project ReadFromFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool SaveToFile(string fileName, Project project)
        {

            XSSFWorkbook wb = new XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = wb.CreateSheet("My Sheet");
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("ProjectId");
            row.CreateCell(1).SetCellValue("UserId");
            row.CreateCell(2).SetCellValue("ProjectTitle");
            row.CreateCell(3).SetCellValue("PledgeOfFunding");
            row.CreateCell(4).SetCellValue("PledgeProgress");
            row.CreateCell(5).SetCellValue("ProjectStatus");
            row.CreateCell(6).SetCellValue("ProjectSuccess");
            row.CreateCell(7).SetCellValue("ProjectViews");
            row.CreateCell(8).SetCellValue("ProjectInfo");
            row.CreateCell(9).SetCellValue("PledgeOptions");
            row.CreateCell(10).SetCellValue("CreationDate");

            for (int i = 0; i < LastWeekProjects.Count; i++)
            {

            }

            try
            {
                using (var fs = new FileStream($@"{fileName}.xlsx", 
                    FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
