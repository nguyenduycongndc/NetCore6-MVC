using ProjectTest.Tool.ServicesTool.IServicesTool;

namespace ProjectTest.Tool
{
    public class DerivedBackgroundPrinter : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IWorker _worker;
        public DerivedBackgroundPrinter(IWorker worker, IHostApplicationLifetime hostApplicationLifetime)
        {
            _worker = worker;
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _worker.DoWork(stoppingToken);
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}
