using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Response
{
    public class ResponseAPI
    {
        public string status { get; set; }
        public string statusdesc { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public dynamic data = "";
        public string ServerAddr { get; set; }
    }
}
