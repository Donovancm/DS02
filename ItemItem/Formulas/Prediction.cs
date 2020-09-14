using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class Prediction
    {
        /// <summary>
        /// Berekent de prediction van de gewenste User product rating
        /// </summary>
        /// <param name="userID">Gewenste UserId</param>
        /// <param name="productID">Gewenste ProductId</param>
        /// <returns>Predictionrating van een product</returns>
        public static double CalculatePrediction(int userID, int productID)
        {
            double deNormalizedRating = Normalization.DeNormalize(userID, productID);
            int maxRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Max();
            int minRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Min();
            double r = ((deNormalizedRating + 1) / 2) * (maxRating - minRating) + 1;
            return r;
        }
    }
}
