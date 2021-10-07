using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CustomerRegistrationAPI.Engine.DBase.BWC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
 
using Microsoft.AspNetCore.StaticFiles;
 
using Microsoft.Net.Http.Headers;

namespace CustomerRegistrationAPI.Controllers.DBase.BWC
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class RunOubController : ControllersBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration Configuration;

        public RunOubController(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _hostingEnvironment = environment;
        }

        [HttpGet("GetData")]
        public async Task<dynamic> GetData( )
        {
            var data = new CustomerRegistrationDBase.Model.DBase.BWC.RUN_OUB();
            var res = new RunOubGetAPI();
            return await Task.Run(() => res.Execute(HttpContext, data));
        }
    }
}
