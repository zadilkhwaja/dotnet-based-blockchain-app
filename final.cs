public void Start()
{
    cancellationToken = new CancellationTokenSource();
    Task.Run(() => DoGenerateBlock(), cancellationToken.Token);
    Console.WriteLine("Mining has started");
}
public void Stop()
{
    cancellationToken.Cancel();
    Console.WriteLine("Mining has stopped");
}

private void DoGenerateBlock()
{
    while (true)
    {
        var startTime = DateTime.Now.Millisecond;
        GenerateBlock();
        var endTime = DateTime.Now.Millisecond;
        var remainTime = MINING_PERIOD - (endTime - startTime);
        Thread.Sleep(remainTime < 0 ? 0 : remainTime);
    }
}
