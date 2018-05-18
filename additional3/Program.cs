
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static double[] semi_result;
        static double[,] matrix;
        static string readfrom = @"C:\Users\User\Documents\visual studio 2017\Projects\parallel_sharp\additional3\matrix.csv";
        static string writeto = @"C:\Users\User\Documents\visual studio 2017\Projects\parallel_sharp\additional3\out.csv";

        static void Main(string[] args)
        {
            string what = Console.ReadLine();
            //readfrom += Console.ReadLine();
            //filling the matrix
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");

            if (!File.Exists(readfrom))
            {
                Console.WriteLine("bad path");
                Console.ReadKey();
                return;
            }

            string[] string_matrix = File.ReadAllLines(readfrom);
            int le = string_matrix.Length;
            semi_result = new double[le];
            int i_m = 0;

            string[] forsize = string_matrix[0].Split(',');
            int le2 = forsize.Length;
            matrix = new double[le, le2];


            foreach (string line in string_matrix)
            {
                string[] line_s = line.Split(',');

                if (line_s.Length != le2)
                {
                    Console.WriteLine("bad matrix");
                    Console.ReadKey();
                    return;
                }

                int j = 0;
                foreach (string str in line_s)
                {
                    matrix[i_m, j] = Double.Parse(str, CultureInfo.InvariantCulture);
                    ++j;
                }
                ++i_m;
            }
            Console.WriteLine("filling done successfully");
            //-------------

            //calculating the stuff
            if (what == "plus")
                Parallel.For(0, le, (i) =>
                {
                    semi_result[i] = calc(ref matrix, i, le2);
                    Console.WriteLine("Calculated " + i.ToString() + "-th row");
                });
            if (what == "middle")
                Parallel.For(0, le, (i) =>
                {
                    semi_result[i] = calc2(ref matrix, i, le2);
                    Console.WriteLine("Calculated " + i.ToString() + "-th row");
                });

            Console.WriteLine("calculated successfully");
            //--------------------

            //writing result
            string[] result = new string[le];
            Parallel.For(0, le, (cur_row) =>
            {
                result[cur_row] = semi_result[cur_row].ToString();
                if (cur_row % 100 == 0)
                    Console.WriteLine("Reached " + cur_row.ToString() + "-th row");
            });
            File.WriteAllLines(writeto, result);
            Console.WriteLine("wrote result successfully");
            Console.ReadKey();
            //-------------------


        }

        static double calc(ref double[,] m, int row, int length) //calculating [i,j]-th cell of matrix
        {
            double res = 0;
            for (int col = 0; col < length; ++col)
                res += m[row, col];
            return res;
        }
        static double calc2(ref double[,] m, int row, int length) //calculating [i,j]-th cell of matrix
        {
            double res = 0;
            for (int col = 0; col < length; ++col)
                res += m[row, col];
            return res / length;
        }

    }

}