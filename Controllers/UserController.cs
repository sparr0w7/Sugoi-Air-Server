using Microsoft.AspNetCore.Mvc;
using SugoiAirServer.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SugoiAirServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDb Db { get; }

        public UserController(AppDb db)
        {
            Db = db;
        }
        // GET api/blog
        [HttpGet("/getuser")]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.ReadAllAsync();
            return new OkObjectResult(result);
        }
    }
}
