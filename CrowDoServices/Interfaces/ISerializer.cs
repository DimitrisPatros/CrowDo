using System;
using System.Collections.Generic;
using System.Text;
using CrowDoServices.Models;
using CrowDoServices.Services;

namespace CrowDoServices.Interfaces
{
    public interface ISerializer
    {
        bool SaveToFile(string fileName, Project project);
        Project ReadFromFile(string fileName);
    }
}
