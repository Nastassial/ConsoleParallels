using System.Diagnostics;

int tasksCount = 10, startIndex = 0, endIndex = 0, subArrayLength;
long resultSum = 0;

int[] numbers = new int[10_000_000];

Random random = new Random();

Stopwatch sw = new Stopwatch();


for (int i = 0; i < numbers.Length; i++)
{
    numbers[i] = random.Next(-10_000, 10_001);
}

subArrayLength = (int)Math.Ceiling(numbers.Length / (decimal)tasksCount);

Task<long>[] tasks = new Task<long>[tasksCount];

sw.Start();

for (int i = 0; i < tasks.Length; i++)
{
    startIndex = endIndex;
    endIndex = (i == tasks.Length - 1) ? numbers.Length : startIndex + subArrayLength;

    tasks[i] = Task.Run(() => Sum(numbers, startIndex, endIndex));

    resultSum += tasks[i].Result;
}

Console.WriteLine("resultSum = " + resultSum);

sw.Stop();

Console.WriteLine(sw.ElapsedMilliseconds);


long Sum(int[] numbers, int startIndex, int endIndex)
{
    long sum = 0;

    for (int i = startIndex; i < endIndex; i++)
    {
        sum += numbers[i];
    }

    return sum;
}