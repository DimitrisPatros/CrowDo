using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CrowDo.Models.Project;

namespace CrowDo.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase {
		private IProjectServices projectService;

		public ProjectController(IProjectServices projectServices) {
			projectService = projectServices;
		}

		[HttpPost("/CreateNewProject")]
		public IActionResult CreateProject(NewProject project) {
			var temp = projectService.CreateProject(project.Email, project.ProjectTitle, project.FundingBudjet);
			return Ok(temp);
		}

		[HttpPost("/AddCategoryToProject")]
		public IActionResult AddCategoryToProject([FromBody] Category newCategory) {
			var temp = projectService.AddCategoryToProject(newCategory.Email,
				newCategory.ProjectId,
				newCategory.CategoryName);
			return Ok(temp);
		}

		[HttpPost("/AddPledgeOptionToProject")]
		public IActionResult AddPledgeOptionToProject([FromBody] NewPledgeOptionToProject npo) {
			var temp = projectService.AddPledgeOptionToProject(npo.Email,
				npo.ProjectId,
				npo.TitleOfPledge,
				npo.PriceOfPledge,
				npo.EstimateDelivery,
				npo.DurationOfPldege,
				npo.NumberOfAvailablePledges,
				npo.Descritpion);
			return Ok(temp);
		}

		[HttpPost("/AddProjectInfo")]
		public IActionResult AddProjectInfo([FromBody] NewProjectInfo npi) {
			var temp = projectService.AddProjectInfo(npi.Email,
				npi.ProjectId,
				npi.Title,
				npi.Descritpion,
				npi.Filename);
			return Ok(temp);
		}

		/*-------------------------------------------------------Delete------------------------------------------------------------------*/
		[HttpDelete("/Project")]
		public IActionResult DeleteProject(int ProjectId, string email) {
			var temp = projectService.DeleteProject(email, ProjectId);
			return Ok(temp);
		}

		[HttpDelete("/CategoryFromProject")]
		public IActionResult DeleteCategoryFromProject(Category category) {
			var temp = projectService.DeleteCategoryFromProject(category.Email,
				category.ProjectId,
				category.CategoryName);
			return Ok(temp);
		}

		[HttpDelete("/PledgeOptionFromProject")]
		public IActionResult DeletePledgeOptionFromProject(RemovePledge removePledge) {
			var temp = projectService.DeletePledgeOptionFromProject(removePledge.Email,
				removePledge.ProjectId,
				removePledge.PledgeOptionsId);
			return Ok(temp);
		}

		[HttpDelete("/ProjectInfo{ProjectInfoId}/User/{email}")]
		public IActionResult DeleteProjectInfo(int ProjectInfoId, string email) {
			var temp = projectService.DeleteProjectInfo(email, ProjectInfoId);
			return Ok(temp);
		}

		/*-------------------------------------------------------Update------------------------------------------------------------------*/
		[HttpPut("/UpdateProject/{ProjectId}/status{status}")]
		public IActionResult UpdateProject(int ProjectId, bool status, [FromBody] NewProject project) {
			var temp = projectService.UpdateProject(project.Email, ProjectId, project.ProjectTitle, status);
			return Ok(temp);
		}

		[HttpPut("/UpdatePledgeOptionOfProject")]
		public IActionResult UpdatePledgeOptionOfProject([FromBody] UpdatePledges pledges) {
			var temp = projectService.UpdatePledgeOptionOfProject(pledges.Email,
				pledges.ProjectId,
				pledges.PledgeOptionsId,
				pledges.TitleOfPledge,
				pledges.PriceOfPledge,
				pledges.EstimateDelivery,
				pledges.DurationOfPldegey,
				pledges.NumberOfAvailablePledges,
				pledges.Description);
			return Ok(temp);
		}

		/*-------------------------------------------------------Auto functions------------------------------------------------------------------*/
		[HttpPut("/ProgressOfFunding/{ProjectId}")]
		public IActionResult ProgressOfFunding(int ProjectId) {
			var temp = projectService.ProgressOfFunding(ProjectId);
			return Ok(temp);
		}

		[HttpPost("/AutoProjectStatusUpdate")]
		public IActionResult AutoProjectStatusUpdate() {
			var temp = projectService.AutoProjectStatusUpdate();
			return Ok(temp);
		}

		[HttpGet("/ProjectComments/{ProjectId}")]
		public IActionResult ProjectComments(int ProjectId) {
			var temp = projectService.ProjectComments(ProjectId);
			return Ok(temp);
		}
	}
}