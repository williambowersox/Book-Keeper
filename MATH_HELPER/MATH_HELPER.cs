using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH_HELPER
{
    public static class MATH_HELPER
    {
        public readonly static string Name = "MATH_HELPER";
        public static float Delta_Change(float start, float after) { return after - start; }

    }
    public static class StandardDeviation
    {
        public static double DeviationAboutTheMean(List<double> sample)
        {
            double xbar = sample.Average();
            double deviationAboutTheMean = 0;
            foreach (int value in sample)
            { deviationAboutTheMean += ((double)value - xbar); }
            return deviationAboutTheMean;
        }

        public static double SquaredDeviationAboutTheMean(List<double> sample)
        {
            double xbar = sample.Average();
            double squaredDeviationAboutTheMean = 0;
            foreach (int value in sample)
            { squaredDeviationAboutTheMean += Math.Pow(((double)value - xbar), 2); }
            return squaredDeviationAboutTheMean;
        }

        public static double PopulationVariance(List<double> sample)
        { return SquaredDeviationAboutTheMean(sample) / (sample.Count); }

        public static double SampleVariance(List<double> sample)
        { return SquaredDeviationAboutTheMean(sample) / (sample.Count - 1); }

        public static double PopulationStandardDeviation(List<double> sample)
        { return Math.Sqrt(PopulationVariance(sample)); }

        public static double SampleStandardDeviation(List<double> sample)
        { return Math.Sqrt(SampleVariance(sample)); }
    }
}

