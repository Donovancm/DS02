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
            var data = new[,] {
                {1,101,2.5},
                { 1,102,3.5},
                { 1,103,3.0},
                { 1,104,3.5},
                { 1,105,2.5},
                { 1,106,3.0},
                { 2,101,3.0},
                { 2,102,3.5},
                { 2,103,1.5},
                { 2,104,5.0},
                { 2,105,3.5},
                { 2,106,3.0},
                { 3,101,2.5},
                { 3,102,3.0},
                { 3,104,3.5},
                { 3,106,4.0},
                { 4,102,3.5},
                { 4,103,3.0},
                { 4,104,4.0},
                { 4,105,2.5},
                { 4,106,4.5},
                { 5,101,3.0},
                { 5,102,4.0},
                { 5,103,2.0},
                { 5,104,3.0},
                { 5,105,2.0},
                { 5,106,3.0},
                { 6,101,3.0},
                { 6,102,4.0},
                { 6,104,5.0},
                { 6,105,3.5},
                { 6,106,3.0},
                { 7,102,4.5},
                { 7,104,4.0},
                { 7,105,1.0}
        };
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
            Formulas.Cosinus.ACS(matrix, 103, 104, itemList);
            Formulas.Deviations.CreateDeviationMatrix(itemList, matrix);
            var devMatrixS = Formulas.Deviations.devMatrixSimilarity;
            var devMatrixU = Formulas.Deviations.devMatrixUserAmount;
            var normalizeMatrixRating = Formulas.Normalization.NormalizedMatrix(matrix);
            Formulas.Prediction.CalculatePrediction(devMatrixS, normalizeMatrixRating, 103, itemList, 1);
        }
    }
}