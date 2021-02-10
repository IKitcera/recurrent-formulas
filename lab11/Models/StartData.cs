using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab11.Models
{
    public class StartData
    {
        public int n1 { get; } = 16;
        public int n2 { get; } = 32;
        public double[] array1()
        {
            double[] arr1 = new double[n1];
            for (int i = 0; i < n1; i++)
            {
                arr1[i] = x(i + 1);
            }
            return arr1;
        }
        public double[] array2()
        {
            double[] arr2 = new double[n2];
            for (int i = 0; i < n2; i++)
            {
                arr2[i] = x(i + 1);
            }
            return arr2;
        }
        private double x(int i)
        {
            return a(i) * b(i);
        }
        //.....................................................
        private double a(int i)
        {
            return ((Math.Pow(-1, i + 1) * (i + 1)) / 1.25 * Math.Pow(i, 2.5));
        }
        private double b(int i)
        {
            return (Math.Cos(Math.Pow(3, -10 * i)) - Math.Pow(2, i + 2) / factorial(i + 2));
        }
        private int factorial(int n)
        {
            if (n <= 1)
                return n;
            return n * factorial(n - 1);
        }
        public double A(int i)
        {
            return a(i);
        }
        public double B(int i)
        {
            return b(i);
        }
    }
}
