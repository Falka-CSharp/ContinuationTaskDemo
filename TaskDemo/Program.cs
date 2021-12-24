using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDemo
{
    internal class Program
    {
        static int FindMin(List<int> numbers)
        {
            int min = 0;
            if (numbers.Count > 0)
            {
                min = numbers[0];
                for(int i = 0; i < numbers.Count; i++)
                {
                    if (min > numbers[i])
                    {
                        min = numbers[i];
                    }
                }
            }
            return min;
        }
        static int FindMax(List<int> numbers)
        {
            int max = 0;

            return max;
        }

        static int FindAvg(List<int> numbers)
        {
            int avg = 0;

            return avg;
        }

        static int FindSum(List<int> numbers)
        {
            int sum = 0;

            return sum;
        }

        static void Main(string[] args)
        {
            List<int> numbers= new List<int>();
            Random random= new Random();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add(random.Next(1000));
            }
            Task<int>[] tasks = new Task<int>[4];
            tasks[0] = new Task<int>(() => { return FindMin(numbers); });
            tasks[1] = new Task<int>(() => { return FindMax(numbers); });
            tasks[2] = new Task<int>(() => { return FindAvg(numbers); });
            tasks[3] = new Task<int>(() => { return FindSum(numbers); });

            for(int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Start();
            }
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Wait();
            }
            Console.WriteLine("========RESULT========");
            Console.WriteLine($"Min: {tasks[0].Result}");
            Console.WriteLine($"Max: {tasks[1].Result}");
            Console.WriteLine($"Avg: {tasks[2].Result}");
            Console.WriteLine($"Sum: {tasks[3].Result}");
            Console.WriteLine("======================");
            Console.ReadKey();
        }
    }
}
