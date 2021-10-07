using CustomerRegistrationModel.Model.Common;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Common
{
    public class TableStructureApi : EngineBase<TableStructureModel>
    {
        public TableStructureApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(TableStructureModel dataReq, ResponseAPI dataRes)
        {
            List<TableStructureModel> lst = new List<TableStructureModel>();
            lst = CustomerRegistrationADO.Connect.Mssql.CRMDB.Common.TableStructureAdo.GetInstant().Get(dataReq);

            dataRes.data = lst;

            if (lst != null && lst.Count > 0)
            {
                dataRes.status = "S";
                dataRes.message = "S : ทำรายการสำเร็จ (Success)";

            }
            else
            {
                dataRes.status = "F";
                dataRes.message = "F : ทำรายการไม่สำเร็จ (Fail) ";
            }
        }
    }
}
