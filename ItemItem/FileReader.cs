using ItemItem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ItemItem
{
    class FileReader
    {
        public static Dictionary<int, List<Tuple<int,double>>> DictionaryData = new Dictionary<int, List<Tuple<int, double>>>();
        public static double[,] data = new[,]
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
        public static double[,] dataToor = new[,]
            {
            {1, 103, 4.0 },
            {1, 106, 3.0},
            {1, 109, 4.0 },

            {2, 103, 5.0},
            {2, 106, 2.0 },

            {3, 106, 3.5},
            {3, 109, 4.0 },

            {4, 103, 5.0},
            {4, 109, 3.0 },


            };
        public static void GetData(int dataChoice)
        {

            if (dataChoice == 2)
            {
                SetupBasicData();
            }else
            {
                SetupAdvancedData();
            }
            
        }

        public static void SetupBasicData()
        {
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
                    list.Add(line); // Add to list.
                    //Console.WriteLine(line); // Write to console.
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

            public static double[] GetItemList_New()
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

        public static double[] GetItemList()
        {
            var data = new[,]
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
            var dataToor = new[,]
            {
            {1, 103, 4.0 },
            {1, 106, 3.0},
            {1, 109, 4.0 },

            {2, 103, 5.0},
            {2, 106, 2.0 },

            {3, 106, 3.5},
            {3, 109, 4.0 },

            {4, 103, 5.0},
            {4, 109, 3.0 },


            };
            var item = new List<double>();
            for (int i = 0; i <= data.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= data.GetLength(1) - 1; j++)
                {
                    if (!item.Contains(data[i, 1]))
                    {
                        item.Add(data[i, 1]);
                    }

                }
            }
            double[] array = item.ToArray();
            Array.Sort(array);
            return array;
        }
        public static double[] GetItemListLens(Dictionary<int, double[,]> data)
        {
            List<double> itemlist = new List<double>();
            foreach (var item in data)
            {
                var arrayRatingMatrix = item.Value;
                for (int i = 0; i <= arrayRatingMatrix.GetLength(1)-1; i++)
                {
                    if (!itemlist.Contains(arrayRatingMatrix[i,0]))
                    {
                        itemlist.Add(arrayRatingMatrix[i, 0]);
                    }
                }
            }
            double[] array = itemlist.ToArray();
            Array.Sort(array);
            return array;
        }

    }
}
