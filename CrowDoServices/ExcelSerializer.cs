using CrowDoServices.Interfaces;
using CrowDoServices.Models;
using CrowDoServices.Services;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace CrowDoServices
{
    public class ExcelSerializer : ISerializer
    {

        public List<Project> projects = new List<Project>();

        public bool SaveToFile(string fileName, List<Project> project)
        {
            XSSFWorkbook wb = new XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = wb.CreateSheet("My Sheet");

            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("ProjectId");
            row.CreateCell(1).SetCellValue("UserId");
            row.CreateCell(2).SetCellValue("Title");
            row.CreateCell(3).SetCellValue("PledgeOfFunding");
            row.CreateCell(4).SetCellValue("Progress");
            row.CreateCell(5).SetCellValue("Status");
            row.CreateCell(6).SetCellValue("Success");
            row.CreateCell(7).SetCellValue("Views");
            row.CreateCell(8).SetCellValue("CreationDate");
            row.CreateCell(9).SetCellValue("NumbeOfComments");
            row.CreateCell(10).SetCellValue("NumberOfProjectInfo");
            row.CreateCell(11).SetCellValue("NumberOfPledgeOpitons");

            for (int i = 0; i < projects.Count; i++)
            {
                row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(projects[i].ProjectId);
                row.CreateCell(1).SetCellValue(projects[i].UserId);
                row.CreateCell(2).SetCellValue(projects[i].ProjectTitle);
                row.CreateCell(3).SetCellValue(projects[i].PledgeOfFunding);
                row.CreateCell(4).SetCellValue(projects[i].PledgeProgress);
                row.CreateCell(5).SetCellValue(projects[i].ProjectStatus);
                row.CreateCell(6).SetCellValue(projects[i].ProjectSuccess);
                row.CreateCell(7).SetCellValue(projects[i].ProjectViews);
                row.CreateCell(8).SetCellValue(projects[i].CreationDate);
                row.CreateCell(9).SetCellValue(projects[i].comments.Count);
                row.CreateCell(10).SetCellValue(projects[i].ProjectInfo.Count);
                row.CreateCell(11).SetCellValue(projects[i].PledgeOptions.Count);
            }


            using (var fs = new FileStream($@"{fileName}.xlsx",
                FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }
            return true;
        }

        public List<User> ReadFromFileUSers(string fileName)
        {
            var users = new List<User>();
            var Jusers = new List<Jproject>();
            string data = File.ReadAllText($@"{fileName}.json");
            Jusers = JsonConvert.DeserializeObject<List<Jproject>>(data);

            foreach (var ju in Jusers)
            {
                var tempUser = new User();
                tempUser.Email = ju.Email;
                tempUser.Address = ju.Address;
                tempUser.Name = ju.Name;
                tempUser.DateOfBirth = DateTime.Now.AddYears(-20);
                users.Add(tempUser);
            }
            return users;
        }

        //public bool ReadFromFileProject(string fileName)
        //{
        //    var projects = new List<Project>();
        //    var jprojects = new List<Jproject>();
        //    string data = File.ReadAllText($@"{fileName}.json");
        //    jprojects = JsonConvert.DeserializeObject<List<Jproject>>(data);

        //    foreach (var Jp in jprojects)
        //    {
        //        var temptproject = new Project();
        //        temptproject.Email = Jp.Email;
        //        temptproject.Address = Jp.Address;
        //        temptproject.Name = Jp.Name;
        //        temptproject.DateOfBirth = DateTime.Now.AddYears(-20);
        //        projects.Add(temptproject);
        //    }

        //    foreach (var u in projects)
        //    {
        //        userServices.UserRegister(u.Email, u.Name, u.Surname, u.Address,
        //            u.Country, u.State, u.ZipCode, u.DateOfBirth);
        //    }
        //    return true;
        //}









    }
    public class Jproject
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string personalMoto { get; set; }
    }

}
