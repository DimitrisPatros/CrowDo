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

       

        public struct UpdateUser
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Address { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string newEmail { get; set; }
        }


        public struct PledgeCreate
        {
            public string Email { get; set; }
            public int pledgeOptionId { get; set; }
        }


        [HttpPost("/RegisterUser")]
        public IActionResult RegisterUser([FromBody] NewUser temp)
        {
            var result =  userService.UserRegister(temp.Email, temp.Name, temp.Surname, temp.Address
                , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth);
            return Ok(result);
        }

        [HttpPut("/DeleteUser")]
        public IActionResult UserDelete([FromBody] string email)
        {
            var result = userService.UserDelete(email);
            return Ok(result);
        }


        [HttpPut("/UpdateUser")]
        public IActionResult UserUpdate([FromBody] UpdateUser temp)
        {
            var result = userService.UserUpdate(temp.Email, temp.Name, temp.Surname, temp.Address
                , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth, temp.newEmail);
            return Ok(result);
        }

        [HttpPost("/CreatePledge")]
        public IActionResult CreatePledge([FromBody] PledgeCreate temp)
        {
            var result = userService.CreatePledge(temp.Email, temp.pledgeOptionId);
            return Ok(result);
        }

        [HttpPost("/MyPledges")]
        public IActionResult MyPledges([FromBody] string email)
        {
            var result = userService.MyPledges(email);
            return Ok(result);
        }

        [HttpPost("/BackedProjects")]
        public IActionResult BackedProjects([FromBody] string email)
        {
            var result = userService.BackedProjects(email);
            return Ok(result);
        }

        [HttpPost("/MyProjects")]
        public IActionResult MyProjects([FromBody] string email)
        {
            var result = userService.MyProjects(email);
            return Ok(result);
        }

        [HttpPost("/UserComments")]
        public IActionResult UserComments([FromBody] string email)
        {
            var result = userService.UserComments(email);
            return Ok(result);
        }
    }
}
