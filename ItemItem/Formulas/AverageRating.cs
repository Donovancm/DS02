using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class AverageRating
    {
        // Key = UserID, Value = (AverageRating))
        public static Dictionary<int, double> AverageRatingDictionary = new Dictionary<int, double>();

        /// <summary>
        /// Berekent de average rating van elke user per product
        /// </summary>
        public static void CalculateAverageRating()
        {
            foreach (var userRatings in FileReader.DictionaryData)
            {
                double sumRating = userRatings.Value.Sum(r => r.Item2);
                int totalProductRating = userRatings.Value.Count();
                AverageRatingDictionary.Add(userRatings.Key, (sumRating / totalProductRating));
            }
        }
    }
}
