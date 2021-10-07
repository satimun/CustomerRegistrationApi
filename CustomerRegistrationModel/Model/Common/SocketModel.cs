using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistrationModel.Model.Common
{
   public  class SocketModel
    {
        public string ID;
        public string Token_Code;
        public int User_ID;
        public DocumentModel Document;

        public class DocumentModel
        {
            public string target;
            public int id;
            public bool locked;
            public bool islocked;
            public string lockby;
        }
    }
}
