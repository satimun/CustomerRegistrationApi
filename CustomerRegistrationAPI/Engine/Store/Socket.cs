using CustomerRegistrationModel.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Engine.Store
{
    public class Socket
    {
        private static Socket instant { get; set; }

        public static Socket GetInstant()
        {
            if (instant == null)
            {
                instant = new Socket();
                instant.data = new List<SocketModel>();
            }
            return instant;
        }

        private List<SocketModel> data;

        public List<SocketModel> List { get => data; }

        public SocketModel Get(string ID)
        {
            if (data == null)
            {
                data = new List<SocketModel>();
            }

            return data.Find(v => v.ID == ID);
        }

        public void Save(SocketModel d)
        {
            data.Add(d);
        }

        public void Remove(SocketModel d)
        {
            data.Remove(d);
        }


    }
}
