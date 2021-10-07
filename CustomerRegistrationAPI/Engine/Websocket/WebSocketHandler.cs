using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomerRegistrationAPI.Constant;

namespace CustomerRegistrationAPI.Engine.Websocket
{
    public abstract class WebSocketHandler
    {
        public WebSocketConnectionManager WebSocketConnectionManager { get; set; }

        public WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => WebSocketConnectionManager.AddSocket(socket));
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {            
            try
            {
                //byte buffer: new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(message), offset: 0, count: message.Length)
                //if (socket?.State != WebSocketState.Open) return;
                await socket.SendAsync(Encoding.UTF8.GetBytes(message),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                // remove
                var tmp = Store.Socket.GetInstant().Get(WebSocketConnectionManager.GetId(socket));
                Store.Socket.GetInstant().Remove(tmp);

                await OnDisconnected(socket);
            }
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            try
            {
                await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
            }
            catch (Exception)
            {
                // remove
                var tmp = Store.Socket.GetInstant().Get(socketId);
                Store.Socket.GetInstant().Remove(tmp);
            }
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                {
                    await SendMessageAsync(pair.Value, message);
                }                    
            }
        }

        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
