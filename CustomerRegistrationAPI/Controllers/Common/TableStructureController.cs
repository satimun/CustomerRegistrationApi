using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CustomerRegistrationAPI.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Controllers.Common
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TableStructureController : ControllersBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration Configuration;

        public TableStructureController(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _hostingEnvironment = environment;
        }

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new TableStructureApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }
    }
}
