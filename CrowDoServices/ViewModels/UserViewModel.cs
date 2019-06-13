using DatabaseContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


        public UserViewModel(User user)
        {
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
        }
    }
}
