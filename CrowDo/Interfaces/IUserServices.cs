using CrowDo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo.Interfaces
{
    public interface IUserServices
    {
        //User Create Delete Update
        Result<User> UserRegister(string email, string name, string surname,
                               string address, string country, string state,
                               string zipcode, DateTime dateofbirth);
        Result<bool> UserDelete(string email);
        Result<bool> UserUpdate(string email, string name, string surname,
                               string address, string country, string state,
                               string zipcode, DateTime dateofbirth,string newEmail);
        //Pledge Create 
        Result<bool> CreatePledge(string email,int pledgeOptionId);


        Result<List<Comment>> UserComments(string email);
        Result<List<Project>> BackedProjects(string email);
        Result<List<Project>> MyProjects(string email);
        Result<List<Pledges>> MyPledges(string email);
    }
}
