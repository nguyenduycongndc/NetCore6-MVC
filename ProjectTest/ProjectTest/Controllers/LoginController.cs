using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectTest.Attributes;
using ProjectTest.Model;
using ProjectTest.Services.Interface;
using ProjectTest.Common;
using ProjectTest.Services;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginServices;
        private readonly ISendMailService _sendMailService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginServices, ISendMailService sendMailService)
        {
            _logger = logger;
            _loginServices = loginServices;
            _sendMailService = sendMailService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUser")]
        public ResultModel LoginUser([FromBody] InputLoginModel inputModel)
        {
            var _login = _loginServices.Login(inputModel);
            if (_login.Data != null)
            {
                HttpContext.Session.SetString("SessionToken", _login.Token);
            }
            return _login;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login");
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SendOTP")]
        public ResultModel SendOTP(string email)
        {
            try
            {
                var sendMailRs = _sendMailService.SendMailOTPAsync(email);
                if (sendMailRs == true)
                {
                    var data = new ResultModel()
                    {
                        Data = true,
                        Message = "Ok",
                        Code = 200,
                    };
                    return data;
                }
                else
                {
                    var data = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return data;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var data = new ResultModel()
                {
                    Message = "Not Found",
                    Code = 404,
                };
                return data;
                throw;
            }


        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassWord")]
        public async Task<ResultModel> ForgotPassWord(ForgotPassWordModel forgotPassWordModel)
        {
            try
            {
                var sendMailRs = await _loginServices.ForgotPassWordAsync(forgotPassWordModel);
                if (sendMailRs != null && sendMailRs.Code == 200)
                {
                    var data = new ResultModel()
                    {
                        Data = true,
                        Message = "Ok",
                        Code = 200,
                    };
                    return data;
                }
                else
                {
                    var data = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var data = new ResultModel()
                {
                    Message = "Not Found",
                    Code = 404,
                };
                return data;
                throw;
            }


        }
    }
}
