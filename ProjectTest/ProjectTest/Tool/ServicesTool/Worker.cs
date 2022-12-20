using ProjectTest.Model;
using ProjectTest.Repo.Interface;
using ProjectTest.Services.Interface;
using ProjectTest.Tool.ServicesTool.IServicesTool;

namespace ProjectTest.Tool.ServicesTool
{
    public class Worker : IWorker
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        //private int number = 0;
        public async Task DoWork(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var sendMailService = scope.ServiceProvider.GetService<ISendMailService>();
                var dataMailRepo = scope.ServiceProvider.GetService<IDataEmailRepo>();
                var userRepo = scope.ServiceProvider.GetService<IUserRepo>();
                var emailRepo = scope.ServiceProvider.GetService<IEmailRepo>();
                var allEmail = emailRepo.CheckAllEmail();
                var dataEmail = dataMailRepo.CheckDataEmailAuto();
                try
                {
                    if ((allEmail!= null ? allEmail.Count() > 0 : allEmail!= null) && dataEmail != null)
                    {
                        List<EmailModel> lst = new List<EmailModel>();
                        var list = allEmail.Select(x => new EmailModel()
                        {
                            Email = x.EmailAddress +';' + x.CC,
                        }).ToList();
                        var strEmail = string.Join(", ", list.Select(x => x.Email).ToArray());
                        EmailDto inputEmail = new EmailDto()
                        {
                            To = strEmail,
                            Subject = dataEmail.Subject,
                            Body = dataEmail.Body,
                        };
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            //Interlocked.Increment(ref number);
                            //_logger.LogInformation($"Test: {number}");
                            var sendMailAuto = await sendMailService.SendMailAsync(inputEmail);
                            await Task.Delay(1000 * 3);
                        }
                    }
                    else return;
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }
    }
}
