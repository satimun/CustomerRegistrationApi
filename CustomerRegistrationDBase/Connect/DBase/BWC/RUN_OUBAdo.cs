 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistrationDBase.Connect.DBase.BWC
{
    public class RUN_OUBAdo : BWCBase
    {
        private static RUN_OUBAdo instant;

        public static RUN_OUBAdo GetInstant()
        {
            if (instant == null) instant = new RUN_OUBAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private RUN_OUBAdo() { }

        public List<Model.DBase.BWC.RUN_OUB> ListActive(string source = null)
        {
            string cmd = " SELECT * FROM RUN_OUB  ";

            string connstr = null;

            if (  string.IsNullOrEmpty(source) == false )
            {
                connstr = SetConnect(source);
            }


            
                var res = Query<Model.DBase.BWC.RUN_OUB>(cmd, null, connstr).ToList();
            

            return res;
        }

    }
}
