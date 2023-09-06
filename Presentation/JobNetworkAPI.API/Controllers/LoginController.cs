using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace JobNetworkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetSessionInfo()
        {
            List<string> sessionInfo=new List<string>();

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyEmail))){
                HttpContext.Session.SetString(SessionKeyEnum.SessionKeyEmail.ToString(), "Current User");
                HttpContext.Session.SetString(SessionKeyEnum.SessionKeyPassword.ToString(), "Current Password");
            }

            var useremail = HttpContext.Session.GetString(SessionVariables.SessionKeyEmail);
            var userpassword=HttpContext.Session.GetString(SessionVariables.SessionKeyPassword);


            sessionInfo.Add(useremail);
            sessionInfo.Add(userpassword);
            return sessionInfo;
        }
      
    }
}
