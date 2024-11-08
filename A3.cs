using System;
using System.Collections.Generic;

namespace Assignment3
{
    /// <summary>
    /// Library of statistical functions using Generics for different statistical calculatuions
    /// see http://www.calculator.net/standard-deviation-calculator.html for sample standard deviation calculations
    ///  
    /// @author Joey Programmer
    /// </summary>

    public class A3
    {
        public static double average(List<double> x, bool incneg)
        {
            double s = sum(x, incneg);
            int c = 0;
            for (int i = 0; i < x.Count; i++)
            {
                if (incneg || x[i] >= 0)
                {
                    c++;
                }
            }
            if (c == 0)
            {
                throw new ArgumentException("x contains no values are > 0");
            }
            return s / c;
        }

        public static double sum(List<double> x, bool incneg)
        {
            if (x.Count < 0)
            {
                throw new ArgumentException("x cannot be empty");
            }

            double sum = 0.0;
            foreach (double val in x)
            {
                if (incneg || val >= 0)
                {
                    sum += val;
                }
            }
            return sum;
        }

        public static double median(List<double> data)
        {
            if (data.Count == 0)
            {
                throw new ArgumentException("data cannot be empty");
            }

            data.Sort();

            double median = data[data.Count / 2];
            if (data.Count % 2 == 0)
                median = (data[data.Count / 2] + data[data.Count / 2 - 1]) / 2;

            return median;
        }

        public static double stdev(List<double> data)
        {
            if (data.Count <= 1)
            {
                throw new ArgumentException("data cannot be empty");
            }

            int n = data.Count;
            double s = 0;
            double a = average(data, true);

            for (int i = 0; i < n; i++)
            {
                double v = data[i];
                s += Math.Pow(v - a, 2);
            }
            double stdev = Math.Sqrt(s / (n - 1));
            return stdev;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Sample Output for Statistical Functions Library");
            List<Double> testDataD = new List<Double> { 2.2, 3.3, 66.2, 17.5, 30.2, 31.1 };
            Console.WriteLine("The sum of the array = {0}", A3.sum(testDataD, true));
            Console.WriteLine("The average of the array = {0}", A3.average(testDataD, true));
            Console.WriteLine("The median value of the Double data set = {0}", A3.median(testDataD));
            Console.WriteLine("The sample standard deviation of the Double test set = {0}\n", A3.stdev(testDataD));
            Console.WriteLine("Press enter key to exit...");
            Console.ReadLine();
        }
    }


}
