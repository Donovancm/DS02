using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Matrices
{
    class CreateMatrix
    {
        // Key = UserID, Value = (AverageRating))
        public static Dictionary<int, double> AverageRatingDictionary = new Dictionary<int, double>();
        // Key = UserID,  Value = (ProductID, UserRating)
        public static Dictionary<int, List<Tuple<int, double>>> MatrixDictionary = new Dictionary<int, List<Tuple<int, double>>>();
        /// <summary>
        ///  Calculate average rating of selected products
        /// </summary>
        /// <param name="matrix">product rating</param>
        public static void CalculateAverageRating(double[,] matrix)
        {
            
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                double sumrating = 0;
                double totalItems = 0;
                for (int j = 2; j <= matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i,j] > 0)
                    {
                        totalItems++;
                        sumrating += matrix[i, j];
                    }
                }
                matrix[i, 1] = sumrating / totalItems;
            }
        }

        public static void CalculateAverageRating_New()
        {
            foreach (var userRatings in FileReader.DictionaryData)
            {
                double sumRating = userRatings.Value.Sum(r => r.Item2);
                int totalProductRating = userRatings.Value.Count();
                AverageRatingDictionary.Add(userRatings.Key, (sumRating / totalProductRating));
            }
        }

        public static void Create_New(double[] itemList)
        {
            List<int> users = new List<int>(FileReader.DictionaryData.Keys);
            foreach (var id in users)
            {
                List<Tuple<int, double>> userProducts = FileReader.DictionaryData[id];
                foreach (var productId in itemList)
                {
                    try
                    {
                        var userProductId = userProducts.Find(x => x.Item1 == productId).Item2;
                        if (MatrixDictionary.ContainsKey(id))
                        {
                           MatrixDictionary[id].Add(new Tuple<int, double>((int)productId, userProductId));
                        }
                        else
                        {
                           MatrixDictionary.Add(id, new List<Tuple<int, double>>() { new Tuple<int, double>((int)productId, userProductId) });
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        if (MatrixDictionary.ContainsKey(id))
                        {
                            MatrixDictionary[id].Add( new Tuple<int, double>((int)productId, 0) );
                        }
                        else
                        {
                            MatrixDictionary.Add(id, new List<Tuple<int, double>>() { new Tuple<int, double>((int)productId, 0) });
                        }
                    }
                  
                }
            }
        }
    }
}
