using ProjectTest.Tool.ServicesTool.IServicesTool;

namespace ProjectTest.Tool
{
    public class BackgroundPrinter : IHostedService /*, IDisposable*/
    {
        private readonly ILogger<BackgroundPrinter> _logger;
        private readonly IWorker _worker;
        public BackgroundPrinter(ILogger<BackgroundPrinter> logger, IWorker worker)
        {
            _logger = logger;
            _worker = worker;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("stop");
            return Task.CompletedTask;
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    timer = new Timer(o =>
        //    {
        //        Interlocked.Increment(ref number);
        //        _logger.LogInformation($"Test: {number}");
        //    },
        //        null,
        //        TimeSpan.Zero,
        //        TimeSpan.FromSeconds(5)
        //        );
        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
