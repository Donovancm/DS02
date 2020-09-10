using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    public class OneSlope
    {
        public static List<Tuple<double, int>> Deviations;

        public static Tuple<double, int> CalculateDeviations(Dictionary<int, List<Tuple<int, double>>> userData, int itemA, int itemB)
        {
            var data = userData.Values;
            int count = 0;
            double sum = 0.0;
            double deviation = 0.0;

            foreach (var products in data)
            {
                foreach (Tuple<int, double> productA in products.Where(x => x.Item1 == itemA))
                {
                    foreach (Tuple<int, double> productB in products.Where(x => x.Item1 == itemB))
                    {
                        if (productA.Item2 != 0 && productB.Item2 != 0)
                        {
                            sum += productA.Item2 - productB.Item2;
                            count++;
                        }
                        //sum += productA.Item2 - productB.Item2;
                        //count++;
                    }
                }
                deviation = sum / count;
            }

            deviation = sum / count;
            Console.WriteLine("deviation: " + deviation + " NumberofUsers: " + count);
            return new Tuple<double, int>(deviation, count);
        }
        public static double PredictRating(int userId, int product)
        {
            double upper = 0.0;
            userId = 3;
            product = 106;
            double lower = 0.0;
            var userData = FileReader.DictionaryData;
            var data = FileReader.DictionaryData.Values;

            //List<Tuple<int,double>> productList  //data.Where((v, i) => i != product);
            //List<Tuple<int, double>> productlistUser = userData;
            foreach (var products in data)
            {
                List<Tuple<int, double>> productlist = products.Where(x => x.Item1 != product).ToList();
                List<Tuple<int, double>> productlistUser = userData[userId].Where(x => x.Item2 != 0 && x.Item1 != 0).ToList();
                foreach (var item in productlistUser.Where(x => x.Item2 != 0))
                {
                    //List<Tuple<int, double>> productlistUser = userData[userId].Where(x => x.Item2 != 0).ToList();
                    if (product != item.Item1 && item.Item2 != 0)
                    {
                        Tuple<double, int> deviationsPair = CalculateDeviations(userData, product, item.Item1);
                        double deviation = deviationsPair.Item1;
                        int countUsers = deviationsPair.Item2;
                        if (deviation != 0 && countUsers > 0)
                        {
                            upper += (item.Item2 + deviation) * countUsers;
                            lower += countUsers;
                        }
                    }
                }
            }
            double result = upper / lower;
            Console.WriteLine("Predicted Rating for User " + userId + " for the product " + product + " is: " + result);
            return result;
        }
    }
}
