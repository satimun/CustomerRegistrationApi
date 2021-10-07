using CustomerRegistrationAPI.Engine.Apis.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Controllers.Apis
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MemberController : ControllersBase
    {
        [HttpPost("SignUp")]
        public async Task<dynamic> SignUp([FromBody] dynamic data)
        {
            var res = new MemberSignUp();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("ResendEmail")]
        public async Task<dynamic> ResendEmail([FromBody] dynamic data)
        {
            var res = new MemberResendEmail();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("Verify")]
        public async Task<dynamic> Verify([FromBody] dynamic data)
        {
            var res = new MemberVerify();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }


        [HttpPost("ForgotPassword")]
        public async Task<dynamic> ReqPassword([FromBody] dynamic data)
        {
            var res = new MemberForgotPassword();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("ResetPassword")]
        public async Task<dynamic> ResetPassword([FromBody] dynamic data)
        {
            var res = new MemberResetPassword();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpGet("Get")]
        public dynamic Get()
        {
            var res = new MemberGet();
            return ResponeValid(res.Execute(HttpContext));
        }

        [HttpPost("ChangePassword")]
        public dynamic ChangePassword([FromBody] dynamic data)
        {
            var res = new MemberChangePassword();
            return ResponeValid(res.Execute(HttpContext, data));
        }

        [HttpPost("ChangePicture")]
        public dynamic ChangePicture([FromBody] dynamic data)
        {
            var res = new MemberPictureChange();
            return ResponeValid(res.Execute(HttpContext, data));
        }


    }
}
