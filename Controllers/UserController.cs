using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SugoiAirServer.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SugoiAirServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDb Db { get; }

        public UserController(AppDb db)
        {
            Db = db;
        }
        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.ReadAllAsync();
            return new OkObjectResult(result);
        }
    }
}
