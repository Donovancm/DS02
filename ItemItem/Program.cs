using ItemItem.Interfaces;
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
            PickDesiredItem();
        }
        public static void PickDesiredItem()
        {
            IReader iReader = null;
            Dictionary<int, double[,]> dictionaryBasic = new Dictionary<int, double[,]>();
            Dictionary<int, double[,]> dictionaryAdvanced = new Dictionary<int, double[,]>();
            var itemList = new double[0];
            var matrix = new double[0, 0];
            string[] headers = new string[] { "userid/itemid", "avg rating" };
            Console.WriteLine("Pick Dataset");
            int choiceData = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choiceData)
            {
                case 1:
                    iReader = new Datareader();
                    dictionaryAdvanced = iReader.GetData();
                    itemList = FileReader.GetItemListLens(dictionaryAdvanced);
                    matrix = Matrices.CreateMatrix.Create(itemList, dictionaryAdvanced);
                    Console.WriteLine("Average waiting time to load is 16 minutes");
                    break;
                case 2:
                    iReader = new FileReader();
                    dictionaryBasic = iReader.GetData();
                    itemList = FileReader.GetItemList();
                    matrix = Matrices.CreateMatrix.Create(itemList, dictionaryBasic);
                    PrintMethods.Print2DArrayMatrix(matrix, PrintMethods.SetTableHeaderMatrix(headers, itemList));
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Closed");
                    Console.ReadLine();
                    break;
            }
            Console.WriteLine("Pick the first item id ");
            int choice1 = int.Parse(Console.ReadLine());

            Console.WriteLine("\n");

            Console.WriteLine("Pick the second item id ");
            int choice2 = int.Parse(Console.ReadLine());

            Console.WriteLine("\n");

            Formulas.Cosinus.ACS(matrix, choice1, choice2, itemList);
            Formulas.Deviations.CreateDeviationMatrix(itemList, matrix);
            var devMatrixS = Formulas.Deviations.devMatrixSimilarity;
            var devMatrixU = Formulas.Deviations.devMatrixUserAmount;
            var normalizeMatrixRating = Formulas.Normalization.NormalizedMatrix(matrix);

            if (choiceData == 2)
            {
                PrintMethods.Print2DArrayDevMatrix(devMatrixS, PrintMethods.SetTableHeaderDevMatrix(null, itemList));
                PrintMethods.Print2DArrayDevMatrixUsers(devMatrixU, PrintMethods.SetTableHeaderDevMatrix(null, itemList));
            }
            Console.WriteLine("Pick product for new predicted rating");
            //read
            int choice3 = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            Console.WriteLine("Pick the desired User");
            int choice4 = int.Parse(Console.ReadLine());
            Console.WriteLine("Predicted result: " + Formulas.Prediction.CalculatePrediction(devMatrixS, normalizeMatrixRating, choice3, itemList, choice4));
            Console.ReadLine();


        }
    }
}