using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class Normalization
    {
        // Key = userID, Value = (ProductID, NormalizedRating)
        public static Dictionary<int, List<Tuple<int, double>>> NormalizedDictionary = new Dictionary<int, List<Tuple<int, double>>>();

        /// <summary>
        /// Normaliseren van ratings van users naar -1 tot 1
        /// </summary>
        /// <param name="userID">Gewenste UserId</param>
        public static void Normalize(int userID)
        {
            var user = FileReader.DictionaryData[userID];
            int maxRating = (int)user.Select(x => x.Item2).Max();
            int minRating = (int)user.Select(x => x.Item2).Min();
            foreach (var userRating in user)
            {
                var upperDev = userRating.Item2 - minRating;
                var lowerDev = maxRating - minRating;
                double normalizedRating = 2 * (upperDev / lowerDev) - 1;
                if (NormalizedDictionary.ContainsKey(userID))
                {
                    NormalizedDictionary[userID].Add(new Tuple<int, double>(userRating.Item1, normalizedRating));
                }
                else
                {
                    NormalizedDictionary.Add(userID, new List<Tuple<int, double>>() { new Tuple<int, double>(userRating.Item1, normalizedRating) });
                }
            }
        }
        public static double DeNormalize(int userID, int productID)
        {
            double deNormalizedRating = 0.0;
            var userNormalizeRatings = Normalization.NormalizedDictionary[userID];
            double upper = 0.0;
            double lower = 0.0;

            foreach (var userRatings in userNormalizeRatings)
            {
                double upperRn = userRatings.Item2;
                double sim = Cosinus.GetSimilarityValue(productID, userRatings.Item1);
                lower += Math.Abs(sim);
                upper += (upperRn * sim);
            }

            deNormalizedRating = upper / lower;
            return deNormalizedRating;
        }
    }
}
