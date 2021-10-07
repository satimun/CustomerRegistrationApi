using SockNet.ClientSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PDSTDEngine.SendMessage
{
    public abstract class SendSocket
    {
        public static async Task<dynamic> Send(string server, int port, string dataMessage = "")
        {
            var resData = "";
            byte[] recData = null;
            SocketClient client = new SocketClient(server, port);
            try
            {
                if (await client.Connect())
                {
                    await client.Send(dataMessage);
                    recData = await client.ReceiveBytes();
                }
                //  Console.WriteLine("Received data: " + Encoding.UTF8.GetString(recData));
                resData = "Received data: " + Encoding.UTF8.GetString(recData);


            }
            catch (Exception e)
            {
               // Console.WriteLine("Exception raised: " + e);
                resData = "Exception raised: " + e.Message;
            }

            
            //...
            client.Disconnect();

            return resData;
        }
    }
}
