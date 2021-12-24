using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuationTasksDemo
{
    internal class Program
    {
        static List<int> DeleteDuplicateValues(List<int> nums)
        {
            List<int> numbers = nums as List<int>;
            return numbers.Distinct().ToList();
        }
        static List<int> SortValues(List<int> nums)
        {
            //List<int> numbers = nums as List<int>;
            var ordered = from i in nums
                          orderby i
                          select i;
            return ordered.ToList();
        }
        static void DisplayList(object nums)
        {
            List<int> numbers = nums as List<int>;
            for(int i=0;i<numbers.Count; i++)
            {
                Console.Write($"{numbers[i]} ");
            }
            Console.WriteLine();
        }

        static int BinarySearch(List<int> numbers,int ToFind)
        {
            int left = 0;
            int right = numbers.Count-1;
            while (left <= right)
            {
                var mid = (right +left) / 2;
                if (ToFind == numbers[mid])
                {
                    return mid;
                }
                else if (ToFind < numbers[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            int valueToFind = 17;

            Console.WriteLine("Main thread start");
            List<int> numbers = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add(random.Next(1001));
            }

            Task<List<int>> task1 = new Task<List<int>>(() => { return DeleteDuplicateValues(numbers); });
            task1.Start();

            //Task task2 = task1.ContinueWith(sum => Display(sum.Result));
            Task<List<int>> task2 = task1.ContinueWith((res) =>  SortValues(res.Result) );

            Task<int> task3 = task2.ContinueWith((res) =>  BinarySearch(res.Result, valueToFind) );

            DisplayList(task2.Result);

            task3.Wait();
            Console.WriteLine("========================");
            if(task3.Result!=-1)
                Console.WriteLine($"{valueToFind} finded on {task3.Result}");
            else
                Console.WriteLine($"{valueToFind} not finded");
            Console.WriteLine("========================");

            Console.ReadKey();
            Console.WriteLine("Main thread end");
        }
    }
}
