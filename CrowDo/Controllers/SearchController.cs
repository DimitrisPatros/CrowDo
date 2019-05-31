using CrowDoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrowDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private IProjectServices projectService;
        private ISearchServices searchService;
        private IUserServices userService;
        private ISerializer serializer;
        public SearchController(IProjectServices projectServices, ISearchServices searchServices,
            IUserServices userServices, ISerializer serializers)
        {
            projectService = projectServices;
            searchService = searchServices;
            userService = userServices;
            serializer = serializers;
        }


        [HttpGet("/SearchByText")]
        public IActionResult Search(string q)
        {
            var temp = searchService.SearchProjects(q);
            return Ok(temp);
        }

        [HttpGet("/AvailableProjects")]
        public async Task<IActionResult> AvailableProjects()
        {
            var temp = await searchService.AvailableProjects();
            return Ok(temp);
        }


        [HttpGet("/TopCreators/{number}")]
        public IActionResult Search(int number)
        {
            var temp = searchService.TopProjectCreators(number);
            return Ok(temp);
        }

        [HttpGet("/FundedProjects")]
        public IActionResult FundedProjects()
        {
            var temp = searchService.FundedProjects();
            return Ok(temp);
        }

        [HttpGet("/RecentProjects")]
        public IActionResult RecentProjects()
        {
            var temp = searchService.RecentProjects();
            return Ok(temp);
        }

        [HttpGet("/MostVisitedProjects")]
        public IActionResult MostVisitedProjects()
        {
            var temp = searchService.MostVisitedProjects();
            return Ok(temp);
        }

        [HttpGet("/LastWeekProjects")]
        public IActionResult LastWeekProjects()
        {
            var temp = searchService.LastWeekProjects();
            serializer.SaveToFile("LastWeekProjects", temp.Data);
            return Ok(temp);
        }

        [HttpGet("/LastMonthProjects")]
        public IActionResult LastMonthProjects()
        {
            var temp = searchService.LastMonthProjects();
            serializer.SaveToFile("LastMonthProjects", temp.Data);
            return Ok(temp);
        }

        [HttpGet("/ProjectByCategory/{id}")]
        public IActionResult ProjectByCategorys(int id)
        {
            var temp = searchService.ProjectByCategory(id);
            return Ok(temp);
        }

        [HttpGet("/AlmostExpireProjects")]
        public IActionResult AlmostExpireProjects()
        {
            var temp = searchService.AlmostExpireProjects();
            return Ok(temp);
        }

        [HttpGet("/MostFunded")]
        public IActionResult MostFunded()
        {
            var temp = searchService.MostFunded();
            return Ok(temp);
        }
    }
}