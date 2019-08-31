using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class Prediction
    {
        /// <summary>
        /// Rating to normalized, normalized ratings converting to new denormalized rating to calculate prediction 
        /// </summary>
        /// <param name="devMatrix">Table of deviations similarity</param>
        /// <param name="normalizationMatrix">table of normalized ratings</param>
        /// <param name="item">product</param>
        /// <param name="itemList">list of products</param>
        /// <param name="userID">user id</param>
        /// <returns>double prediction value</returns>
        public static double CalculatePrediction(double[,] devMatrix, double[,] normalizationMatrix, int item, double[] itemList, int userID )
        {
            var numerator = 0.0;
            var denominator = 0.0;
            var prediction = 0.0;
            for (int i = 2; i <= normalizationMatrix.GetLength(1) - 3; i++)
            {
                if (item != itemList[i-2])
                {
                    double sim = devMatrix[(0), (i - 2)];
                    double rn = normalizationMatrix[userID-1, i];
                    numerator = numerator+( rn * sim);
                    denominator = denominator+ Math.Abs(sim);
                }
            }
            prediction = numerator / denominator;
            double r = ((prediction + 1) / 2) * (normalizationMatrix[userID - 1, normalizationMatrix.GetLength(1)-1] - normalizationMatrix[userID - 1, normalizationMatrix.GetLength(1)-2]) + 1;
            return r;
        }
    }
}
