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
            UserChoice.choiceUserId = int.Parse(Console.ReadLine());
            Console.WriteLine("Pick a desired Product");
            UserChoice.choiceProductId = int.Parse(Console.ReadLine());
            if (UserChoice.choiceData == 1)
            {
                Console.WriteLine("Calculating prediction...");
            }
            OneSlope.PredictRating(UserChoice.choiceUserId, UserChoice.choiceProductId);
            Console.ReadLine();
        }
        public static void PickedACS()
        {
            Console.WriteLine("Pick product and an user for new predicted rating");
            Console.WriteLine("Pick the userID");
            UserChoice.choiceUserId = int.Parse(Console.ReadLine());


            Console.WriteLine("\n");
            Console.WriteLine("Pick the itemId ");
            UserChoice.choiceProductId = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            if (UserChoice.choiceData == 1)
            {
                Console.WriteLine("Calculating prediction...");
            }
            Cosinus.ACS(UserChoice.choiceProductId);
            Normalization.Normalize(UserChoice.choiceUserId);

            Console.WriteLine("Predicted result: " + Prediction.CalculatePrediction(UserChoice.choiceUserId, UserChoice.choiceProductId));
            Console.ReadLine();

        }
        public static void PickDesiredItem()
        {
            Dictionary<int, List<Tuple<int, double>>> dictionaryBasic = new Dictionary<int, List<Tuple<int, double>>>();
            Dictionary<int, double[,]> dictionaryAdvanced = new Dictionary<int, double[,]>();
            var itemList = new double[0];
            var matrix = new double[0, 0];
            string[] headers = new string[] { "userid/itemid", "avg rating" };
            string[] headersOneSlope = new string[] { "userid/itemid" };
            Console.WriteLine("Pick a dataset, 1 for Group Lens dataset and 2 for Basic dataset");
            UserChoice.choiceData = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            Console.WriteLine("Pick a desired Algorithm: 1 for ACS and 2 for Oneslope");
            UserChoice.choiceAlgorithm = int.Parse(Console.ReadLine());
            switch (UserChoice.choiceData)
            {
                case 1:
                    FileReader.GetData(1);
                    if (UserChoice.choiceAlgorithm == 1)
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
                    itemList = FileReader.GetItemList();
                 
                    if (UserChoice.choiceAlgorithm == 1)
                    {
                        AverageRating.CalculateAverageRating();
                        PrintMethods.Print2DArrayMatrix(PrintMethods.SetTableHeaderMatrix(headers, itemList), itemList);
                        PickedACS();
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        PrintMethods.Print2DArrayMatrixPreOneSlope(PrintMethods.SetTableHeaderMatrix(headersOneSlope, itemList), itemList);
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