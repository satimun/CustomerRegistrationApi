using KKFCoreEngine.Util;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using CustomerRegistrationAPI.Constant;
using CustomerRegistrationModel.Model.Common;
using CustomerRegistrationModel.Model.Response.Member;

namespace CustomerRegistrationAPI.Engine.Websocket
{
    public class SocketHandler : WebSocketHandler
    {
        public SocketHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private JsonSerializerSettings serializerSettings;

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var req = JsonConvert.DeserializeObject<dynamic>(message);

            string socketId = WebSocketConnectionManager.GetId(socket);
            if (req?.ping != null)
            {
                await SendMessageAsync(socketId, "{\"pong\":\"ping\"}");
            }
            else if (req?.token != null)
            {
                await Task.Run(() => tokenMapping((string)req.token, socketId));
            }
            else if (req?.action != null)
            {
                if((string)req.action == "close" )
                {
                    // remove
                    var tmp = Store.Socket.GetInstant().Get(socketId);
                    if(tmp != null && tmp.Document != null && tmp.Document.locked)
                    {
                        var obj = new { document = tmp.Document };
                        obj.document.locked = false;
                        obj.document.islocked = false;
                        await SendData(JsonConvert.SerializeObject(obj));
                    }
                    Store.Socket.GetInstant().Remove(tmp);

                    await OnDisconnected(socket);
                }                
            }
            else if (req?.document != null)
            {
                int id = NumberUtil.ParseInt(req.document.id);
                string target = (string)req.document.target;
                bool locked = (bool)req.document.locked;
                //bool islocked = (bool)req.document.islocked;

                var tmp = Store.Socket.GetInstant().List.Find(v => v.Document != null && v.Document.target == target && v.Document.id == id && v.Document.locked);
                if (tmp != null && locked && tmp.ID != socketId)
                {
                    var sockTmp = WebSocketConnectionManager.GetSocketById(tmp.ID);
                    if (sockTmp?.State == WebSocketState.Open)
                    {
                        var objTmp = new { document = tmp.Document };
                        objTmp.document.islocked = true;
                        await SendMessageAsync(socketId, JsonConvert.SerializeObject(objTmp));
                        return;
                    } 
                    else
                    {
                        Store.Socket.GetInstant().Remove(tmp);
                    }
                }

                var Socket = Store.Socket.GetInstant().Get(socketId);
                Socket.Document = new SocketModel.DocumentModel()
                {
                    id = id,
                    target = target,
                    locked = locked,
                    islocked = locked,
                    lockby = Store.Member.GetInstant().GetUserDetail(Socket.User_ID)
                };

                var obj = new { document = Socket.Document };
                if (!locked)
                {
                    await SendData(JsonConvert.SerializeObject(obj));
                    Socket.Document = null;
                } 
                else
                {
                    obj.document.islocked = false;
                    await SendMessageAsync(socketId, JsonConvert.SerializeObject(obj));
                }                
            }            
            else
            {
                await SendData(message);
            }
        }

        private void tokenMapping(string token, string socketId)
        {
            try
            {
                var tmp = Store.Token.GetInstant().Get(token);
                if (tmp != null)
                {
                    var req = new SocketModel()
                    {
                        ID = socketId,
                        Token_Code = token,
                        User_ID = tmp.member_id
                    };

                    Store.Socket.GetInstant().Save(req);
                }
            }
            catch (Exception) { }
        }

        private void documentLock(dynamic document)
        {

        }

        public async Task sendNotify(MemberNotifyRes d, List<int> UserIDs)
        {
            //send noti
            try
            {
                var socket = Store.Socket.GetInstant().List.Where(v => UserIDs.Any(UserID => UserID == v.User_ID));
                foreach (var x in socket)
                {
                    await SendMessageAsync(x.ID, JsonConvert.SerializeObject(new { notify = d }, serializerSettings));
                }
            }
            catch (Exception) { }
        }

        public async Task SendData(string str)
        {
            //Newtonsoft.Json.JsonConvert.SerializeObject(tmp)
            await SendMessageToAllAsync(str);
        }

    }
}
