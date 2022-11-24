using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTest.Common;
using ProjectTest.Model;

namespace ProjectTest.Services.Interface
{
    public interface ILoginService
    {
        public ResultModel Login(InputLoginModel inputModel);
        public Task<ResultModel> ForgotPassWordAsync(ForgotPassWordModel forgotPassWordModel);
    }
}
