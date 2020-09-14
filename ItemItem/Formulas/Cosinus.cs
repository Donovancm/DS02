using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class Cosinus
    {
        //Dictionary op  key => int:product1, <int:product2, double:sim, int:users.Count()> 
        public static Dictionary<int, List<Tuple<int, double, int>>> SimilarityDictionary = new Dictionary<int, List<Tuple<int, double, int>>>();

        /// <summary>
        /// Checkt of een combinatie van items bestaat
        /// </summary>
        /// <param name="productIdA">ProductId van A</param>
        /// <param name="productIdB">ProductId van B</param>
        /// <returns>True of False</returns>
        public static Boolean CombinationExist(int productIdA, int productIdB)
        {
            bool result = false;
            if (SimilarityDictionary.ContainsKey(productIdA) && SimilarityDictionary[productIdA].Exists(x => x.Item1 == productIdB))
            {
                result = true;
            }
            else
            {
                if (SimilarityDictionary.ContainsKey(productIdB) && SimilarityDictionary[productIdB].Exists(x => x.Item1 == productIdA))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        ///  Het toevoegen van Similarity door ACS 
        /// </summary>
        /// <param name="userProductID">Het gewenste productId</param>
        public static void ACS(int userProductID)
        {
            //gekozen product 103 + combinatie met die product 104/107/18/109
            foreach (var productIdB in FileReader.GetItemList())
            {
                if (userProductID != productIdB && !CombinationExist((int)userProductID, (int)productIdB))
                {
                    Dictionary<int, List<Tuple<int, double>>> userList = FileReader.DictionaryData.Where(x => x.Value.Any(a => a.Item1 == userProductID) && x.Value.Any(b => b.Item1 == productIdB)).ToDictionary(d => d.Key, d => d.Value);
                    double similarity = CalculateACS(userList, (int)userProductID, (int)productIdB);
                    if (SimilarityDictionary.ContainsKey((int)userProductID))
                    {
                        SimilarityDictionary[(int)userProductID].Add(new Tuple<int, double, int>((int)productIdB, similarity, userList.Count()));
                    }
                    else
                    {
                        SimilarityDictionary.Add((int)userProductID, new List<Tuple<int, double, int>>() { new Tuple<int, double, int>((int)productIdB, similarity, userList.Count()) });
                    }
                }
            }
        }

        /// <summary>
        /// Het berekenen van ACS per combinatie
        /// </summary>
        /// <param name="userData">Gefilteerde data van users</param>
        /// <param name="itemA">ProductId A</param>
        /// <param name="itemB">ProductId B</param>
        /// <returns>resultaat in double</returns>
        public static double CalculateACS(Dictionary<int, List<Tuple<int, double>>> userData, int itemA, int itemB)
        {
            double upper = 0.0;
            foreach (var user in userData)
            {

                double userAgvRating = AverageRating.AverageRatingDictionary[user.Key];
                double upperProductARating = user.Value.Find(u => u.Item1 == itemA).Item2;
                double upperProductBRating = user.Value.Find(u => u.Item1 == itemB).Item2;
                upper += (upperProductARating - userAgvRating) * (upperProductBRating - userAgvRating);
            }
            double lowerProductARating = 0.0;
            double lowerProductBRating = 0.0;
            foreach (var user in userData)
            {
                // (Prod)
                double userAgvRating = AverageRating.AverageRatingDictionary[user.Key];
                lowerProductARating += Math.Pow((user.Value.Find(x => x.Item1 == itemA).Item2 - userAgvRating),2);
            }

            foreach (var user in userData)
            {
                // (Prod)
                double userAgvRating = AverageRating.AverageRatingDictionary[user.Key];
                lowerProductBRating += Math.Pow((user.Value.Find(x => x.Item1 == itemB).Item2 - userAgvRating),2);
            }
            var result = (upper / (Math.Sqrt(lowerProductARating) * Math.Sqrt(lowerProductBRating)));
            return result;
        }
    }
}
