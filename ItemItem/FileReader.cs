using ItemItem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem
{
    class FileReader : IReader
    {
        public Dictionary<int, double[,]> GetData()
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
            var dictionary = new Dictionary<int, double[,]>();
            int rowLength = dataToor.GetLength(0);
            int colLenght = dataToor.GetLength(1);
            bool checkeNewRow = false;

            for (int i = 0; i < rowLength; i++)
            {
                int key = -1;
                double productvalue = -1;
                double userRating = -1;
                checkeNewRow = false;

                for (int j = 0; j < colLenght; j++)
                {
                    //Console.WriteLine(dataToor[i, j]);
                    if (!checkeNewRow)
                    {
                        key = int.Parse(dataToor[i, j] + "");
                        checkeNewRow = true;

                    }

                    else if (j == 1)
                    {
                        productvalue = dataToor[i, j];
                    }
                    else if (j == 2)
                    {
                        userRating = dataToor[i, j];
                        var userdata = new double[,]
                        {
                            {productvalue,userRating}
                        };
                        if (dictionary.ContainsKey(key))
                        {
                            var data2 = new double[,] { };
                            data2 = dictionary[key];
                            int rowLengthdata2 = data2.GetLength(0) - 1;
                            int colLenghtdata2 = 2;
                            int rowLengthdata3 = rowLengthdata2 + 1;
                            int newRow2Ddata3 = data2.GetLength(0) + 1;
                            var data3 = new double[newRow2Ddata3, colLenghtdata2];

                            for (int r = 0; r <= rowLengthdata2; r++)
                            {
                                for (int c = 0; c < colLenghtdata2; c++)
                                {
                                    data3[r, c] = data2[r, c];
                                }

                            }
                            data3[rowLengthdata3, 0] = productvalue;
                            data3[rowLengthdata3, 1] = userRating;
                            dictionary[key] = data3;

                        }
                        else { dictionary.Add(key, userdata); }

                    }

                }
            }
            return dictionary;
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
            for (int i = 0; i <= dataToor.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= dataToor.GetLength(1) - 1; j++)
                {
                    if (!item.Contains(dataToor[i, 1]))
                    {
                        item.Add(dataToor[i, 1]);
                    }

                }
            }
            double[] array = item.ToArray();
            Array.Sort(array);
            return array;
        }
        public static double[] GetItemListLens(Dictionary<int, double[,]> dataToor)
        {
            List<double> itemlist = new List<double>();
            foreach (var item in dataToor)
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
