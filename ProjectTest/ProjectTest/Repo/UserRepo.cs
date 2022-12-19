using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Data;
using ProjectTest.Model;
using ProjectTest.Repo.Interface;

namespace ProjectTest.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly SqlDbContext context;

        public UserRepo(SqlDbContext context)
        {
            this.context = context;
        }
        #region Search
        public async Task<List<Users>> GetAll(SearchUserModel searchUserModel)
        {
            //return context.Users.ToList();
            List<Users> list;
            //string sql = "EXECUTE SP_USER";
            try
            {
                string sql = "EXECUTE SP_SEARCH_USER @user_name, @is_active, @start_number, @page_size";

                ////string sql = "EXECUTE SP_USER" +
                ////"@user_name = '" + searchUserModel.UserName + "'," +
                ////"@is_active = " + searchUserModel.IsActive + "," +
                ////"@start_number = " + searchUserModel.StartNumber + "," +
                ////"@page_size = " + searchUserModel.PageSize + ",";


                List<SqlParameter> parms = new List<SqlParameter>
                { 
                    // Create parameters    
                    new SqlParameter { ParameterName = "@user_name", Value = searchUserModel.UserName },
                    new SqlParameter { ParameterName = "@is_active", Value = searchUserModel.IsActive },
                    new SqlParameter { ParameterName = "@start_number", Value = searchUserModel.StartNumber },
                    new SqlParameter { ParameterName = "@page_size", Value = searchUserModel.PageSize }
                };
                list = await context.Users.FromSqlRaw<Users>(sql, parms.ToArray()).ToListAsync();

                ////List<SqlParameter> LstParam = new List<SqlParameter>();
                ////SqlParameter param = new SqlParameter();
                ////param = new SqlParameter("@user_name", searchUserModel.UserName);
                ////LstParam.Add(param);
                ////param = new SqlParameter("@is_active", searchUserModel.IsActive);
                ////LstParam.Add(param);
                ////param = new SqlParameter("@start_number", searchUserModel.StartNumber);
                ////LstParam.Add(param);
                ////param = new SqlParameter("@page_size", searchUserModel.PageSize);
                ////LstParam.Add(param);

                //list = await context.Users.FromSqlRaw<Users>(sql,parms.ToArray()).ToListAsync();
                //Debugger.Break();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //public async Task<bool> CreateUs(Users user, UsersRoles usersRoles)
        #region Create
        public async Task<bool> CreateUs(UserCreateModel user)
        {
            try
            {
                string sql = "EXECUTE SP_CREATE_USER @user_name, @role_id, @password, @salt, @created_by";

                List<SqlParameter> parms = new List<SqlParameter>
                { 
                    // Create parameters    
                    new SqlParameter { ParameterName = "@user_name", Value = user.UserName },
                    new SqlParameter { ParameterName = "@role_id", Value = user.RoleId },
                    new SqlParameter { ParameterName = "@password", Value = user.PassWord },
                    new SqlParameter { ParameterName = "@salt", Value = user.SaltKey },
                    new SqlParameter { ParameterName = "@created_by", Value = user.CreatedBy },
                };

                var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Check
        public List<Users> CheckUser(string userName)
        {
            List<Users> list;
            string sql = "EXECUTE SP_CHECK_USER @user_name";


            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@user_name", Value = userName },
            };
            list = context.Users.FromSqlRaw<Users>(sql, parms.ToArray()).ToList();
            return list;
        }
        public async Task<List<Roles>> CheckRoles(int RolesId)
        {
            List<Roles> list;
            string sql = "EXECUTE SP_CHECK_ROLES @roles_id";


            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@roles_id", Value = RolesId },
            };
            list = await context.Roles.FromSqlRaw<Roles>(sql, parms.ToArray()).ToListAsync();
            return list;
        }
        public List<Users> GetDetail(int id)
        {
            List<Users> list;
            string sql = "EXECUTE SP_DETAIL_USER @user_id";


            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@user_id", Value = id },
            };
            list = context.Users.FromSqlRaw<Users>(sql, parms.ToArray()).ToList();
            return list;
        }

        public List<Users> CheckEmailUser(string email)
        {
            List<Users> list;
            string sql = "EXECUTE SP_CHECK_EMAIL_USER @email";


            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@email", Value = email },
            };
            list = context.Users.FromSqlRaw<Users>(sql, parms.ToArray()).ToList();
            return list;
        }
        public List<Users> CheckAllEmail()//AutoSendMail
        {
            return null;
            //List<Users> list;
            //string sql = "EXEC SP_CHECK_ALL_EMAIL";
            //list = context.Users.FromSqlRaw<Users>(sql).ToList();
            //return list;
        }

        public List<Users> CheckOTP(checkOTPModel checkOTPModel)
        {
            List<Users> list;
            string sql = "EXECUTE SP_CHECK_OTP @email, @otp";


            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@email", Value = checkOTPModel.Email },
                new SqlParameter { ParameterName = "@otp", Value = checkOTPModel.OTP },
            };
            list = context.Users.FromSqlRaw<Users>(sql, parms.ToArray()).ToList();
            return list;
        }
        #endregion
        //public Users GetDetail(int id)
        //{
        //    var query = (from x in context.Users
        //                 where x.Id.Equals(id)
        //                 select new Users
        //                 {
        //                     Id = x.Id,
        //                     UserName = x.UserName,
        //                     Password = x.Password,
        //                     IsActive = x.IsActive,
        //                     RoleId = x.RoleId,
        //                     FullName = x.FullName,
        //                     Email = x.Email,
        //                 }).FirstOrDefault();

        //    return query;
        //}
        //public Users GetDetailByName(InputLoginModel inputModel)
        //{
        //    var query = (from x in context.Users
        //                 where x.UserName.Equals(inputModel.UserName) && x.IsActive.Equals(1)
        //                 select new Users
        //                 {
        //                     Id = x.Id,
        //                     UserName = x.UserName,
        //                     FullName = x.FullName,
        //                     Password = x.Password,
        //                     IsActive = x.IsActive,
        //                     RoleId = x.RoleId,
        //                     SaltKey = x.SaltKey,
        //                 }).FirstOrDefault();

        //    return query;
        //}


        #region Update
        public async Task<bool> UpdateUs(UserUpdateModel user)
        {
            try
            {
                string sql = "EXECUTE SP_UPDATE_USER @id, @full_name, @email, @is_active, @modified_by";

                List<SqlParameter> parms = new List<SqlParameter>
                { 
                    new SqlParameter { ParameterName = "@id", Value = user.Id },
                    new SqlParameter { ParameterName = "@full_name", Value = user.FullName },
                    new SqlParameter { ParameterName = "@email", Value = user.Email },
                    new SqlParameter { ParameterName = "@is_active", Value = user.IsActive },
                    new SqlParameter { ParameterName = "@modified_by", Value = user.ModifiedBy },
                }; 

                var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Delete
        public async Task<bool> DeleteUs(int id, CurrentUserModel _userInfo)
        {
            try
            {
                string sql = "EXECUTE SP_DELETE_USER @id, @deleted_by";

                List<SqlParameter> parms = new List<SqlParameter>
                { 
                    new SqlParameter { ParameterName = "@id", Value = id },
                    new SqlParameter { ParameterName = "@deleted_by", Value = _userInfo.Id },
                }; 

                var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region send OTP
        public async Task<bool> UpdateOTPUs(UserUpdateOTPModel userUpdateOTPModel)
        {
            try
            {
                string sql = "EXECUTE SP_UPDATE_USER_OTP @email, @otp, @expdate";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@email", Value = userUpdateOTPModel.Email },
                    new SqlParameter { ParameterName = "@otp", Value = int.Parse(userUpdateOTPModel.OTP) },
                    new SqlParameter { ParameterName = "@expdate", Value = userUpdateOTPModel.Expdate },
                };

                var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ForgotPassWord
        public async Task<bool> ForgotPassWordUs(ChangePassWordModel changePassWordModel)
        {
            try
            {
                string sql = "EXECUTE SP_FORGOT_PASSWORD_USER @email, @password, @salt";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@email", Value = changePassWordModel.Email },
                    new SqlParameter { ParameterName = "@password", Value = changePassWordModel.NewPassWord },
                    new SqlParameter { ParameterName = "@salt", Value = changePassWordModel.SaltKey },
                };

                var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region change password
        public async Task<bool> ChangePassWordRepo(ChangePassWordLoginSuccessModel changePassWordLoginSuccessModel)
        {
            string sql = "EXECUTE SP_CHANGE_PASSWORD_LOGIN @id, @passwordnew, @salt";

            List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@id", Value = changePassWordLoginSuccessModel.Id },
                    new SqlParameter { ParameterName = "@passwordnew", Value = changePassWordLoginSuccessModel.PassWordNew },
                    new SqlParameter { ParameterName = "@salt", Value = changePassWordLoginSuccessModel.SaltKey },
                };

            var dt = await context.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
            return true;
        }
        #endregion
    }
}
