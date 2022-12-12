using Microsoft.EntityFrameworkCore;
using ProjectTest.Data;
using ProjectTest.Repo.Interface;

namespace ProjectTest.Repo
{
    public class DataEmailRepo: IDataEmailRepo
    {
        private readonly SqlDbContext _context;

        public DataEmailRepo(SqlDbContext context)
        {
            _context = context;
        }
        public DataEmail CheckDataEmail()
        {
            //List<DataEmail> data;
            string sql = "EXEC SP_CHECK_DATA_EMAIL";
            var data = _context.DataEmail.FromSqlRaw<DataEmail>(sql).ToList().FirstOrDefault();
            return data;
        }
        public DataEmail CheckDataEmailAuto()//AutoSendMail
        {
            ////List<DataEmail> data;
            //string sql = "EXEC SP_CHECK_DATA_EMAIL";
            //var data = _context.DataEmail.FromSqlRaw<DataEmail>(sql).ToList().FirstOrDefault();
            //return data;
            return null;
        }
    }
}
