using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ItemItem
{
    class FileReader
    {
        // item.key = userId, List<productId, rating>
        // key = 1, List<(104,3.0),(106,5.0)> etc etc
        public static Dictionary<int, List<Tuple<int, double>>> DictionaryData = new Dictionary<int, List<Tuple<int, double>>>();

        public static double[,] dataItem = new[,]
        {
            {1, 103, 4.0 },
            {1, 106, 3.0 },
            {1, 109, 4.0 },

            {2, 103, 5.0 },
            {2, 106, 2.0 },

            {3, 106, 3.5 },
            {3, 109, 4.0 },

            {4, 103, 5.0 },
            {4, 109, 3.0 }
        };
        public static double[,] dataACS = new[,]
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
        //data nodig voor opdracht
        public static double[,] dataUserItem = new[,]
        {
            { 1,101,2.5 },
            { 1,102,3.5},
            { 1,105,2.5},
            { 3,104,3.5},
            { 2,102,3.5},
            { 1,106,3.0},
            { 2,101,3.0},
            { 5,103,2.0},
            { 2,103,1.5},
            { 2,104,5.0 },
            { 6,105,3.5 },
            { 2,106,3.0 },
            { 3,101,2.5 },
            { 6,104,5.0},
            { 3,102,3.0},
            { 5,106,3.0},
            { 3,106,4.0},
            { 4,102,3.5},
            { 6,106,3.0},
            { 4,104,4.0},
            { 7,104,4.0},
            { 4,105,2.5},
            { 1,104,3.5},
            { 1,103,3.0},
            { 2,105,3.5},
            { 4,106,4.5},
            { 5,101,3.0},
            { 5,102,4.0},
            { 6,102,4.0},
            { 5,104,3.0},
            { 4,103,3.0},
            { 5,105,2.0},
            { 6,101,3.0},
            { 7,102,4.5},
            { 7,105,1.0},

            };
        public static double[,] dataOneSlope = new[,]
        {
            {1, 101, 5.0 },
            {1, 102, 3.0 },
            {1, 103, 2.5 },

            {2, 101, 2.0},
            {2, 102, 2.5 },
            {2, 103, 5.0 },
            {2, 104, 2.0 },

            {3, 101, 2.5},
            {3, 104, 4.0 },
            {3, 105, 4.5 },
            {3, 107, 5.0 },

            {4, 101, 5.0},
            {4, 103, 3.0 },
            {4, 104, 4.5 },
            {4, 106, 4.0 },

            {5, 101, 4.0 },
            {5, 102, 3.0 },
            {5, 103, 2.0 },
            {5, 104, 4.0 },
            {5, 105, 3.5 },
            {5, 106, 4.0 }
            };

        public static void GetData(int dataChoice)
        {

            if (dataChoice == 2)
            {
                SetupBasicData();
            }
            else
            {
                SetupAdvancedData();
            }

        }

        public static void SetupBasicData()
        {
            var data = dataUserItem;
            if (UserChoice.choiceAlgorithm == 1)
            {
                //ACS
                data = dataACS;
            }
            int rowLength = data.GetLength(0);
            int colLenght = data.GetLength(1);

            for (int i = 0; i <= rowLength - 1; i++)
            {
                double userId = data[i, 0];
                double userProduct = data[i, 1];
                double userProductRating = data[i, 2];
                if (DictionaryData.ContainsKey((int)userId))
                {
                    DictionaryData[(int)userId].Add(new Tuple<int, double>((int)userProduct, userProductRating));
                }
                else
                {
                    DictionaryData.Add((int)userId, new List<Tuple<int, double>> { new Tuple<int, double>((int)userProduct, userProductRating) });
                }
            }

        }
        public static void SetupAdvancedData()
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("../../../../ItemItem/Files/u.data"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (var item in list)
            {
                string[] userItem = item.Split("\t");
                int userId = int.Parse(userItem[0]);
                int userProduct = int.Parse(userItem[1]);
                double userProductRating = double.Parse(userItem[2]);

                if (DictionaryData.ContainsKey(userId))
                {
                    DictionaryData[userId].Add(new Tuple<int, double>(userProduct, userProductRating));
                }
                else
                {
                    DictionaryData.Add(userId, new List<Tuple<int, double>> { new Tuple<int, double>(userProduct, userProductRating) });
                }
            }
        }

        /// <summary>
        /// Pakt alle producten en zet het in een array
        /// </summary>
        /// <returns>een array van productId in double</returns>
        public static double[] GetItemList()
        {
            HashSet<double> hsetProducts = new HashSet<double>();
            foreach (var user in DictionaryData)
            {
                var userProducts = user.Value.Select(x => (double)x.Item1).ToList<double>();
                hsetProducts = hsetProducts.Concat((List<double>)userProducts).ToHashSet();
            }
            double[] array = new double[hsetProducts.Count()];
            hsetProducts.CopyTo(array);
            Array.Sort(array);
            return array;
        }
    }
}
