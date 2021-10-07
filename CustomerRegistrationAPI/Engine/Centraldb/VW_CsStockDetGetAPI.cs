using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Centraldb
{
    public class VW_CsStockDetGetAPI : EngineBase<CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.VW_CsStockDet>
    {
        public VW_CsStockDetGetAPI()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.VW_CsStockDet dataReq, ResponseAPI dataRes)
        {
            var res = new List<CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.VW_CsStockDet>();
            //var roles = CustomerRegistrationADO.Connect.Mssql.Centraldb.VW_CsStockDetAdo.GetInstant().GetData(dataReq);

            //foreach (var x in roles)
            //{
            //    var tmp = new CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.VW_CsStockDet();

                 

            //    res.Add(tmp);
            //}
            dataRes.data = res;
        }
    }
}
