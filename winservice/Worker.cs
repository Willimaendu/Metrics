namespace winservice;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly List<object> _myList = new();
    private static readonly Random Random = new();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (Random.Next(0, 20) == 7)
            {
                try
                {
                    throw new Exception();
                }catch(Exception) { }
            }
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _myList.AddRange(Enumerable.Range(1, 1000000).Select(_ => new object()));
            await Task.Delay(2000, stoppingToken);
        }
    }
}
