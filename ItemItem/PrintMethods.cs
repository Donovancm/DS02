using ItemItem.Formulas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem
{
    class PrintMethods
    {
        /// <summary>
        /// Print de user en de averagerating per user
        /// </summary>
        /// <param name="tableHeaders">Headers</param>
        /// <param name="productIdList">Lijst van productId</param>
        public static void PrintGroupLens(string[] tableHeaders, double[] productIdList)
        {
            for (int a = 0; a < 2; a++)
            {
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            int[] userIDs = new List<int>(FileReader.DictionaryData.Keys).ToArray();
            Array.Sort(userIDs);
            foreach (var userID in userIDs)
            {
                Console.Write(userID + "\t" + "\t");
                Console.Write(Math.Round(AverageRating.AverageRatingDictionary[userID], 2) + "\t");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print voor basicData, de user, average rating, product ratings
        /// </summary>
        /// <param name="tableHeaders">Headers</param>
        /// <param name="productIdList">Lijst van ProductId</param>
        public static void Print2DArrayMatrix(string[] tableHeaders, double[] productIdList)
        {
            for (int a = 0; a <= tableHeaders.Length - 1; a++)
            {
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            int[] userIDs = new List<int>(FileReader.DictionaryData.Keys).ToArray();
            Array.Sort(userIDs);
            foreach (var userID in userIDs)
            {
                Console.Write(userID + "\t" + "\t");
                Console.Write(Math.Round(AverageRating.AverageRatingDictionary[userID],2) + "\t");
                foreach (var productID in productIdList)
                {
                    try
                    {
                        var userProduct= FileReader.DictionaryData[userID].Find(x => x.Item1 == productID);
                        Console.Write("\t" + userProduct.Item2);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.Write("\t" + 0);
                    }
                    
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print voor basicData, de user, average rating, product ratings
        /// </summary>
        /// <param name="tableHeaders">Headers</param>
        /// <param name="productIdList">Lijst van ProductId</param>
        public static void Print2DArrayMatrixPreOneSlope(string[] tableHeaders, double[] productIdList)
        {
            for (int a = 0; a <= tableHeaders.Length - 1; a++)
            {
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            int[] userIDs = new List<int>(FileReader.DictionaryData.Keys).ToArray();
            Array.Sort(userIDs);
            foreach (var userID in userIDs)
            {
                Console.Write(userID + "\t");
                foreach (var productID in productIdList)
                {
                    try
                    {
                        var userProduct = FileReader.DictionaryData[userID].Find(x => x.Item1 == productID);
                        Console.Write("\t" + userProduct.Item2);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.Write("\t" + 0);
                    }

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Het instellen van headers
        /// </summary>
        /// <param name="names">Naam van extra kolom</param>
        /// <param name="itemlist">Lijst van productId</param>
        /// <returns>Headers</returns>
        public static string[] SetTableHeaderMatrix(string[] names, double[] itemlist)
        {
            int headerArrayLength = names.Length + itemlist.Length;
            string[] headers = new string[headerArrayLength];
            for (int i = 0; i < names.Length; i++)
            {
                headers[i] = names[i];
            }
            for (int j = 0; j < itemlist.Length; j++)
            {
                headers[j + names.Length] = itemlist[j].ToString();
            }
            return headers;
        }
    }
}
