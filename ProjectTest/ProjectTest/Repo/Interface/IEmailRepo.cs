using ProjectTest.Data;
using ProjectTest.Model;

namespace ProjectTest.Repo.Interface
{
    public interface IEmailRepo
    {
        Task<List<Email>> GetAllEmail(EmailSearchModel emailSearchModel);
        List<Email> CheckEmail(string email);
        Task<bool> CreateEmailR(EmailCrModel cre);
        List<Email> GetDetailEmailR(int id);
        Task<bool> DeleteEmailR(int id, CurrentUserModel _userInfo);
        List<Email> CheckAllEmail();

    }
}
