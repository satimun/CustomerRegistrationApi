using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerRegistrationAPI.Engine.Centraldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Controllers.Centraldb
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class CsProductTypeController : ControllersBase
    {
        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new CsProductTypeGetAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }
        
    }
}
