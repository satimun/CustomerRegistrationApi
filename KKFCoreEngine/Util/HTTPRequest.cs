using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KKFCoreEngine.Util
{
    public abstract class HTTPRequest
    {
        public static string Url { get; set; }

        public static dynamic Call(string method, string path, dynamic req)
        {
            string body = "";
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(Url);
                //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.ChannelAccessToken}");
                client.DefaultRequestHeaders.Add("VendorID", "t4y3hhy7sjpjstfp4erj7gvxrhxv4xrku93qa8b8s4jx2r5h69c33qpqrfw45y3g");
                client.DefaultRequestHeaders.Add("AuthKey", "gbtwtxp3mv73esb3qa3x8v56mmxx34zzrmw5cgbx2wthuhnkjsp3ydye3kxvwtjh");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                req = req is string ? (string)req : (string)JsonConvert.SerializeObject(req);

                var content = new StringContent(req, Encoding.UTF8, "application/json");

                HttpResponseMessage response = new HttpResponseMessage();

                if (method.ToLower() == "post")
                {
                    response = client.PostAsync(Url + path, content).Result;
                }
                else if (method.ToLower() == "get")
                {
                    response = client.GetAsync(Url + path).Result;
                }
                else if (method.ToLower() == "put")
                {
                    response = client.PutAsync(Url + path, content).Result;
                }
                else if (method.ToLower() == "delete")
                {
                    response = client.DeleteAsync(Url + path).Result;
                }

                //await response.CheckResult();
                HttpContent self = response.Content;
                body = self.ReadAsStringAsync().Result;


                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    dynamic tmps = JsonConvert.DeserializeObject(body);
                    throw new Exception(tmps?.code?.ToString() + ": " + tmps?.message?.ToString());
                }
            }
            dynamic tmp = "";
            try
            {
                tmp = JsonConvert.DeserializeObject(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tmp = body.ToString();
            }
            return tmp;
        }

    }
}
