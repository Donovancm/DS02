using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class Prediction
    {
        public static double CalculatePrediction(double[,] devMatrix, double[,] normalizationMatrix, int item, double[] itemList, int userID )
        {
            var upper = 0.0;
            var lower = 0.0;
            var prediction = 0.0;
            for (int i = 2; i <= normalizationMatrix.GetLength(1) - 3; i++)
            {
                if (item != itemList[i-2])
                {
                    double sim = devMatrix[userID -1, i - 2];
                    double nr = normalizationMatrix[userID-1, i];
                    upper += nr * sim;
                    lower += Math.Abs(sim);
                }
            }
            prediction = upper / lower;
            double r = ((prediction + 1) / 2) * (normalizationMatrix[userID - 1, normalizationMatrix.GetLength(1)-1] - normalizationMatrix[userID - 1, normalizationMatrix.GetLength(1)-2]) + 1;
            return r;
        }
    }
}
