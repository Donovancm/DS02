using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ItemItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data2 = new[,]
            {
            {1, 104, 3.0},
            {1, 106, 5.0 },
            {1, 107, 4.0 },
            {1,109,1.0 },

            {2, 104, 3.0},
            {2, 106, 4.0 },
            {2, 107, 4.0 },
            {2,109,1.0 },

            {3, 103, 4.0},
            {3, 104, 3.0 },
            {3, 107, 3.0 },
            {3, 109, 1.0 },

            {4, 103, 4.0},
            {4, 104, 4.0 },
            {4, 106, 4.0 },
            {4, 107, 3.0 },
            {4, 109, 1.0 },

            {5, 103, 5.0 },
            {5, 104, 4.0 },
            {5, 106, 5.0 },
            {5, 109, 3.0 }
            };
            Datareader.GetData();

            Dictionary<int, double[,]> dictionaryBasic = FileReader.GetData(data2);
            var itemList = FileReader.GetItemList(data2);

            //Dictionary<int, double[,]> dictionaryBasic = Datareader.GetData();
            //var itemList = FileReader.GetItemListLens(Datareader.GetData());

            //Dictionary<int, double[,]> dictionaryBasic = FileReader.GetData(data2);
            //var itemList = FileReader.GetItemList(data2);

            var matrix = Matrices.CreateMatrix.Create(itemList, dictionaryBasic);
            string[] headers = new string[] { "userid/itemid", "avg rating"  };
           // Print2DArrayMatrix(matrix, SetTableHeaderMatrix(headers,itemList));

            Formulas.Cosinus.ACS(matrix, 103, 104, itemList);
            Formulas.Deviations.CreateDeviationMatrix(itemList, matrix);
            var devMatrixS = Formulas.Deviations.devMatrixSimilarity;
            var devMatrixU = Formulas.Deviations.devMatrixUserAmount;
            Print2DArrayDevMatrix(devMatrixS, SetTableHeaderDevMatrix(null, itemList));
            Print2DArrayDevMatrixUsers(devMatrixU, SetTableHeaderDevMatrix(null, itemList));
            var normalizeMatrixRating = Formulas.Normalization.NormalizedMatrix(matrix);
            Formulas.Prediction.CalculatePrediction(devMatrixS, normalizeMatrixRating, 103, itemList, 1);
        }
        public static void PickData()
        {

        }
        public static void Print2DArrayMatrix(double[,] matrix, string[] tableHeaders)
        {
            for (int a = 0; a <= tableHeaders.Length-1; a++)
            {
                Console.Write(tableHeaders[a] + "\t");
              
            }
            Console.Write("\n");
            for (int i = 0; i <= matrix.GetLength(0) -1; i++)
            {
                for (int j = 0; j <= matrix.GetLength(1) -1; j++)
                {
                    if (j == 0 || j == 1)
                    {
                        Console.Write(matrix[i, j] + "\t" + "\t");
                    }
                    else
                    {
                        Console.Write(matrix[i, j] + "\t");
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
                        Console.Write(tableHeaders[i] + "\t" );
                        Console.Write(Math.Round(matrix[i, j],4) + "\t" + "\t");
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