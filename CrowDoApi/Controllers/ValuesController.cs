using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrowDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("/register")]
        public void RegisterNewUser([FromBody] User user)
        {
            servise.retsgapijoadg=(user.Address,user.DateOfBirth)
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
