using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Centraldb
{
    public class CsProductTypeGetAPI : EngineBase<CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.CsProductType>
    {
        public CsProductTypeGetAPI()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.CsProductType dataReq, ResponseAPI dataRes)
        {
            var res = new List<CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.CsProductType>();
            //var roles = CustomerRegistrationADO.Connect.Mssql.Centraldb.CsProductTypeAdo.GetInstant().GetData(dataReq);

            //foreach (var x in roles)
            //{
            //    var tmp = new CustomerRegistrationModel.Model.Dataset.Mssql.Centraldb.CsProductType();

            //    tmp.product_type = x.product_type;
            //    tmp.product_desc = x.product_desc;
            //    tmp.user_id = x.user_id;
            //    tmp.user_date = x.user_date;
            //    tmp.productcate = x.productcate;
            //    tmp.productcate_desc = x.productcate_desc;
            //    tmp.productcate_grp = x.productcate_grp;
            //    tmp.productcate_grp_desc = x.productcate_grp_desc;
            //    tmp.productcate_mate = x.productcate_mate;
            //    tmp.productcate_mate_desc = x.productcate_mate_desc;
            //    tmp.help_twine = x.help_twine;
            //    tmp.symbol = x.symbol;
            //    tmp.pum_flag = x.pum_flag;

            //    res.Add(tmp);
            //}
            dataRes.data = res;
        }
    }
}
