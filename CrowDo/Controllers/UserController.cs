using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CrowDoServices.Services;
using CrowDoServices;
using CrowDoServices.Models;
using System.ComponentModel.DataAnnotations;

namespace CrowDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IProjectServices projectService;
        private ISearchServices searchService;
        private IUserServices userService;
        private ISerializer serializer;
        public UserController(IProjectServices projectServices, ISearchServices searchServices,
            IUserServices userServices, ISerializer serializers)
        {
            projectService = projectServices;
            searchService = searchServices;
            userService = userServices;
            serializer = serializers;
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


        [HttpPost("/BackAProject/{PledgeId}/email/{email}")]
        public IActionResult BackAProject(int PledgeId, string email)
        {
            var result = userService.CreatePledge(email, PledgeId);
            return Ok(result);
        }



        /*-------------------------------------------------------Delete------------------------------------------------------------------*/
        [HttpPut("/DeleteUser/{email}")]
        public IActionResult UserDelete(string email)
        {
            var result = userService.UserDelete(email);
            return Ok(result);
        }
        [HttpPut("/UpdateUser/{email}")]
        public IActionResult UserUpdate(string email, [FromBody] NewUser temp)
        {
            var result = userService.UserUpdate(email, temp.Name, temp.Surname, temp.Address
                , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth, temp.Email);
            return Ok(result);
        }
        [HttpGet("/MyPledges{email}")]
        public IActionResult MyPledges(string email)
        {
            var result = userService.MyPledges(email);
            return Ok(result);
        }

        [HttpGet("/UserComments{email}")]
        public IActionResult UserComments(string email)
        {
            var result = userService.UserComments(email);
            return Ok(result);
        }

        [HttpGet("/MyProjects{email}")]
        public IActionResult MyProjects(string email)
        {
            var result = userService.MyProjects(email);
            return Ok(result);
        }
    }
}