using ItemItem.Formulas;
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
            Dictionary<int, List<Tuple<int, double>>> dictionaryBasic = new Dictionary<int, List<Tuple<int, double>>>();
            Dictionary<int, double[,]> dictionaryAdvanced = new Dictionary<int, double[,]>();
            var itemList = new double[0];
            var matrix = new double[0, 0];
            string[] headers = new string[] { "userid/itemid", "avg rating" };
            Console.WriteLine("Pick a dataset, 1 for Advanced dataset and 2 for Basic dataset");
            int choiceData = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (choiceData)
            {
                case 1:
                    FileReader.GetData(1);
                    itemList = FileReader.GetItemList_New();
                    Matrices.CreateMatrix.CalculateAverageRating_New();
                    PrintMethods.Print2DArrayMatrix_New_BigData(PrintMethods.SetTableHeaderMatrix(headers, itemList), itemList);
                    Console.WriteLine("\n");
                    break;
                case 2:
                    FileReader.GetData(2);
                    itemList = FileReader.GetItemList_New();
                    Matrices.CreateMatrix.CalculateAverageRating_New();
                    PrintMethods.Print2DArrayMatrix_New(PrintMethods.SetTableHeaderMatrix(headers, itemList), itemList);
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Closed");
                    Console.ReadLine();
                    break;
            }

            Console.WriteLine("Pick product and an user for new predicted rating");
            Console.WriteLine("Pick the userID");
            int userID = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            Console.WriteLine("Pick the itemId ");
            int productID = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            Cosinus.ACS(productID);
            Normalization.Normalize(userID);

            //TODO print basic data info
            if (choiceData == 2)
            {
                //PrintMethods.Print2DArrayDevMatrix(devMatrixS, PrintMethods.SetTableHeaderDevMatrix(null, itemList));
                //PrintMethods.Print2DArrayDevMatrixUsers(devMatrixU, PrintMethods.SetTableHeaderDevMatrix(null, itemList));
            }
     
            Console.WriteLine("Predicted result: " + Prediction.CalculatePrediction_New(userID,productID));
            Console.ReadLine();


        }
    }
}