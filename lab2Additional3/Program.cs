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
        static double curstart;
        static double elemseg = 1e-5;
        static int numtasks;
        static double global_result = 0;

        static void Main(string[] args)
        {
            for (int i = 1; i <= 2000; ++i)
            {

            }

            //-----  INPUT START  -----

            Stopwatch stop = new Stopwatch();
            stop.Start();

            double start, end;
            start = 0;
            end = 50;
            
            curstart = start;
            //-----  INPUT END  -----

            int num_segs = Convert.ToInt32(Math.Round((end - start) / elemseg));
            if (start + num_segs * elemseg < end)
                num_segs++;


            int num_of_full_threads = numtasks - num_segs % numtasks;
            int num_works = num_segs / numtasks;
            if (num_of_full_threads != numtasks) num_works++;

            Task<double>[] t = new Task<double>[numtasks];

            for (int i = 0; i < num_of_full_threads; i++)
            {
                Tuple<double, int, double> arg = new Tuple<double, int, double>(curstart, num_works, elemseg);
                t[i] = new Task<double>(x => Calc((Tuple<double, int, double>)x), arg);
                t[i].Start();
                curstart += num_works * elemseg;
            }
            for (int i = num_of_full_threads; i < numtasks; i++)
            {
                Tuple<double, int, double> arg = new Tuple<double, int, double>(curstart, num_works - 1, elemseg);
                t[i] = new Task<double>(x => Calc((Tuple<double, int, double>)x), arg);
                t[i].Start();
                curstart += (num_works - 1) * elemseg;
            }

            for (int i = 0; i < numtasks; i++)
            {
                global_result += t[i].Result;
            }

            stop.Stop();

            Console.Write("Time: " + stop.ElapsedMilliseconds.ToString() + " ");
            Console.Write("My result is: ");
            Console.WriteLine(global_result);
        }

        static double Calc(Tuple<double, int, double> start_works_elemseg)
        {
            double res = 0;
            double start = start_works_elemseg.Item1;
            int works = start_works_elemseg.Item2;
            double elemseg = start_works_elemseg.Item3;

            for (int i = 0; i < works; i++, start += elemseg)
            {
                res += Math.Pow(Math.Sin(start), 2) * elemseg;
            }

            return res;
        }

    }
}
