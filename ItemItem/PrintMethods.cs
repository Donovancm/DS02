using ItemItem.Matrices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem
{
    class PrintMethods
    {
        public static void Print2DArrayMatrix_New_BigData(string[] tableHeaders, double[] productIdList)
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
                Console.Write(Math.Round(CreateMatrix.AverageRatingDictionary[userID], 2) + "\t");
                Console.WriteLine();
            }
        }

        public static void Print2DArrayMatrix_New(string[] tableHeaders, double[] productIdList)
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
                Console.Write(Math.Round(CreateMatrix.AverageRatingDictionary[userID],2) + "\t");
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

        public static void Print2DArrayMatrix(double[,] matrix, string[] tableHeaders)
        {
            for (int a = 0; a <= tableHeaders.Length - 1; a++)
            {
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= matrix.GetLength(1) - 1; j++)
                {
                    if (j == 0 || j == 1)
                    {
                        Console.Write(Math.Round(matrix[i, j],2) + "\t" + "\t");
                    }
                    else
                    {
                        Console.Write(Math.Round(matrix[i, j], 2) + "\t");
                    }

                }
                Console.WriteLine();
            }
        }
        public static void Print2DArrayDevMatrix(double[,] matrix, string[] tableHeaders)
        {
            Console.WriteLine("Deviation Matrix Similarity Values");
            for (int a = 0; a <= tableHeaders.Length - 1; a++)
            {
                Console.Write("\t");
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= matrix.GetLength(1) - 1; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(tableHeaders[i] + "\t");
                        Console.Write(Math.Round(matrix[i, j], 4) + "\t" + "\t");
                    }
                    else
                    {
                        Console.Write(Math.Round(matrix[i, j], 4) + "\t" + "\t");
                    }

                }
                Console.WriteLine();
                //Console.Write("\n");
            }
        }
        public static void Print2DArrayDevMatrixUsers(double[,] matrix, string[] tableHeaders)
        {
            Console.Write("\n");
            Console.WriteLine("Deviation Matrix Amount of Users");
            for (int a = 0; a <= tableHeaders.Length - 1; a++)
            {
                Console.Write("\t");
                Console.Write(tableHeaders[a] + "\t");

            }
            Console.Write("\n");
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= matrix.GetLength(1) - 1; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(tableHeaders[i] + "\t");
                        Console.Write(Math.Round(matrix[i, j], 4) + "\t" + "\t");
                    }
                    else
                    {
                        Console.Write(Math.Round(matrix[i, j], 4) + "\t" + "\t");
                    }

                }
                Console.WriteLine();

            }
        }
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
        public static string[] SetTableHeaderDevMatrix(string[] names, double[] itemlist)
        {
            string[] headers = new string[itemlist.Length];
            for (int i = 0; i < itemlist.Length; i++)
            {
                headers[i] = itemlist[i].ToString();
            }
            return headers;
        }
    }
}
