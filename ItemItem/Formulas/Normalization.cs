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
    }
}
