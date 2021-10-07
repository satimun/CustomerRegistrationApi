using KKFCoreEngine.Recaptha;
using KKFCoreEngine.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using CustomerRegistrationAPI.Constant;
using CustomerRegistrationModel.Model.Enum;
using CustomerRegistrationModel.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CustomerRegistrationAPI.Engine
{
    public abstract class EngineBase<TRequest>     
    {
        protected string AccessToken { get; set; }
        protected string IPAddress { get; set; }
        protected string UserAgent { get; set; }

        protected string Token { get; set; }
        protected string HostReq { get; set; }

        protected string RecaptchaResponse { get; set; }
        protected string RecaptchaSecret { get; set; }

        protected string PermissionKey = "PUBLIC";
        protected bool AllowAnonymous = false;
        protected bool RecaptchaRequire = false;
        protected bool CheckVerify = false;


        protected string Token_Code { get; set; }
        protected DateTime DefaultExDate = new DateTime(2000, 1, 1);

        protected DateTime? EffectiveDateEN(DateTime? effDT)
        {
            return effDT != null ? effDT : DefaultExDate;
        }

        protected SqlTransaction transac = null;
        protected int UserID { get; set; }

        protected abstract void ExecuteChild(TRequest dataReq, ResponseAPI dataRes);

        public T ExecuteResponse<T>(TRequest dataReq, int userID, SqlTransaction transac = null)
        {
            this.UserID = userID;
            ResponseAPI res = new ResponseAPI();
            this.transac = transac;
            this.ExecuteChild(dataReq, res);
            string json = JsonConvert.SerializeObject(res.data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public ResponseAPI Execute(HttpContext context, dynamic dataReq = null)
        {
            ResponseAPI res = new ResponseAPI();

            StringValues HToken;
            context.Request.Headers.TryGetValue("Token", out HToken);
            Token = HToken.ToString();

            DateTime StartTime = DateTime.Now;
            string StackTraceMsg = string.Empty;
            try
            {
                StringValues HAccessToken;
                context.Request.Headers.TryGetValue("AccessToken", out HAccessToken);
                AccessToken = HAccessToken.ToString();

                StringValues HHostReq;
                context.Request.Headers.TryGetValue("Origin", out HHostReq);
                HostReq = HHostReq.ToString();

                IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress = /*string.Join(',', heserver.AddressList.Select(x => x.ToString()).ToList());*/ context.Connection.RemoteIpAddress.ToString();

                StringValues HUserAgent;
                context.Request.Headers.TryGetValue("User-Agent", out HUserAgent);
                UserAgent = HUserAgent.ToString();

                StringValues HRecaptchaResponse;
                context.Request.Headers.TryGetValue("RecaptchaResponse", out HRecaptchaResponse);
                RecaptchaResponse = HRecaptchaResponse.ToString();

                StringValues HRecaptchaSecret;
                context.Request.Headers.TryGetValue("RecaptchaSecret", out HRecaptchaSecret);
                RecaptchaSecret = HRecaptchaSecret.ToString();

                /*if (RecaptchaRequire)
                {
                    Recaptha.secret = RecaptchaSecret;
                    if (!Recaptha.ReCaptchaPassed(RecaptchaResponse))
                    {
                        throw new Exception(ErrorCode.V000.ToString());
                    }
                }*/

                // if (!this.GetType().Name.Equals("OauthAccessTokenGet")) this.ValidatePermission();
                /*
                     StringValues Husercode;
                     context.Request.Headers.TryGetValue("UserCode", out Husercode);
                     UserCode = Husercode.ToString();
                */

                if (!GetType().Name.Equals("OauthAccessTokenGet")) ValidatePermission();

                if (dataReq  != null)
                {
                    try
                    {
                        dataReq = this.MappingRequest(dataReq);
                    }
                    catch (Exception)
                    {
                        dataReq = this.MappingRequestArr(dataReq);
                    }

                }

                this.ExecuteChild(dataReq, res);
                if (res.code == "")
                {
                    res.code = "S0001";
                    res.message = "SUCCESS";
                    res.status = "S";
                }
            }
            catch (Exception ex)
            {
                StackTraceMsg = ex.StackTrace;
                //map error code, message
                ErrorCode error = EnumUtil.GetEnum<ErrorCode>(ex.Message);
                res.code = error.ToString();
                if (res.code == ErrorCode.U000.ToString())
                {
                    res.message = ex.Message;
                }
                else
                {
                    res.message = error.GetDescription();
                }

                res.status = "F";
            }
            finally
            {
                //
            }

            return res;

        }

        private TRequest MappingRequest(dynamic dataReq)
        {
            string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
            return JsonConvert.DeserializeObject<TRequest>(json);
        }

        private List<TRequest> MappingRequestArr(dynamic dataReq)
        {
            string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
            return JsonConvert.DeserializeObject<List<TRequest>>(json);
        }

        private void ValidatePermission()
        {

           /* if (RecaptchaRequire && !StaticValue.GetInstant().IsDevMode)
            {
                if (!Recaptha.ReCaptchaPassed(RecaptchaResponse))
                {
                    throw new Exception(ErrorCode.V000.ToString());
                }
            }*/

            // check access token
            var accessToken = Store.AccessToken.GetInstant().Get(AccessToken);
            if (accessToken == null) { throw new Exception(ErrorCode.O000.ToString()); }

            // check token
            if (!AllowAnonymous)
            {
                var token = Store.Token.GetInstant().Get(Token);

                if (token == null) { throw new Exception(ErrorCode.O000.ToString()); }
                if (token.accesstoken_code != accessToken.code ) { throw new Exception(ErrorCode.O000.ToString()); }
                if (token.status != "A") { throw new Exception(ErrorCode.O001.ToString()); }
                

                // set user id
                UserID = token.member_id;

                if (!PermissionKey.Equals("PUBLIC"))
                {
                    //if (!PermissionADO.GetInstant().CheckPermission(this.token, "C", this.PermissionKey).Any(x => x.Code == this.PermissionKey)) { throw new Exception(ErrorCode.P000.ToString()); }

                }
            }

        }
    }
}
