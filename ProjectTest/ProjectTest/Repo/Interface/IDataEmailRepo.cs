using ProjectTest.Common;
using ProjectTest.Data;
using ProjectTest.Model;

namespace ProjectTest.Repo.Interface
{
    public interface IDataEmailRepo
    {
        DataEmail CheckDataEmail();
        DataEmail CheckDataEmailAuto();
        Task<bool> CrUpDataEmail(DataEmailModel dataEmailModel, CurrentUserModel _userInfo);
        DataEmail DataEmailDetail();
        //Task<bool> UpdateDataEmail(DataEmailModel dataEmailModel, CurrentUserModel _userInfo);
    }
}
