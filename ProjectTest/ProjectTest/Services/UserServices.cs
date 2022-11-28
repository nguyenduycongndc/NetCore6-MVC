using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using ProjectTest.Common;
using ProjectTest.Data;
using ProjectTest.Model;
using ProjectTest.Repo.Interface;
using ProjectTest.Services.Interface;
using System.Text;
using System.Web.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectTest.Services
{
    public class UserServices : IUserService
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IUserRepo userRepo;
        private ResultModel Result;

        public UserServices(IUserRepo userRepo, ILogger<UserServices> logger)
        {
            this.userRepo = userRepo;
            _logger = logger;
        }
        public async Task<ResultModel> GetAllUser(SearchUserModel searchUserModel)
        {
            var qr = await userRepo.GetAll(searchUserModel);
            var listUser = qr.Select(x => new UserModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                IsActive = x.IsActive,
            }).OrderBy(x => x.Id).ToList();
            var data = new ResultModel()
            {
                Data = listUser,
                Message = "Successfull",
                Code = 200,
                Count = listUser.Count(),
            };
            //var data = new ResultModel()
            //{
            //    Data = listUser,
            //    Count = listUser.Count(),
            //};
            return data;
        }
        public async Task<ExportUserModel> GetAllExport(SearchUserModel searchUserModel)
        {
            var qr = await userRepo.GetAll(searchUserModel);
            var listUser = qr.Select(x => new UserModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                IsActive = x.IsActive,
            }).OrderBy(x => x.Id).ToList();
            var data = new ExportUserModel()
            {
                Data = listUser,
                Message = "Successfull",
                Code = 200,
                Count = listUser.Count(),
            };
            return data;
        }
        public async Task<ResultModel> CreateUser(CreateModel input, CurrentUserModel _userInfo)
        {
            try
            {
                var checkUser = userRepo.CheckUser(input.UserName);
                var checkRoles = await userRepo.CheckRoles(input.RoleId);
                if (checkUser.Count() > 0)
                {
                    _logger.LogError("Tài khoản đã tồn tại");
                    Result = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return Result;
                }
                if (checkRoles.Count() == 0)
                {
                    _logger.LogError("Không tồn tại quyền này");
                    Result = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return Result;
                }
                if (input.UserName == "" || input.UserName == null || input.Password == "" || input.Password == null)
                {
                    Result = new ResultModel()
                    {
                        Message = "Bad Request",
                        Code = 400,
                    };
                    return Result;
                }
                string salt = "";
                string hashedPassword = "";
                if (input != null)
                {
                    var pass = input.Password;
                    salt = Crypto.GenerateSalt(); // salt key
                    var password = input.Password + salt;
                    hashedPassword = Crypto.HashPassword(/*input.Password*/password);
                }

                UserCreateModel us = new UserCreateModel()
                {
                    UserName = input.UserName.Trim(),
                    PassWord = hashedPassword,
                    SaltKey = salt,
                    RoleId = input.RoleId,
                    CreatedBy = /*_userInfo.Id*/1,
                };
                var rs = await userRepo.CreateUs(us);
                Result = new ResultModel()
                {
                    Data = rs,
                    Message = (rs == true ? "OK" : "Bad Request"),
                    Code = (rs == true ? 200 : 400),
                };
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result;
            }
        }
        //public CurrentUserModel GetDetailModels(int Id)
        //{
        //    try
        //    {
        //        var data = userRepo.GetDetail(Id);

        //        var detailUs = new CurrentUserModel()
        //        {
        //            Id = data.Id,
        //            UserName = data.UserName,
        //            FullName = data.FullName,
        //            IsActive = data.IsActive,
        //            Email = data.Email,
        //            RoleId = data.RoleId,
        //        };

        //        return detailUs;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return null;
        //    }
        //}
        public ResultModel GetDetailModels(int Id)
        {
            try
            {
                var rs = userRepo.GetDetail(Id);
                if (rs.Count == 0)
                {
                    return Result;
                }
                else
                {
                    var detailUs = new CurrentUserModel()
                    {
                        Id = rs[0].Id,
                        UserName = rs[0].UserName,
                        FullName = rs[0].FullName,
                        IsActive = rs[0].IsActive,
                        Email = rs[0].Email,
                        RoleId = rs[0].RoleId,
                    };

                    Result = new ResultModel()
                    {
                        Data = detailUs,
                        Message = "OK"/*"Successfull"*/,
                        Code = 200,
                    };

                    return Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result;
            }
        }
        //public static string EncodeServerName(string serverName)
        //{
        //    return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        //}
        public async Task<ResultModel> UpdateUser(UpdateModel updateModel, CurrentUserModel _userInfo)
        {
            try
            {
                var checkEmail = new List<Users>();
                var checkUser = userRepo.GetDetail(updateModel.Id);
                if (string.IsNullOrEmpty(updateModel.Email) && updateModel.Email != checkUser[0].Email)
                {
                    checkEmail = userRepo.CheckEmail(updateModel.Email);
                    if (checkEmail.Count() != 0)
                    {
                        _logger.LogError("Email này đã được sử dụng");
                        Result = new ResultModel()
                        {
                            Message = "Not Found",
                            Code = 404,
                        };
                        return Result;
                    }
                }
                if (checkUser.Count() == 0)
                {
                    _logger.LogError("Tài khoản không tồn tại");
                    Result = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return Result;
                }
                
                UserUpdateModel us = new UserUpdateModel()
                {
                    Id = updateModel.Id,
                    Email = updateModel.Email,
                    IsActive = updateModel.IsActive,
                    FullName = updateModel.FullName,
                    ModifiedBy = _userInfo.Id,
                };
                var rs = await userRepo.UpdateUs(us);
                Result = new ResultModel()
                {
                    Data = rs,
                    Message = (rs == true ? "OK" : "Bad Request"),
                    Code = (rs == true ? 200 : 400),
                };
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result;
            }
        }
        public async Task<ResultModel> DeleteUser(int id, CurrentUserModel _userInfo)
        {
            try
            {
                var checkUser = userRepo.GetDetail(id);
                if (checkUser.Count() == 0)
                {
                    _logger.LogError("Tài khoản không tồn tại");
                    Result = new ResultModel()
                    {
                        Message = "Not Found",
                        Code = 404,
                    };
                    return Result;
                }
                var rs = await userRepo.DeleteUs(id, _userInfo);
                Result = new ResultModel()
                {
                    Data = rs,
                    Message = (rs == true ? "OK" : "Bad Request"),
                    Code = (rs == true ? 200 : 400),
                };
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result;
            }
        }
    }
}
