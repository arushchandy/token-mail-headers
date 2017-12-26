using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TokenAuthentication.Entity;
using TokenAuthentication.Filter;
using TokenAuthentication.Helper;
using TokenAuthentication.Models;

namespace TokenAuthentication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class UserController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/login/")]
        public HttpResponseMessage Login([FromBody]UserRequestEntity userData)
        {
            if (!string.IsNullOrEmpty(userData.UserName) && !string.IsNullOrEmpty(userData.Password))
            {
                // TODO :: Verify credntial then set the required role 
                var token = TokenHelper.GenerateToken(userData.UserName, "Role of user");
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Client data invalid, request un-authrorized.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/forgotpassword/")]
        public HttpResponseMessage ForgotPassword([FromBody]UserRequestEntity userData)
        {
            if (!string.IsNullOrEmpty(userData.UserName))
            {
                // TODO ::Verify User Email in DB 
                var isUserValid = true;
                MessageEntity message = new MessageEntity();
                if (isUserValid)
                {
                    var appUrl = Request.RequestUri.AbsoluteUri.ToString().Replace("api/user/forgotpassword", "resetpassword.html");
                    EmailHelper emailHelper = new EmailHelper();
                    var resetKey = emailHelper.SendForgotPasswordEmail(userData.UserName, appUrl);
                    if (string.IsNullOrEmpty(resetKey))
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        //TODO :: Save resetKey to the user row to be checked for later.
                        message.Message = "Password reset mail process initiated.";
                    }
                }
                // TO Ensure that user does not exits message is not shown to user 
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Client data invalid, request un-authrorized.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/resetpassword/")]
        public HttpResponseMessage ResetPassword([FromBody]UserRequestEntity userData)
        {
            MessageEntity message = new MessageEntity();
            if (!string.IsNullOrEmpty(userData.UserName) && !string.IsNullOrEmpty(userData.Password))
            {
                // TODO ::Verify Process Reset Key Earlier saved in Db and reset the password.
                // Reset the process id with a new guid so that the same code cannot be reused.
                var isProcessIdValid = true;
                if (isProcessIdValid)
                {
                    message.Message = "Password reset successful.";
                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Client data invalid, request un-authrorized.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Client data invalid, request un-authrorized.");
            }
        }

        [HttpPost]
        [TokenAuthenticate]
        [Route("api/user/getdashboard/")]
        public HttpResponseMessage GetDashBoard([FromBody]JObject formData)
        {
            string something = formData["testid"].ToString();
            var text = "Dashboard view for role : " + HttpContext.Current.Items["Role"] + " For " + HttpContext.Current.Items["UserName"];
            return Request.CreateResponse(HttpStatusCode.OK, text);
        }
    }
}