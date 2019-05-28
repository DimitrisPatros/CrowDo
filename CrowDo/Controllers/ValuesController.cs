using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CrowDoServices.Services;
using CrowDoServices;
using CrowDoServices.Models;

namespace CrowDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IProjectServices projectService;
        private ISearchServices searchService;
        private IUserServices userService;
        public ValuesController(IProjectServices projectServices, ISearchServices searchServices, IUserServices userServices)
        {
            projectService = projectServices;
            searchService = searchServices;
            userService = userServices;
        }

        public struct NewUser
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Address { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        [HttpPost("/RegisterUser")]
        public ActionResult<Result<User>> RegisterUser([FromBody] NewUser temp)
        {
            return userService.UserRegister(temp.Email, temp.Name, temp.Surname, temp.Address
                , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth);
        }

    }
}
