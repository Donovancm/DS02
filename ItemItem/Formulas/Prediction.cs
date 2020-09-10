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
            var prediction = 0.0;
            var userNormalizeRatings = Normalization.NormalizedDictionary[userID];
            double upper = 0.0;
            double lower = 0.0;

            foreach (var userRatings in userNormalizeRatings)
            {
                double upperRn = userRatings.Item2; 
                double sim = GetSimilarityValue(productID, userRatings.Item1); 
                lower += Math.Abs(sim);
                upper += (upperRn * sim);
            }
            prediction = upper / lower;
            int maxRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Max();
            int minRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Min();
            double r = ((prediction + 1) / 2) * ( maxRating - minRating ) + 1;
            return r;
        }

        /// <summary>
        ///  Pakt de gewenste productId en combinatie met een andere productId
        /// </summary>
        /// <param name="productID">Gewenste ProductId</param>
        /// <param name="item1">Andere productId</param>
        /// <returns>Similarity in double</returns>
        private static double GetSimilarityValue(int productID, int item1)
        {
            double similarity = 0.0;
            if (Cosinus.SimilarityDictionary.ContainsKey(productID) && Cosinus.SimilarityDictionary[productID].Any(x => x.Item1 == item1))
            {
                similarity = Cosinus.SimilarityDictionary[productID].Find(x => x.Item1 == item1).Item2;
            }
            else
            {
                similarity = Cosinus.SimilarityDictionary[item1].Find(x => x.Item1 == productID).Item2;
            }
            return similarity;
        }
    }
}
