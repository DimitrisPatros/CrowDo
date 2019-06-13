using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CrowDoServices.Services;
using CrowDoServices;
using System.ComponentModel.DataAnnotations;

namespace CrowDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IProjectServices projectService;
        private ISearchServices searchService;
        private IUserServices userService;
        private ISerializer serializer;
        public ValuesController(IProjectServices projectServices, ISearchServices searchServices,
            IUserServices userServices, ISerializer serializers)
        {
            projectService = projectServices;
            searchService = searchServices;
            userService = userServices;
            serializer = serializers;
        }
        //[HttpPost("/UploadFromJsonUsers/filename")]
        //public IActionResult UploadFromJsonUsers(string filename)
        //{
        //    var temp = serializer.ReadFromFileUSers(filename);
        //    foreach (var u in temp)
        //    {
        //        userService.UserRegister(u.Email, u.Name, u.Surname, u.Address,
        //            u.Country, u.State, u.ZipCode, u.DateOfBirth);
        //    }
        //    return Ok(temp);
        //}

        //[HttpPost("/UploadFromJsonProjects/filename")]
        //public void UploadFromJsonProjects(string filename)
        //{
        //    var jprojects = serializer.ReadFromFileProject(filename);

        //    foreach (var Jp in jprojects)
        //    {
        //        projectService.CreateProject(Jp.creator, Jp.nameOfProject, Jp.demandedfunds);
        //    }
        //}



        //public struct NewUser
        //{
        //    public string Email { get; set; }
        //    public string Name { get; set; }
        //    public string Surname { get; set; }
        //    public string Address { get; set; }
        //    public string Country { get; set; }
        //    public string State { get; set; }
        //    public string ZipCode { get; set; }
        //    public DateTime DateOfBirth { get; set; }
        //}

        //[HttpPost("/RegisterUser")]
        //public ActionResult<Result<User>> RegisterUser([FromBody] NewUser temp)
        //{
        //    return userService.UserRegister(temp.Email, temp.Name, temp.Surname, temp.Address
        //        , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth);
        //}

        //[HttpPut("/DeleteUser/{email}")]
        //public IActionResult UserDelete(string email)
        //{
        //    var result = userService.UserDelete(email);
        //    return Ok(result);
        //}

        //[HttpPut("/UpdateUser/{email}")]
        //public IActionResult UserUpdate(string email, [FromBody] NewUser temp)
        //{
        //    var result = userService.UserUpdate(email, temp.Name, temp.Surname, temp.Address
        //        , temp.Country, temp.State, temp.ZipCode, temp.DateOfBirth, temp.Email);
        //    return Ok(result);
        //}

        //public struct NewProject
        //{
        //    public string Email { get; set; }
        //    public string ProjectTitle { get; set; }
        //    public double FundingBudjet { get; set; }
        //}

        //[HttpPost("/ CreateProject")]
        //public IActionResult CreateProject(NewProject project)
        //{
        //    var temp = projectService.CreateProject(project.Email, project.ProjectTitle, project.FundingBudjet);
        //    return Ok(temp);
        //}


        //[HttpGet("/MyPledges{email}")]
        //public IActionResult MyPledges(string email)
        //{
        //    var result = userService.MyPledges(email);
        //    return Ok(result);
        //}

        //[HttpGet("/UserComments{email}")]
        //public IActionResult UserComments(string email)
        //{
        //    var result = userService.UserComments(email);
        //    return Ok(result);
        //}


        //public struct Category
        //{
        //    public string Email { get; set; }
        //    public int ProjectId { get; set; }
        //    public string CategoryName { get; set; }
        //}
        //[HttpPost("/ AddCategoryToProject")]
        //public IActionResult AddCategoryToProject([FromBody] Category newCategory)
        //{
        //    var temp = projectService.AddCategoryToProject(newCategory.Email, newCategory.ProjectId, newCategory.CategoryName);
        //    return Ok(temp);
        //}

        //[HttpPut("/  DeleteCategoryFromProject")]
        //public IActionResult DeleteCategoryFromProject(Category category)
        //{
        //    var temp = projectService.DeleteCategoryFromProject(category.Email, category.ProjectId, category.CategoryName);
        //    return Ok(temp);
        //}


        //public struct NewPledgeOptionToProject
        //{
        //    public string Email { get; set; }
        //    public int ProjectId { get; set; }
        //    public string TitleOfPledge { get; set; }
        //    public double PriceOfPledge { get; set; }
        //    public DateTime EstimateDelivery { get; set; }
        //    public DateTime DurationOfPldege { get; set; }
        //    public int NumberOfAvailablePledges { get; set; }
        //    public string Descritpion { get; set; }
        //}
        //[HttpPost("/ AddPledgeOptionToProject")]
        //public IActionResult AddPledgeOptionToProject([FromBody] NewPledgeOptionToProject npo)
        //{
        //    var temp = projectService.AddPledgeOptionToProject(npo.Email, npo.ProjectId,
        //    npo.TitleOfPledge, npo.PriceOfPledge, npo.EstimateDelivery,
        //    npo.DurationOfPldege, npo.NumberOfAvailablePledges, npo.Descritpion);
        //    return Ok(temp);
        //}

        //public struct RemovePledge
        //{
        //    public string Email { get; set; }
        //    public int ProjectId { get; set; }
        //    public int PledgeOptionsId { get; set; }
        //}
        //[HttpPut("/  DeletePledgeOptionFromProject")]
        //public IActionResult DeletePledgeOptionFromProject(RemovePledge removePledge)
        //{
        //    var temp = projectService.DeletePledgeOptionFromProject(removePledge.Email, removePledge.ProjectId, removePledge.PledgeOptionsId);
        //    return Ok(temp);
        //}

        //public struct UpdatePledges
        //{
        //    public string Email { get; set; }
        //    public int ProjectId { get; set; }
        //    public int PledgeOptionsId { get; set; }
        //    public string TitleOfPledge { get; set; }
        //    public double PriceOfPledge { get; set; }
        //    public DateTime EstimateDelivery { get; set; }
        //    public DateTime DurationOfPldegey { get; set; }
        //    public int NumberOfAvailablePledges { get; set; }
        //    public string Description { get; set; }

        //}
        //[HttpPut("/UpdatePledgeOptionOfProject")]
        //public IActionResult UpdatePledgeOptionOfProject([FromBody]UpdatePledges pledges)
        //{
        //    var temp = projectService.UpdatePledgeOptionOfProject(pledges.Email, pledges.ProjectId, pledges.PledgeOptionsId
        //        , pledges.TitleOfPledge, pledges.PriceOfPledge, pledges.EstimateDelivery, pledges.DurationOfPldegey
        //        , pledges.NumberOfAvailablePledges, pledges.Description);
        //    return Ok(temp);
        //}
        //////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //public struct NewProjectInfo
        //{
        //    public string Email { get; set; }
        //    public int ProjectId { get; set; }
        //    public string Title { get; set; }
        //    public string Filename { get; set; }
        //    public string Descritpion { get; set; }
        //}

        //[HttpPost("/ AddProjectInfo")]
        //public IActionResult AddProjectInfo([FromBody] NewProjectInfo npi)
        //{
        //    var temp = projectService.AddProjectInfo(npi.Email, npi.ProjectId, npi.Title, npi.Descritpion, npi.Filename);
        //    return Ok(temp);
        //}

        //[HttpPut("/DeleteProjectInfo{ProjectInfoId}/User/{email}")]
        //public IActionResult DeleteProjectInfo(int ProjectInfoId, string email)
        //{
        //    var temp = projectService.DeleteProjectInfo(email, ProjectInfoId);
        //    return Ok(temp);
        //}


        //[HttpPut("/DeleteProject")]
        //public IActionResult DeleteProject(int ProjectId, string email)
        //{
        //    var temp = projectService.DeleteProject(email, ProjectId);
        //    return Ok(temp);
        //}


        //[HttpPut("/ UpdateProject/{ProjectId}/status{status}")]
        //public IActionResult UpdateProject(int ProjectId, bool status, [FromBody] NewProject project)
        //{
        //    var temp = projectService.UpdateProject(project.Email, ProjectId, project.ProjectTitle, status);
        //    return Ok(temp);
        //}
        ////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //[HttpPut("/ProgressOfFunding/{ProjectId}")]
        //public IActionResult ProgressOfFunding(int ProjectId)
        //{
        //    var temp = projectService.ProgressOfFunding(ProjectId);
        //    return Ok(temp);
        //}


        //[HttpPost("/ AutoProjectStatusUpdate")]
        //public IActionResult AutoProjectStatusUpdate()
        //{
        //    var temp = projectService.AutoProjectStatusUpdate();
        //    return Ok(temp);
        //}


        //[HttpGet("/ProjectComments/{ProjectId}")]
        //public IActionResult ProjectComments(int ProjectId)
        //{
        //    var temp = projectService.ProjectComments(ProjectId);
        //    return Ok(temp);
        //}
        ////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //[HttpGet("/Search")]
        //public IActionResult Search(string q)
        //{
        //    var temp = searchService.SearchProjects(q);
        //    return Ok(temp);
        //}

        //[HttpGet("/TopCreators/{number}")]
        //public IActionResult Search(int number)
        //{
        //    var temp = searchService.TopProjectCreators(number);
        //    return Ok(temp);
        //}

        //[HttpGet("/AvailableProjects")]
        //public IActionResult AvailableProjects()
        //{
        //    var temp = searchService.AvailableProjects();
        //    return Ok(temp);
        //}

        //[HttpGet("/FundedProjects")]
        //public IActionResult FundedProjects()
        //{
        //    var temp = searchService.FundedProjects();
        //    return Ok(temp);
        //}
        //[HttpGet("/RecentProjects")]
        //public IActionResult RecentProjects()
        //{
        //    var temp = searchService.RecentProjects();
        //    return Ok(temp);
        //}
        //[HttpGet("/MostVisitedProjects")]
        //public IActionResult MostVisitedProjects()
        //{
        //    var temp = searchService.MostVisitedProjects();
        //    return Ok(temp);
        //}

        //[HttpGet("/LastWeekProjects")]
        //public IActionResult LastWeekProjects()
        //{
        //    var temp = searchService.LastWeekProjects();
        //    //here i have to call the excel serializer
        //    return Ok(temp);
        //}
        //[HttpGet("/LastMonthProjects")]
        //public IActionResult LastMonthProjects()
        //{
        //    var temp = searchService.LastMonthProjects();
        //    //here i have to call the excel serializer
        //    return Ok(temp);
        //}

        //[HttpGet("/ProjectByCategorys/{id}")]
        //public IActionResult ProjectByCategorys(int id)
        //{
        //    var temp = searchService.ProjectByCategory(id);
        //    return Ok(temp);
        //}

        //[HttpGet("/AlmostExpireProjects")]
        //public IActionResult AlmostExpireProjects()
        //{
        //    var temp = searchService.AlmostExpireProjects();
        //    return Ok(temp);
        //}
        //[HttpGet("/MostFunded")]
        //public IActionResult MostFunded()
        //{
        //    var temp = searchService.MostFunded();
        //    return Ok(temp);
        //}



        //[HttpPost("/BackAProject/{PledgeId}/email/{email}")]
        //public IActionResult BackAProject(int PledgeId, string email)
        //{
        //    var result = userService.CreatePledge(email, PledgeId);
        //    return Ok(result);
        //}

        //[HttpGet("/MyProjects{email}")]
        //public IActionResult MyProjects(string email)
        //{
        //    var result = userService.MyProjects(email);
        //    return Ok(result);
        //}
    }
}




