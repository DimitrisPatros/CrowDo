using CrowDoServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices.Interfaces
{
    public interface ISerializer
    {
        bool SaveToFile(string fileName, List<Project> project);
        List<User> ReadFromFileUSers(string fileName);
    }
}
