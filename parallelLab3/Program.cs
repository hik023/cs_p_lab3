using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace parallelLab3
{
    class Program
    {
        public static double eps = 1E-15;
        public static int p = 1;
        public static double[] y;
        static void Main(string[] args)
        {
                     
            Console.WriteLine("Введите количество потоков: ");
            p = Convert.ToInt32(Console.Read());
            double a = 1;
            double S = 0;
            y = new double[p];
            Thread[] mas = new Thread[p];
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int k = 0; k < p; k++)
            {
                int i = k + 1;
                a = 1 / ((4 * Convert.ToDouble(i) - 1) * (4 * Convert.ToDouble(i) + 1));
                mas[k] = new Thread(Count);
                mas[k].Start(i);
                S += a;
            }
            for (int k = 0; k < p; k++)
            {
                mas[k].Join();
                S += y[k];
            }
            watch.Stop();
            Console.WriteLine(S);
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadKey();
        }
        public static void Count(object i)
        {
            int ip = (int)i;
            int u = ip;
            double a = 1, S = 0;
            while (Math.Abs(a) > eps)
            {
                ip += p;
                a = 1 / ((4 * Convert.ToDouble(ip) - 1) * (4 * Convert.ToDouble(ip) + 1));
                S += a;
            }
            y[u - 1] = S;
        }
    }
}
