using ItemItem.Formulas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ItemItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ItemItem Algorithm");
            PickDesiredItem();
        }

        public static void PickedOneSlope()
        {
            Console.WriteLine("Pick a desired User");
            UserChoice.choiceUserId = int.Parse(Console.ReadLine());

            if (UserChoice.choiceData == 1)
            {
                Console.WriteLine("Calculating prediction...");
            }
            List<int> ratedProduct = FileReader.DictionaryData[UserChoice.choiceUserId].Select(x => x.Item1).ToList();
            ratedProduct.Sort();
            foreach (var productId in FileReader.GetItemList())
            {
                if (!ratedProduct.Contains((int)productId))
                {
                    OneSlope.PredictRating(UserChoice.choiceUserId, (int)productId);
                }
            }
            Console.ReadLine();
        }
        public static void PickedACS()
        {
            Console.WriteLine("Pick product and an user for new predicted rating");
            Console.WriteLine("Pick the userID");
            UserChoice.choiceUserId = int.Parse(Console.ReadLine());

            Console.WriteLine("1: for specific product or 2: show all predictions");
            int choiceOption = int.Parse(Console.ReadLine());

            List<int> ratedProduct = FileReader.DictionaryData[UserChoice.choiceUserId].Select(x => x.Item1).ToList();
            ratedProduct.Sort();
            foreach (var productId in FileReader.GetItemList())
            {
                if (!ratedProduct.Contains((int)productId))
                {
                    Cosinus.ACS((int)productId);
                    if (Normalization.NormalizedDictionary.Count() == 0)
                    {
                        Normalization.Normalize(UserChoice.choiceUserId);
                    }
                    Console.WriteLine("Predicted result for productId: " + productId + " predicted rating is " + Prediction.CalculatePrediction(UserChoice.choiceUserId, (int)productId));
                }
            }
            Console.ReadLine();

        }
        public static void PickDesiredItem()
        {
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
        }
    }
}