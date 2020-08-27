using ItemItem.Matrices;
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
        static double[] itemlist = null;
        static double[,] matrixACS = null;
        /// <summary>
        ///  Checks whether item id exist in user rating
        /// </summary>
        /// <param name="item">item id</param>
        /// <param name="userIndex">user id</param>
        /// <returns>Boolean</returns>
        public static Boolean CheckItem(int item, int userIndex)
        {

            double[] itemList = itemlist;
            double[,] Matrix = matrixACS;

            int indexFoundItem = 0;
            for (int a = 0; a <= itemList.Length - 1; a++)
            {
                if (itemList[a] == item)
                {
                    indexFoundItem = a + 2;
                    break;
                }
            }
            if (Matrix[userIndex, indexFoundItem] != 0)
            {
                return true;
            }
            return false;
        }

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

        public static void ACS(int userProductID)
        {
            //product 1 tegen product 2 
            //item list ->  
            //foreach (var productIdA in FileReader.GetItemList_New())
            //{
                //gekozen product 103 + combinatie met die product 104/107/18/109
                foreach (var productIdB in FileReader.GetItemList_New())
                {
                    if (userProductID != productIdB && !CombinationExist((int)userProductID, (int)productIdB))
                    {
                        Dictionary<int, List<Tuple<int,double>>> userList = FileReader.DictionaryData.Where(x => x.Value.Any(a => a.Item1 == userProductID) && x.Value.Any( b => b.Item1 == productIdB)).ToDictionary( d => d.Key, d => d.Value);
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
           // }
        }

        public static double CalculateACS(Dictionary<int, List<Tuple<int, double>>> userData, int itemA, int itemB)
        {
            double upper = 0.0;
            foreach (var user in userData)
            {

                double userAgvRating = CreateMatrix.AverageRatingDictionary[user.Key];
                double upperProductARating = user.Value.Find(u => u.Item1 == itemA).Item2;
                double upperProductBRating = user.Value.Find(u => u.Item1 == itemB).Item2;
                upper += (upperProductARating - userAgvRating) * (upperProductBRating - userAgvRating);
            }
            double lowerProductARating = 0.0;
            double lowerProductBRating = 0.0;
            foreach (var user in userData)
            {
                // (Prod)
                double userAgvRating = CreateMatrix.AverageRatingDictionary[user.Key];
                lowerProductARating += Math.Pow((user.Value.Find(x => x.Item1 == itemA).Item2 - userAgvRating),2);
            }

            foreach (var user in userData)
            {
                // (Prod)
                double userAgvRating = CreateMatrix.AverageRatingDictionary[user.Key];
                lowerProductBRating += Math.Pow((user.Value.Find(x => x.Item1 == itemB).Item2 - userAgvRating),2);
            }
            var result = (upper / (Math.Sqrt(lowerProductARating) * Math.Sqrt(lowerProductBRating)));
            return result;
        }

        /// <summary>
        ///  Performs the Adjusted Cosinus Formula
        /// </summary>
        /// <param name="matrix">table</param>
        /// <param name="itemI">product I</param>
        /// <param name="itemJ">product J</param>
        /// <param name="itemList">list of items</param>
        /// <returns>double array</returns>
        public static double[] ACS(double[,] matrix, int itemI, int itemJ, double[] itemList)
        {
            itemlist = itemList;
            matrixACS = matrix;
            double ratingItemI = 0.0;
            double ratingItemJ = 0.0;
            double userAverageRating = 0.0;
            int row = matrix.GetLength(0);

            double[] arraySumItemI = new double[row];
            double[] arraySumItemJ = new double[row];
            double leftDenominator = 0.0;
            double rightDenominator = 0.0;
            int indexItemI = 0;
            int indexItemJ = 0;
            double userAmount = 0;

            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= itemList.Length - 1; j++)
                {
                    int userIndex = i;
                    if (itemList[j] == itemI && matrix[i, j + 2] != 0)
                    {

                        if (CheckItem(itemJ,userIndex))
                        {
                            userAmount++;
                            ratingItemI = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arraySumItemI[indexItemI] = ratingItemI - userAverageRating;
                            leftDenominator += Math.Pow(ratingItemI - userAverageRating, 2);
                            indexItemI++;
                        }

                    }
                    else if (itemList[j] == itemJ && matrix[i, j + 2] != 0)
                    {
                        if (CheckItem(itemI,userIndex))
                        {
                            ratingItemJ = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arraySumItemJ[indexItemJ] = ratingItemJ - userAverageRating;
                            rightDenominator += Math.Pow(ratingItemJ - userAverageRating, 2);
                            indexItemJ++;
                        }
                    }
                }
            }
            double numerator = 0;
            double denominator = Math.Sqrt(leftDenominator) * Math.Sqrt(rightDenominator);
            for (int a = 0; a < arraySumItemI.Length - 1; a++)
            {
                numerator += arraySumItemI[a] * arraySumItemJ[a];
            }
            var result = numerator / denominator;
            double[] resultArray = new double[] { result, userAmount };
            return resultArray;
        }
    }
}
