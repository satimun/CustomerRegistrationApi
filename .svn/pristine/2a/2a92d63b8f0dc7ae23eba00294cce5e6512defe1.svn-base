using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KKFCoreEngine.SendMessage
{
    public abstract class SendLineNotify
    {
        public static LineNotifyResponse lineNotify(string token = "", LineNotifyRequest msg = null,string method = "POST", string url = "https://notify-api.line.me/api/notify")
        {
            // string token = "9IBnp37LVHj0a6W5HLq2dF7sqIjGyEVn2DQtpQq7wYv";
            
            var request =   (HttpWebRequest)WebRequest.Create(url);
            var postData = "";
            postData = string.Format("message={0}", msg.message);

            //แสดง sticker ตาม Package , id 
            if (msg.stickerId != "" && msg.stickerPackageId != "")
            {
                postData = postData + "&" + string.Format("stickerPackageId={0}", msg.stickerPackageId);
                postData = postData + "&" + string.Format("stickerId={0}", msg.stickerId);
            }
            //แสดงรูปตาม url
            if (msg.imageThumbnail != "" && msg.imageFullsize != "")
            {
                postData = postData + "&" + string.Format("imageThumbnail={0}", msg.imageThumbnail);
                postData = postData + "&" + string.Format("imageFullsize={0}", msg.imageFullsize);
            }
            // Upload Fill รูป
            if (msg.imageFile != ""  )
            {
                postData = postData + "&" + string.Format("imageFile={0}", msg.imageFile);
            }


            var data = Encoding.UTF8.GetBytes(postData);

            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add("Authorization", "Bearer " + token);

            // using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length)
            //  var response = (HttpWebResponse)request.GetResponse();
            //  var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (method.Equals("POST"))
            {
               // using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
                using (var stream = request.GetRequestStream())  
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            try
            {
                // begin async call to web request.
                IAsyncResult asyncResult = request.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                using (var response = request.EndGetResponse(asyncResult))
                {
                    using (var content = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(content, Encoding.GetEncoding("UTF-8")))
                        {
                        
                            var _reader = reader.ReadToEnd();                         

                            reader.Close();
                            content.Close();
                            response.Close();
                            asyncResult.AsyncWaitHandle.Close();
                            
                            return JsonConvert.DeserializeObject<LineNotifyResponse>(_reader);
                            //return _reader;
                        }
                    }
                }
            }
            catch (WebException we)
            {
                // throw new Exception(we.Message);
                var _linenotifyRes  = new LineNotifyResponse();

                _linenotifyRes.status = 500;
                _linenotifyRes.message = we.Message;
                return _linenotifyRes;
            }
                                 
        }
    }

    public class LineNotifyResponse
    {
        public int status { get; set; }
        public string message { get; set; }

    }

    public class LineNotifyRequest
    {
        public string message { get; set; }
        public string imageThumbnail { get; set; }
        public string imageFullsize { get; set; }
        public string imageFile { get; set; }
        public string stickerPackageId { get; set; }
        public string stickerId { get; set; }
    }

}
