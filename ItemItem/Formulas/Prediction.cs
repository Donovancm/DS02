using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class Prediction
    {
        public static double CalculatePrediction_New(int userID, int productID)
        {
            var prediction = 0.0;

            var userNormalizeRatings = Normalization.NormalizedDictionary[userID];

            double upper = 0.0;
            double lower = 0.0;
            foreach (var userRatings in userNormalizeRatings)
            {
                //productID = 103
                double upperRn = userRatings.Item2; // 106 , rn = 5
                double sim = GetSimilarityValue(productID, userRatings.Item1); // 103 & 106
                lower += Math.Abs(sim);
                upper += (upperRn * sim);
            }

            prediction = upper / lower;
            int maxRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Max();
            int minRating = (int)FileReader.DictionaryData[userID].Select(x => x.Item2).Min();
            double r = ((prediction + 1) / 2) * ( maxRating - minRating ) + 1;
            return r;
        }

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
