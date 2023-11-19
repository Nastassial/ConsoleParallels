using System.Diagnostics;


int[] numbers = new int[10_000_000];

Random random = new Random();

for (int i = 0; i < numbers.Length; i++)
{
    numbers[i] = random.Next(-10_000, 10_001);
}

int tasksCount = 10;

Stopwatch sw = new Stopwatch();

sw.Start();

long resultSum = GetArrayTotalSum(numbers, tasksCount).Result;

sw.Stop();

Console.WriteLine($"TotalSum: {resultSum}\nTime: {sw.ElapsedMilliseconds} ms");


async Task<long> GetArrayTotalSum(int[] numbers, int tasksCount)
{
    int subArrayLength = (int)Math.Ceiling(numbers.Length / (decimal)tasksCount);

    Task<long>[] tasks = new Task<long>[tasksCount];

    for (int i = 0; i < tasks.Length; i++)
    {
        int startIndex = i * subArrayLength;
        int endIndex = (i == tasks.Length - 1) ? numbers.Length : startIndex + subArrayLength;

        tasks[i] = Task.Run(() => Sum(numbers, startIndex, endIndex));
    }

    long[] tasksResults = await Task.WhenAll(tasks);

    long resultSum = 0;

    foreach (long result in tasksResults)
    {
        resultSum += result;
    }

    return resultSum;
}

long Sum(int[] numbers, int startIndex, int endIndex)
{
    long sum = 0;

    for (int i = startIndex; i < endIndex; i++)
    {
        sum += numbers[i];
    }

    return sum;
}