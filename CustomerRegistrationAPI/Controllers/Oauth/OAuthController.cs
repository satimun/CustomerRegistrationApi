using CustomerRegistrationAPI.Engine.Oauth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Controllers.Oauth
{
    [Route("v1/[controller]")]
    [ApiController]
    public class OAuthController : ControllersBase
    {

        [HttpGet("Access")]
        public dynamic Access()
        {
            var res = new OauthAccessTokenGet();
            return ResponeValid(res.Execute(HttpContext));
        }

        [HttpPost("Login")]
        public dynamic Login([FromBody] dynamic data)
        {
            var res = new OauthLogin();
            return ResponeValid(res.Execute(HttpContext, data));
        }

        [HttpDelete("Logout")]
        public dynamic Logout()
        {
            var res = new OauthLogout();
            return ResponeValid(res.Execute(HttpContext));
        }
    }
}
