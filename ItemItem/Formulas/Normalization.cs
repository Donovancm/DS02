using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class Normalization
    {
        public static double[,] NormalizedMatrix(double[,] matrix)
        {
            var normalizedMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)+2];
            double rating = 0.0;
            double RatingMax = 0.0;
            double RatingMin = 0.0;
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                RatingMin = setMinRating(matrix, i);
                RatingMax = setMaxRating(matrix, i);
               
                normalizedMatrix[i, normalizedMatrix.GetLength(1)-2] = RatingMin;
                normalizedMatrix[i, normalizedMatrix.GetLength(1) - 1] = RatingMax;
                for (int j = 2; j <= matrix.GetLength(1) - 1; j++)
                {
                    rating = matrix[i, j];
                    if (rating >0)
                    {
                        var upperDev = rating - RatingMin;
                        var lowerDev = RatingMax - RatingMin;
                        double normalize = 2 * (upperDev / lowerDev) - 1;
                        normalizedMatrix[i, j] = normalize;
                    }
                }
            }
            return normalizedMatrix;
        }

        public static double setMinRating(double[,] matrix, int userIndex)
        {
            var data = matrix;
            var number = data.GetLength(1);
            List<double> list = new List<double>();
            //var array = new double[number -2];
            //var newArray = new double[number, 2];
            for (int i = 2; i <= data.GetLength(1) - 1; i++)
            {
                if (data[userIndex, i] >0)
                {
                    list.Add(data[userIndex, i]);
                }
                
            }
            double[] array = list.ToArray();
            Array.Sort(array);
            return array[0];
        }
        public static double setMaxRating(double[,] matrix, int userIndex)
        {
            var data = matrix;
            var number = data.GetLength(1);
            var array = new double[number - 1];
            //var newArray = new double[number, 2];
            for (int i = 1; i <= data.GetLength(1) - 1; i++)
            {
                if (data[userIndex, i] != 0)
                {
                    array[i - 1] = data[userIndex, i];
                }
            }
            Array.Sort(array);
            Array.Reverse(array);
            return array[0];
        }
    }
}
