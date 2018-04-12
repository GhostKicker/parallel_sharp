using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp25
{
    class Program
    {

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int k, n;
            k = int.Parse(Console.ReadLine());
            n = int.Parse(Console.ReadLine());
            int N = 0;

            Task<int>[] t = new Task<int>[k];
            int[] res = new int[k];

            for (int i = 0; i < k; i++)
            {
                res[i] = 0;
                t[i] = new Task<int>(x => Calc((int)x), n);
                t[i].Start();
            }

            for (int i = 0; i < k; i++)
            {
                N += t[i].Result;
            }


            Console.WriteLine((double)(4 * N) / (n * k));
            sw.Stop();
            Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString() + " seconds passed");
        }

        static int Calc(int n)
        {
            int res = 0;

            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random(i);

                double x = rnd.NextDouble();
                double y = rnd.NextDouble();

                if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) < 1)
                    res++;
                
            }
            return res;
        }

    }
}
