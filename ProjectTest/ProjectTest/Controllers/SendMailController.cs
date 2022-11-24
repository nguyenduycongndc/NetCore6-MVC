using Microsoft.AspNetCore.Mvc;
using ProjectTest.Attributes;
using ProjectTest.Common;
using ProjectTest.Model;
using ProjectTest.Services.Interface;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseAuthorize]
    public class SendMailController : Controller
    {
        private readonly ILogger<SendMailController> _logger;

        private readonly ISendMailService _sendMailService;
        public SendMailController(ILogger<SendMailController> logger, ISendMailService sendMailService)
        {
            _logger = logger;
            _sendMailService = sendMailService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("SendMail")]
        public async Task<ResultModel> SendMail(EmailDto emailDto)
        {
            try
            {
                var sendMailRs = await _sendMailService.SendMailAsync(emailDto);
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
    }
}
