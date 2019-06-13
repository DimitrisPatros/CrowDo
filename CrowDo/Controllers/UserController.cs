using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CrowDoServices;
using CrowDo.Models.User;
using DatabaseContext.Models;

namespace CrowDo.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase {
		private IUserServices userService;

		public UserController(IUserServices userServices) { userService = userServices; }

		[HttpPost("/RegisterUser")]
		public ActionResult<Result<User>> RegisterUser([FromBody] NewUser temp) {
			return userService.UserRegister(temp.Email,
				temp.Name,
				temp.Surname,
				temp.Address,
				temp.Country,
				temp.State,
				temp.ZipCode,
				temp.DateOfBirth);
		}

		[HttpPost("/BackAProject/{PledgeId}/email/{email}")]
		public IActionResult BackAProject(int PledgeId, string email) {
			var result = userService.CreatePledge(email, PledgeId);
			return Ok(result);
		}

		/*-------------------------------------------------------Delete------------------------------------------------------------------*/
		[HttpDelete("/User/{email}")]
		public IActionResult UserDelete(string email) {
			var result = userService.UserDelete(email);
			return Ok(result);
		}

		[HttpPut("/UpdateUser/{email}")]
		public IActionResult UserUpdate(string email, [FromBody] NewUser temp) {
			var result = userService.UserUpdate(email,
				temp.Name,
				temp.Surname,
				temp.Address,
				temp.Country,
				temp.State,
				temp.ZipCode,
				temp.DateOfBirth,
				temp.Email);
			return Ok(result);
		}

		[HttpGet("/MyPledges{email}")]
		public IActionResult MyPledges(string email) {
			var result = userService.MyPledges(email);
			return Ok(result);
		}

		[HttpGet("/UserComments{email}")]
		public IActionResult UserComments(string email) {
			var result = userService.UserComments(email);
			return Ok(result);
		}

		[HttpGet("/MyProjects{email}")]
		public IActionResult MyProjects(string email) {
			var result = userService.MyProjects(email);
			return Ok(result);
		}
	}
}