
using System.Diagnostics;
using System.Security.Cryptography;

int N = 8*2*2*2;

var stopWatch = Stopwatch.StartNew();
{
    HeavyLoad();
}
stopWatch.Stop();
Console.WriteLine($"{N} threads sleeps for 1sec => {stopWatch.Elapsed}");

//stopWatch = Stopwatch.StartNew();

//{
//    Thread[] threads = new Thread[N];
//    for(int i = 0; i < N; i++)
//    {
//        threads[i] = new Thread((_) =>
//            {
//                HeavyLoad();
//                //Thread.Sleep(TimeSpan.FromSeconds(1));
//            }
//        );

//        threads[i].Start();
//    }
//    for (int i = 0; i < N; i++)
//    {
//        threads[i].Join();
//    }
//}
//stopWatch.Stop();
//Console.WriteLine($"{N} threads sleeps for 1sec => {stopWatch.Elapsed}");

stopWatch = Stopwatch.StartNew();

{
    Task[] tasks = new Task[N];
    for(int i = 0; i < N; i++)
    {
        tasks[i] = new Task(() =>
            {
                HeavyLoad();
                //Thread.Sleep(TimeSpan.FromSeconds(1));
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
        );

        tasks[i].Start();
    }
    Task.WaitAll(tasks);
}
stopWatch.Stop();
Console.WriteLine($"{N} tasks sync   sleeps for 1sec => {stopWatch.Elapsed}");

stopWatch = Stopwatch.StartNew();

{
    Task[] tasks = new Task[N];
    for(int i = 0; i < N; i++)
    {
        tasks[i] = new Task(async() =>
            {
                HeavyLoad();
                //Thread.Sleep(TimeSpan.FromSeconds(1));
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        );

        tasks[i].Start();
    }
    Task.WaitAll(tasks);
}
stopWatch.Stop();
Console.WriteLine($"{N} tasks async  sleeps for 1sec => {stopWatch.Elapsed}");


void HeavyLoad()
{
    var rand = new Random();
    byte[] bytes = new byte[256];
    for (int i = 0; i < 256; i++)
    {
        bytes[i] = (byte)rand.Next(0, 256);
    }
    for (int i = 0; i < 1_000_000; i++)
    {
        MD5.HashData(bytes);
    }
}