using Microsoft.AspNetCore.Mvc;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Controllers
{
    public abstract class ControllersBase : ControllerBase
    {
        public dynamic ResponeValid(ResponseAPI res)
        {
            if (res.status == "F")
            {
                return StatusCode(404, new { code = res.code, message = res.message });
            }

            return res.data;
        }
    }
}
