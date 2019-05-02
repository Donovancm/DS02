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
            //var itemList = FileReader.GetItemListLens(dictionaryBasic);

            //Dictionary<int, double[,]> dictionaryBasic = FileReader.GetData(data2);
            //var itemList = FileReader.GetItemList(data2);
            var matrix = Matrices.CreateMatrix.Create(itemList, dictionaryBasic);
            string[] headers = new string[] { "userid/itemid", "avg rating"  };
           // PrintMethods.Print2DArrayMatrix(matrix, PrintMethods.SetTableHeaderMatrix(headers,itemList));

            Formulas.Cosinus.ACS(matrix, 103, 104, itemList);
            Formulas.Deviations.CreateDeviationMatrix(itemList, matrix);
            var devMatrixS = Formulas.Deviations.devMatrixSimilarity;
            var devMatrixU = Formulas.Deviations.devMatrixUserAmount;
           // PrintMethods.Print2DArrayDevMatrix(devMatrixS, PrintMethods.SetTableHeaderDevMatrix(null, itemList));



           // PrintMethods.Print2DArrayDevMatrixUsers(devMatrixU, PrintMethods.SetTableHeaderDevMatrix(null, itemList));
            var normalizeMatrixRating = Formulas.Normalization.NormalizedMatrix(matrix);

            Console.WriteLine("Predicted result: "+ Formulas.Prediction.CalculatePrediction(devMatrixS, normalizeMatrixRating, 103, itemList, 1));
            Console.ReadLine();
        }
        public static void PickData()
        {

        }
    }
}