using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lab11.Models
{
    public class Algorithms
    {
        private double[] arr;
        private int n;
        public Stopwatch stopwatch1 = new Stopwatch();
        public Stopwatch stopwatch2 = new Stopwatch();
        public Stopwatch stopwatch3;
        public double effectivity;
        public double acceleration;
        public double amdal;
        public double gustavson_barsis;

        int counterLinear = 0;
        int counterParallel = 0;

        public Algorithms()
        {

        }
        public Algorithms(double[] arr)
        {
            this.arr = arr;
            n = arr.Length;
        }

        public double CascadeSummationScheme()
        {
            stopwatch1.Start();
            double[] arrCopy = arr;
            double sum = 0;

            int k = 0;
            int q = 0;

            while (k < Math.Log2(n))
            {
                q = (int)Math.Pow(2, k);
                Parallel.For(q, n, (i) =>
                {
                    stopwatch2.Start();
                    arr[i] += arrCopy[i - q];
                    arr[i - q] = 0;
                    counterParallel++;
                    stopwatch2.Stop();
                });
                counterLinear++;
                k++;
            }
            stopwatch2.Start();
            Parallel.ForEach(arr, (s) => { sum += s; });
            stopwatch2.Stop();
            stopwatch1.Stop();

            effectivity = (n - 1) / ((n - 2) * Math.Log2(n));
            acceleration = (n - 1) / Math.Log2(n);

            double p = counterLinear / (counterLinear + counterParallel);
            var processorCount = 4;

            amdal = 1 / (p + ((1 - p) / processorCount));
            gustavson_barsis = processorCount + (1 - processorCount) * p;

            return sum;

        }
        public double ModifyCascadeSummationScheme()
        {
            stopwatch3 = new Stopwatch();
            stopwatch1.Start();
            int subArrCount = (int)(n / Math.Log2(n));
            int itemsCount = (int)Math.Log2(n);

            double[] subGroupSum = new double[subArrCount];

            int k = 0;
            stopwatch2.Start();
            Parallel.For(0, subArrCount, (i) =>
            {
                stopwatch3.Start();
                for (int j = 0; j < itemsCount; j++)
                {
                    subGroupSum[i] += arr[k];
                    k++;
                    counterParallel++;
                }
                counterLinear++;
                stopwatch3.Stop();
            });
            stopwatch2.Stop();
            Algorithms temp = new Algorithms(subGroupSum);
            temp.stopwatch1 = stopwatch1;
            temp.stopwatch2 = stopwatch2;

            var result = temp.CascadeSummationScheme();

            effectivity = (n - 1) / 2 * n;
            acceleration = (n - 1) / 2 * Math.Log2(n);


            counterLinear += temp.counterLinear;
            counterParallel += temp.counterParallel;

            double p = counterLinear / (counterLinear + counterParallel);
            var processorCount = 4;

            amdal = 1 / (p + ((1 - p) / processorCount));
            gustavson_barsis = processorCount + (1 - processorCount) * p;

            return result;
        }

        public double[] LinearReduction(int n)
        {
            StartData sd = new StartData();
            stopwatch3 = new Stopwatch();
            stopwatch1.Start();

            this.n = n;
            int l_last = (int)Math.Log2(n);

            double[,] a = new double[l_last, n];
            double[,] b = new double[l_last, n];
            double[] x = new double[n];
            for (int j = 0; j < n; j++)
            {
                a[0, j] = sd.A(j + 1);
                b[0, j] = sd.B(j + 1);
                counterLinear++;
            }

            for (int l = 1; l < l_last; l++)
            {
                stopwatch2.Start();
                Parallel.For(0, n, (j) =>
                {
                    stopwatch3.Start();
                    if ((j - (int)Math.Pow(2, l - 1)) < 0)
                    {
                        a[l, j] = 0;
                        b[l, j] = 0;
                    }
                    else
                    {
                        a[l, j] = a[l - 1, l] * a[l - 1, j - (int)Math.Pow(2, l - 1)];
                        b[l, j] = b[l - 1, l] * b[l - 1, j - (int)Math.Pow(2, l - 1)] + b[l - 1, j];
                    }
                    if ((j - (int)Math.Pow(2, l)) < 0)
                    {
                        x[j] = 0;
                    }
                    else
                    {
                        x[j] = a[l, j] * x[j - (int)Math.Pow(2, l)] + b[l, j];
                    }
                    stopwatch3.Stop();
                    counterParallel++;
                });
                stopwatch2.Stop();
                counterLinear++;
            }
            effectivity = 2 / (3 * Math.Log2(n));
            acceleration = 2 * n /( 3 * Math.Log2(n));

            stopwatch1.Stop();

            double p = counterLinear / (counterLinear + counterParallel);
            var processorCount = 4;

            amdal = 1 / (p + ((1 - p) / processorCount));
            gustavson_barsis = processorCount + (1 - processorCount) * p;

            return x;
        }
    }
}
