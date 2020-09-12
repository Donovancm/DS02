using ItemItem.Formulas;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ItemItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ItemItem Algorithm");
            PickDesiredItem();
            //PickDesiredAlgorithm();
        }

        public static void PickedOneSlope()
        {
            Console.WriteLine("Pick a desired User");
            int choiceUserId = int.Parse(Console.ReadLine());
            Console.WriteLine("Pick a desired Product");
            int choiceProductId = int.Parse(Console.ReadLine());
            OneSlope.PredictRating(choiceUserId, choiceProductId);
            Console.ReadLine();
        }
        public static void PickedACS()
        {
            Console.WriteLine("Pick product and an user for new predicted rating");
            Console.WriteLine("Pick the userID");
            int userID = int.Parse(Console.ReadLine());


            Console.WriteLine("\n");
            Console.WriteLine("Pick the itemId ");
            int productID = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            //if (FileReader.GetData(1))
            //{
            //    Console.WriteLine("Calculating prediction...");
            //}
            Cosinus.ACS(productID);
            Normalization.Normalize(userID);

            Console.WriteLine("Predicted result: " + Prediction.CalculatePrediction(userID, productID));
            Console.ReadLine();

        }
        public static void PickDesiredItem()
        {
            Dictionary<int, List<Tuple<int, double>>> dictionaryBasic = new Dictionary<int, List<Tuple<int, double>>>();
            Dictionary<int, double[,]> dictionaryAdvanced = new Dictionary<int, double[,]>();
            var itemList = new double[0];
            var matrix = new double[0, 0];
            string[] headers = new string[] { "userid/itemid", "avg rating" };
            Console.WriteLine("Pick a dataset, 1 for Group Lens dataset and 2 for Basic dataset");
            int choiceData = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            Console.WriteLine("Pick a desired Algorithm: 1 for ACS and 2 for Oneslope");
            int choiceAlgorithm = int.Parse(Console.ReadLine());
            switch (choiceData)
            {
                case 1:
                    FileReader.GetData(1);
                    if (choiceAlgorithm == 1)
                    {
                        itemList = FileReader.GetItemList();
                        AverageRating.CalculateAverageRating();
                        PrintMethods.PrintGroupLens(PrintMethods.SetTableHeaderMatrix(headers, itemList), itemList);
                        PickedACS();
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        PickedOneSlope();
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("\n");
                    break;
                case 2:
                    FileReader.GetData(2);
                    if (choiceAlgorithm == 1)
                    {
                        itemList = FileReader.GetItemList();
                        AverageRating.CalculateAverageRating();
                        PrintMethods.PrintGroupLens(PrintMethods.SetTableHeaderMatrix(headers, itemList), itemList);
                        PickedACS();
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        PickedOneSlope();
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Closed");
                    Console.ReadLine();
                    break;
            }
            //Console.WriteLine("Pick product and an user for new predicted rating");
            //Console.WriteLine("Pick the userID");
            //int userID = int.Parse(Console.ReadLine());


            //Console.WriteLine("\n");
            //Console.WriteLine("Pick the itemId ");
            //int productID = int.Parse(Console.ReadLine());
            //Console.WriteLine("\n");
            //if (choiceData == 1)
            //{
            //    Console.WriteLine("Calculating prediction...");
            //}
            //Cosinus.ACS(productID);
            //Normalization.Normalize(userID);

            //Console.WriteLine("Predicted result: " + Prediction.CalculatePrediction(userID,productID));
            //Console.ReadLine();
        }
    }
}