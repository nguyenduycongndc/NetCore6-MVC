using ProjectTest.Common;
using ProjectTest.Model;

namespace ProjectTest.Services.Interface
{
    public interface IUserService
    {
        Task<ResultModel> GetAllUser(SearchUserModel searchUserModel);
        Task<ExportUserModel> GetAllExport(SearchUserModel searchUserModel);
        public Task<ResultModel> CreateUser(CreateModel input, CurrentUserModel _userInfo);
        public Task<ResultModel> UpdateUser(UpdateModel updateModel, CurrentUserModel _userInfo);
        public ResultModel GetDetailModels(int id);
        //public CurrentUserModel GetDetailModels(int id);
        public Task<ResultModel> DeleteUser(int id, CurrentUserModel _userInfo);
    }
}
