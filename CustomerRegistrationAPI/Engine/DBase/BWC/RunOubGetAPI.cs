using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace CustomerRegistrationAPI.Engine.DBase.BWC
{
    public class RunOubGetAPI : EngineBase<CustomerRegistrationDBase.Model.DBase.BWC.RUN_OUB>
    {
        public RunOubGetAPI()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(CustomerRegistrationDBase.Model.DBase.BWC.RUN_OUB dataReq, ResponseAPI dataRes)
        {
            var res = new List<CustomerRegistrationDBase.Model.DBase.BWC.RUN_OUB>();
           string source = "\\\\191.10.2.4\\Vol\\BKKF\\DB";
            // KKF \\191.10.2.4\vol\BKKF\DB
            //NR \\191.70.2.70\vol\BKKF\DB
            //  string source = "\\\\191.60.2.1\\SYSVOL\\BKKF\\DB";


            if (!source.EndsWith("\\"))
            {
                source =  source + "\\";
            }

            var uploads = Path.Combine(source, "");
           
          
            if (Directory.Exists(uploads))
            {
                var roles = CustomerRegistrationDBase.Connect.DBase.BWC.RUN_OUBAdo.GetInstant().ListActive(uploads);
               

                dataRes.status = "S";
                dataRes.code = "200";
                dataRes.message = "SUCCESS : "+ roles.Count().ToString();
                dataRes.data = roles;


            }
            else
            {
                dataRes.status = "F";
                dataRes.code = "404";
                dataRes.message = "Failed : " + "ไม่สามารติดต่อ ฐานข้อมูล " + uploads + " ได้";  
                
            }
            // var sources = @"D:\backup\bwc\PAYOUNDAM\";
            // var roles = CustomerRegistrationDBase.Connect.DBase.BWC.RUN_OUBAdo.GetInstant().ListActive(source);
            //var roles = CustomerRegistrationADO.Connect.DBase.Product.RUN_OUBAdo.GetInstant().ListActive(source);
            
            /*
            foreach (var x in roles)
            {
            }
            */

           // dataRes.data = res;



        }

        

    }
}
