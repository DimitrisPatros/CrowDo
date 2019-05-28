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
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct Category
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public string CategoryName { get; set; }
        }
        [HttpPost("/ AddCategoryToProject")]
        public IActionResult AddCategoryToProject([FromBody] Category newCategory)
        {
            var temp = projectService.AddCategoryToProject(newCategory.Email, newCategory.ProjectId, newCategory.CategoryName);
            return Ok(temp);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct NEwPledgeOptionToProject
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public string TitleOfPledge { get; set; }
            public double PriceOfPledge { get; set; }
            public DateTime EstimateDelivery { get; set; }
            public DateTime DurationOfPldege { get; set; }
            public int NumberOfAvailablePledges { get; set; }
            public string Descritpion { get; set; }
        }
        [HttpPost("/ AddPledgeOptionToProject")]
        public IActionResult AddPledgeOptionToProject([FromBody] NEwPledgeOptionToProject npo)
        {
            var temp = projectService.AddPledgeOptionToProject(npo.Email, npo.ProjectId,
            npo.TitleOfPledge, npo.PriceOfPledge, npo.EstimateDelivery,
            npo.DurationOfPldege, npo.NumberOfAvailablePledges, npo.Descritpion);
            return Ok(temp);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct NEwProjectInfo
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public string Title { get; set; }
            public string Filename { get; set; }
            public string Descritpion { get; set; }
        }

        [HttpPost("/ AddProjectInfo")]
        public IActionResult AddProjectInfo([FromBody] NEwProjectInfo npi)
        {
            var temp = projectService.AddProjectInfo(npi.Email, npi.ProjectId, npi.Title, npi.Descritpion, npi.Filename);
            return Ok(temp);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpPost("/ AutoProjectStatusUpdate")]
        public IActionResult AutoProjectStatusUpdate()
        {
            var temp = projectService.AutoProjectStatusUpdate();
            return Ok(temp);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct NEwProject
        {
            public string Email { get; set; }
            public string ProjectTitle { get; set; }
            public double FundingBudjet { get; set; }
        }

        [HttpPost("/  CreateProject")]
        public IActionResult CreateProject(NEwProject project)
        {
            var temp = projectService.CreateProject(project.Email, project.ProjectTitle, project.FundingBudjet);
            return Ok(temp);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpPut("/  DeleteCategoryFromProject")]
        public IActionResult DeleteCategoryFromProject(Category category)
        {
            var temp = projectService.DeleteCategoryFromProject(category.Email, category.ProjectId, category.CategoryName);
            return Ok(temp);
        }



        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct RemovePledge
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public int PledgeOptionsId { get; set; }
        }
        [HttpPut("/  DeletePledgeOptionFromProject")]
        public IActionResult DeletePledgeOptionFromProject(RemovePledge removePledge)
        {
            var temp = projectService.DeletePledgeOptionFromProject(removePledge.Email, removePledge.ProjectId, removePledge.PledgeOptionsId);
            return Ok(temp);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpPut("/DeleteProject")]
         public IActionResult DeleteProject(int ProjectId, string email)
        {
            var temp = projectService.DeleteProject(email, ProjectId);
            return Ok(temp);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpPut("/DeleteProjectInfo{ProjectInfoId}/User/{email}")]
        public IActionResult DeleteProjectInfo(int ProjectInfoId, string email)
        {
            var temp = projectService.DeleteProjectInfo(email, ProjectInfoId);
            return Ok(temp);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpPut("/ProgressOfFunding/{ProjectId}")]
        public IActionResult ProgressOfFunding(int ProjectId)
        {
            var temp = projectService.ProgressOfFunding(ProjectId);
            return Ok(temp);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet("/ProjectComments/{ProjectId}")]
        public IActionResult ProjectComments(int ProjectId)
        {
            var temp = projectService.ProjectComments(ProjectId);
            return Ok(temp);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public struct UpdatePledges
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public int PledgeOptionsId { get; set; }
            public string TitleOfPledge { get; set; }
            public double PriceOfPledge { get; set; }
            public DateTime EstimateDelivery { get; set; }
            public DateTime DurationOfPldegey { get; set; }
            public int NumberOfAvailablePledges { get; set; }
            public string Description { get; set; }

        }
        [HttpPut("/UpdatePledgeOptionOfProject")]
        public IActionResult UpdatePledgeOptionOfProject([FromBody]UpdatePledges pledges)
        {
            var temp = projectService.UpdatePledgeOptionOfProject(pledges.Email, pledges.ProjectId,pledges.PledgeOptionsId
                , pledges.TitleOfPledge,pledges.PriceOfPledge,pledges.EstimateDelivery, pledges.DurationOfPldegey
                ,pledges.NumberOfAvailablePledges, pledges.Description);
            return Ok(temp);
        }

        [HttpPut("/ UpdateProject/{ProjectId}/status{status}")]
        public IActionResult UpdateProject(int ProjectId,bool status ,[FromBody] NEwProject project)
        {
            var temp = projectService.UpdateProject(project.Email, ProjectId, project.ProjectTitle, status);
            return Ok(temp);
        }



    }
}
