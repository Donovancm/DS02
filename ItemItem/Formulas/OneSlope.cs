using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    public class OneSlope
    {
        public static List<Tuple<double, int>> Deviations;
        // key productIdA, Values: productIdB, deviation, userAmount
        public static Dictionary<int, List<Tuple<int, double, int>>> DeviationDictionary = new Dictionary<int, List<Tuple<int, double, int>>>();

        /// <summary>
        /// Checkt of een combinatie van items bestaat
        /// </summary>
        /// <param name="productIdA">ProductId van A</param>
        /// <param name="productIdB">ProductId van B</param>
        /// <returns>True of False</returns>
        public static Boolean CombinationExist(int productIdA, int productIdB)
        {
            bool result = false;
            if (DeviationDictionary.ContainsKey(productIdA) && DeviationDictionary[productIdA].Exists(x => x.Item1 == productIdB))
            {
                result = true;
            }
            else
            {
                if (DeviationDictionary.ContainsKey(productIdB) && DeviationDictionary[productIdB].Exists(x => x.Item1 == productIdA))
                {
                    result = true;
                }
            }
            return result;
        }
        public static void AddDeviations(int userProductID)
        {
            var data = FileReader.DictionaryData;
            //gekozen product 103 + combinatie met die product 104/107/18/109
                foreach (var productIdB in FileReader.GetItemList())
                {
                    var productCombinationRating = FileReader.DictionaryData.Where(x => x.Value.Any(a => a.Item1 == userProductID) && x.Value.Any(b => b.Item1 == productIdB)).ToDictionary(d => d.Key, d => d.Value);
                    if (userProductID != productIdB && !CombinationExist((int)userProductID, (int)productIdB))
                    {
                        if (productCombinationRating.Count != 0)
                        {
                            Dictionary<int, List<Tuple<int, double>>> userList = FileReader.DictionaryData.Where(x => x.Value.Any(a => a.Item1 == userProductID) && x.Value.Any(b => b.Item1 == productIdB)).ToDictionary(d => d.Key, d => d.Value);
                            Tuple<double, int> deviations = CalculateDeviations((int)userProductID, (int)productIdB);
                            if (DeviationDictionary.ContainsKey((int)userProductID))
                            {
                                DeviationDictionary[(int)userProductID].Add(new Tuple<int, double, int>((int)productIdB, deviations.Item1, deviations.Item2));
                            }
                            else
                            {
                                DeviationDictionary.Add((int)userProductID, new List<Tuple<int, double, int>>() { new Tuple<int, double, int>((int)productIdB, deviations.Item1, deviations.Item2) });
                            }
                        }
                    }
            }
        }
        public static Tuple<double, int> FindDeviations(int itemA, int itemB)
        {
            if (DeviationDictionary.ContainsKey(itemA) && DeviationDictionary[itemA].Exists(x => x.Item1 == itemB))
            {
                var deviationA = DeviationDictionary[itemA].Find(x => x.Item1 == itemB);
                return new Tuple<double, int>(deviationA.Item2, deviationA.Item3);

            }
            var deviationB = DeviationDictionary[itemB].Find(x => x.Item1 == itemA);
            return new Tuple<double, int>(deviationB.Item2, deviationB.Item3);
        }

        public static Tuple<double, int> CalculateDeviations(int itemA, int itemB)
        {
            double sum = 0.0;
            double deviation = 0.0;

            var dataFilter = FileReader.DictionaryData.Where(x => x.Value.Any(a => a.Item1 == itemA) && x.Value.Any(b => b.Item1 == itemB)).ToDictionary(d => d.Key, d => d.Value);
            foreach (var userRatings in dataFilter)
            {
                var ratingA = userRatings.Value.Find(a => a.Item1 == itemA).Item2;
                var ratingB = userRatings.Value.Find(a => a.Item1 == itemB).Item2;
                if (itemA < itemB)
                {
                    sum += ratingA - ratingB;
                }
                else
                {
                    sum += ratingB - ratingA;
                }

            }
            deviation = sum / dataFilter.Count();
            Console.WriteLine("ItemA: " + itemA + " ItemB: " + itemB + " deviation: " + deviation + " NumberofUsers: " + dataFilter.Count());
            return new Tuple<double, int>(deviation, dataFilter.Count());
        }
        public static double PredictRating(int userId, int productId)
        {
           
            double upper = 0.0;
            userId = 2;
            productId = 2;
            double lower = 0.0;
            var userData = FileReader.DictionaryData;
            var data = FileReader.DictionaryData.Values;
            var productlistUser = FileReader.DictionaryData[userId];
            AddDeviations(productId);
            foreach (var userProduct in productlistUser)
            {
                //List<Tuple<int, double>> productlistUser = userData[userId].Where(x => x.Item2 != 0).ToList();
                if (productId != userProduct.Item1)
                {
                    int itemA = userProduct.Item1;
                    if (DeviationDictionary.ContainsKey(itemA) && DeviationDictionary[itemA].Exists(x => x.Item1 == productId) ||
                        DeviationDictionary.ContainsKey(productId) && DeviationDictionary[productId].Exists(x => x.Item1 == itemA))
                    {
                        Tuple<double, int> deviationsPair = FindDeviations(productId, userProduct.Item1);
                        double deviation = deviationsPair.Item1;
                        int countUsers = deviationsPair.Item2;
                        if (deviation != 0 && countUsers > 0)
                        {
                            upper += (userProduct.Item2 - deviation) * countUsers;
                            lower += countUsers;
                        }
                    }
                }
            }
            double result = upper / lower;
            Console.WriteLine("Predicted Rating for User " + userId + " for the product " + productId + " is: " + result);
            return result;
        }
    }
}
