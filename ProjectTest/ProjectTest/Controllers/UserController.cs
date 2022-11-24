using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProjectTest.Attributes;
using ProjectTest.Common;
using ProjectTest.Model;
using ProjectTest.Services.Interface;
using System.Text.Json;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BaseAuthorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public readonly string _contentFolderUser;
        public const string CONTEN_FOLDER_NAME_USER = "FileExcelUser.xlsx";


        public UserController(ILogger<UserController> logger, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userService = userService;
            _contentFolderUser = Path.Combine(Environment.CurrentDirectory, @"FileExcel\", CONTEN_FOLDER_NAME_USER);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ResultModel> Search([FromBody] SearchUserModel searchUserModel)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _userService.GetAllUser(searchUserModel);
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
        [HttpPost]
        [Route("Create")]
        public async Task<ResultModel> Create([FromBody] CreateModel add)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _userService.CreateUser(add, _userInfo);
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
        [Route("Detail")]
        public ResultModel Detail(int id)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return _userService.GetDetailModels(id);
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
        [HttpPut]
        [Route("Update")]
        public async Task<ResultModel> Update(UpdateModel updateModel)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _userService.UpdateUser(updateModel, _userInfo);
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
        [Route("Delete")]
        public async Task<ResultModel> Delete(int id)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel _userInfo)
                {
                    return ResUnAuthorized.Unauthor();
                }
                return await _userService.DeleteUser(id, _userInfo);
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
        //xuất excel
        [HttpPost]
        [Route("ExportUser")]
        public async Task<IActionResult> ExportUserAsync([FromBody] SearchUserModel searchUserModel)
        {
            try
            {
                if (HttpContext.Items["UserInfo"] is not CurrentUserModel userInfo)
                {
                    return Unauthorized();
                }
                var data = await _userService.GetAllExport(searchUserModel);
                List<UserModel> lst = new List<UserModel>();
                var count = data.Count;
                if (count == 0)
                {
                    var tem = new FileInfo($"{_contentFolderUser}");
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
                var template = new FileInfo($"{_contentFolderUser}");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage;
                byte[] Bytes = null;
                var memoryStream = new MemoryStream();
                using (excelPackage = new ExcelPackage(template, false))
                {
                    var worksheet = excelPackage.Workbook.Worksheets["Sheet1"];
                    var startrow = 2;
                    var startcol = 1;
                    var index = 0;
                    foreach (var a in data.Data)
                    {


                        //
                        ExcelRange dataRp0 = worksheet.Cells[startrow, startcol];
                        dataRp0.Value = string.Join(", ", a.Id);
                        //
                        ExcelRange dataRp1 = worksheet.Cells[startrow, startcol + 1];
                        dataRp1.Value = string.Join(", ", a.FullName);
                        //
                        ExcelRange dataRp2 = worksheet.Cells[startrow, startcol + 2];
                        dataRp2.Value = string.Join(", ", a.UserName);
                        ////
                        //ExcelRange dataRp3 = worksheet.Cells[startrow, startcol + 3];
                        //dataRp3.Value = string.Join(", ", a.);
                        ////
                        //ExcelRange dataRp4 = worksheet.Cells[startrow, startcol + 4];
                        //dataRp4.Value = string.Join(", ", a.);
                        ////
                        //ExcelRange dataRp5 = worksheet.Cells[startrow, startcol + 5];
                        //dataRp5.Value = string.Join(", ", a.);
                        ////
                        //ExcelRange dataRp6 = worksheet.Cells[startrow, startcol + 6];
                        //dataRp6.Value = string.Join(", ", a.);
                        ////
                        //ExcelRange dataRp7 = worksheet.Cells[startrow, startcol + 7];
                        //dataRp7.Value = string.Join(", ", a.);

                        startrow++;
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
    }
}
