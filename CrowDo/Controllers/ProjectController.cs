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
    public class ProjectController : ControllerBase
    {
        private IProjectServices projectService;
        private ISearchServices searchService;
        private IUserServices userService;
        private ISerializer serializer;
        public ProjectController(IProjectServices projectServices, ISearchServices searchServices,
            IUserServices userServices, ISerializer serializers)
        {
            projectService = projectServices;
            searchService = searchServices;
            userService = userServices;
            serializer = serializers;
        }

        public struct NEwProject
        {
            public string Email { get; set; }
            public string ProjectTitle { get; set; }
            public double FundingBudjet { get; set; }
        }

        [HttpPost("/CreateNewProject")]
        public IActionResult CreateProject(NEwProject project)
        {
            var temp = projectService.CreateProject(project.Email, project.ProjectTitle, project.FundingBudjet);
            return Ok(temp);
        }
        public struct Category
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public string CategoryName { get; set; }
        }

        [HttpPost("/AddCategoryToProject")]
        public IActionResult AddCategoryToProject([FromBody] Category newCategory)
        {
            var temp = projectService.AddCategoryToProject(newCategory.Email, newCategory.ProjectId, newCategory.CategoryName);
            return Ok(temp);
        }

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
        [HttpPost("/AddPledgeOptionToProject")]
        public IActionResult AddPledgeOptionToProject([FromBody] NEwPledgeOptionToProject npo)
        {
            var temp = projectService.AddPledgeOptionToProject(npo.Email, npo.ProjectId,
            npo.TitleOfPledge, npo.PriceOfPledge, npo.EstimateDelivery,
            npo.DurationOfPldege, npo.NumberOfAvailablePledges, npo.Descritpion);
            return Ok(temp);
        }

        public struct NewProjectInfo
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public string Title { get; set; }
            public string Filename { get; set; }
            public string Descritpion { get; set; }
        }

        [HttpPost("/AddProjectInfo")]
        public IActionResult AddProjectInfo([FromBody] NewProjectInfo npi)
        {
            var temp = projectService.AddProjectInfo(npi.Email, npi.ProjectId, npi.Title, npi.Descritpion, npi.Filename);
            return Ok(temp);
        }

        /*-------------------------------------------------------Delete------------------------------------------------------------------*/
        [HttpPut("/DeleteProject")]
        public IActionResult DeleteProject(int ProjectId, string email)
        {
            var temp = projectService.DeleteProject(email, ProjectId);
            return Ok(temp);
        }

        [HttpPut("/DeleteCategoryFromProject")]
        public IActionResult DeleteCategoryFromProject(Category category)
        {
            var temp = projectService.DeleteCategoryFromProject(category.Email, category.ProjectId, category.CategoryName);
            return Ok(temp);
        }

        public struct RemovePledge
        {
            public string Email { get; set; }
            public int ProjectId { get; set; }
            public int PledgeOptionsId { get; set; }
        }
        [HttpPut("/DeletePledgeOptionFromProject")]
        public IActionResult DeletePledgeOptionFromProject(RemovePledge removePledge)
        {
            var temp = projectService.DeletePledgeOptionFromProject(removePledge.Email, removePledge.ProjectId, removePledge.PledgeOptionsId);
            return Ok(temp);
        }

        [HttpPut("/DeleteProjectInfo{ProjectInfoId}/User/{email}")]
        public IActionResult DeleteProjectInfo(int ProjectInfoId, string email)
        {
            var temp = projectService.DeleteProjectInfo(email, ProjectInfoId);
            return Ok(temp);
        }
        /*-------------------------------------------------------Update------------------------------------------------------------------*/
        [HttpPut("/UpdateProject/{ProjectId}/status{status}")]
        public IActionResult UpdateProject(int ProjectId, bool status, [FromBody] NEwProject project)
        {
            var temp = projectService.UpdateProject(project.Email, ProjectId, project.ProjectTitle, status);
            return Ok(temp);
        }

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
            var temp = projectService.UpdatePledgeOptionOfProject(pledges.Email, pledges.ProjectId, pledges.PledgeOptionsId
                , pledges.TitleOfPledge, pledges.PriceOfPledge, pledges.EstimateDelivery, pledges.DurationOfPldegey
                , pledges.NumberOfAvailablePledges, pledges.Description);
            return Ok(temp);
        }

        /*-------------------------------------------------------Auto functions------------------------------------------------------------------*/
        [HttpPut("/ProgressOfFunding/{ProjectId}")]
        public IActionResult ProgressOfFunding(int ProjectId)
        {
            var temp = projectService.ProgressOfFunding(ProjectId);
            return Ok(temp);
        }

        [HttpPost("/AutoProjectStatusUpdate")]
        public IActionResult AutoProjectStatusUpdate()
        {
            var temp = projectService.AutoProjectStatusUpdate();
            return Ok(temp);
        }

        [HttpGet("/ProjectComments/{ProjectId}")]
        public IActionResult ProjectComments(int ProjectId)
        {
            var temp = projectService.ProjectComments(ProjectId);
            return Ok(temp);
        }
    }
}