using Microsoft.AspNetCore.Mvc;
using ProjectTest.Attributes;
using ProjectTest.Common;
using ProjectTest.Model;
using ProjectTest.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ProjectTest.Services;
using ClosedXML.Excel;
using ProjectTest.Data;
using ProjectTest.Repo.Interface;
using ProjectTest.Repo;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseAuthorize]
    public class SendMailController : Controller
    {
        private readonly ILogger<SendMailController> _logger;
        protected readonly IConfiguration _config;
        private readonly ISendMailService _sendMailService;
        private readonly IEmailRepo _emailRepo;
        public readonly string _contentFolderEmailSample;
        public const string CONTEN_FOLDER_NAME_EMAIL_SAMPLE = "SampleFile.xlsx";
        public readonly string _contentFolderEmail;
        public const string CONTEN_FOLDER_NAME_EMAIL = "FileExcelEmail.xlsx";
        public readonly string _contentFolder;
        public const string CONTEN_FOLDER_NAME = "UploadFile";
        public SendMailController(ILogger<SendMailController> logger, ISendMailService sendMailService, IConfiguration config, IWebHostEnvironment webHostEnvironment, IEmailRepo emailRepo)
        {
            _logger = logger;
            _sendMailService = sendMailService;
            _config = config;
            _contentFolderEmailSample = Path.Combine(webHostEnvironment.WebRootPath, CONTEN_FOLDER_NAME_EMAIL_SAMPLE);
            _contentFolderEmail = Path.Combine(webHostEnvironment.WebRootPath, CONTEN_FOLDER_NAME_EMAIL);
            _contentFolder = Path.Combine(webHostEnvironment.WebRootPath, CONTEN_FOLDER_NAME);
            _emailRepo = emailRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("SendEmail")]
        public async Task<ResultModel> SendEmail([FromBody] EmailDto emailDto)
        {
            try
            {
                var sendMailRs = await _sendMailService.SendMailAsync(emailDto);
                if (sendMailRs == true)
                {
                    var data = new ResultModel()
                    {
                        Data = true,
                        Message = "Email sending success",
                        Code = 200,
                    };
                    return data;
                }
                else if(sendMailRs == false)
                {
                    var data = new ResultModel()
                    {
                        Data = false,
                        Message = "Email sending failed",
                        Code = 400,
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

        [HttpPost]
        [Route("SearchEmail")]
        public async Task<ResultModel> SearchEmail([FromBody] EmailSearchModel emailSearchModel)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _sendMailService.GetAllEmailService(emailSearchModel);
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
            }
        }
        //xuất file mẫu excel
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel(string jsonData)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel userInfo)
                {
                    return Unauthorized();
                }
                var obj = JsonSerializer.Deserialize<EmailSearchModel>(jsonData);
                var data = await _sendMailService.GetAllEmailService(obj);
                List<EmailDataModel> list = (List<EmailDataModel>)data.Data;
                if (data.Count == 0)
                {
                    var tem = new FileInfo($"{_contentFolderEmailSample}");
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage excelPk;
                    byte[] Bt = null;
                    var mrStream = new MemoryStream();
                    using (excelPk = new ExcelPackage(tem, false))
                    {
                        var worksheet = excelPk.Workbook.Worksheets["Sheet1"];
                        Bt = excelPk.GetAsByteArray();
                    }
                    return File(Bt, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportFile.xlsx");
                }
                var template = new FileInfo($"{_contentFolderEmail}");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage;
                byte[] Bytes = null;
                var memoryStream = new MemoryStream();
                using (excelPackage = new ExcelPackage(template, false))
                {
                    var worksheet = excelPackage.Workbook.Worksheets["Sheet1"];
                    var startrow = 3;
                    var startcol = 1;
                    var index = 1;
                    foreach (var a in list)
                    {
                        //
                        ExcelRange dataRp0 = worksheet.Cells[startrow, startcol];
                        dataRp0.Value = string.Join(", ", index);
                        //
                        ExcelRange dataRp1 = worksheet.Cells[startrow, startcol + 1];
                        dataRp1.Value = string.Join(", ", a.email_address);
                        //
                        ExcelRange dataRp2 = worksheet.Cells[startrow, startcol + 2];
                        dataRp2.Value = string.Join(", ", a.cc);
                        //
                        startrow++;
                        index++;
                    }

                    Bytes = excelPackage.GetAsByteArray();
                }
                return File(Bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportFile.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("CreateEmail")]
        public async Task<ResultModel> CreateEmail([FromBody] CreateEmailModel createEmailModel)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _sendMailService.CreateEmailS(createEmailModel);
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
            }
        }

        [HttpGet]
        [Route("DetailEmail")]
        public ResultModel DetailEmail(int id)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return _sendMailService.GetDetailEmailModels(id);
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
            }
        }
        [HttpDelete]
        [Route("DeleteEmail")]
        public async Task<ResultModel> DeleteEmail(int id)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _sendMailService.DeleteEmail(id, _userInfo);
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
            }
        }
        //import excel
        [HttpPost]
        [Route("ImportExcel"), DisableRequestSizeLimit]
        public ResultModel ImportExcel(IFormFile file)
        {
            try
            {
                //var fileextension = Path.GetExtension(file.FileName);
                //var filename = Guid.NewGuid().ToString() + fileextension;
                //var filepath = Path.Combine(_contentFolder, filename);
                var filepath = Path.Combine(_contentFolder, file.FileName);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    if (rowno != 1)
                    {
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        var allEmail = _emailRepo.CheckAllEmail();
                        //email = _context.Email.Where(s => s.Name == row.Cell(1).Value.ToString()).FirstOrDefault();
                        if (allEmail == null)
                        {
                            allEmail = new List<Email>();
                        }
                        //allEmail.Name = row.Cell(1).Value.ToString();
                        //student.Class = row.Cell(2).Value.ToString();
                        //state.Roll_No = row.Cell(3).Value.ToString();
                        //if (student.Id == Guid.Empty)
                        //    _context.Students.Add(student);
                        //else
                        //    _context.Students.Update(student);
                    }
                    else
                    {
                        rowno = 2;
                    }
                }
                var data = new ResultModel()
                {
                    Message = "Not Found",
                    Code = 404,
                };
                return data;
                //_context.SaveChanges();
                //return new ResponseViewModel<object>
                //{
                //    Status = true,
                //    Message = "Data Updated Successfully",
                //    StatusCode = System.Net.HttpStatusCode.OK.ToString()
                //};
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
