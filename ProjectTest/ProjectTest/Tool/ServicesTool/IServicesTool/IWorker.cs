namespace ProjectTest.Tool.ServicesTool.IServicesTool
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
