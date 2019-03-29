using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem
{
    class FileReader
    {
        public FileReader()
        {

        }
        public static Dictionary<int, double[,]> GetData(double[,] data)
        {
            var dictionary = new Dictionary<int, double[,]>();
            int rowLength = data.GetLength(0);
            int colLenght = data.GetLength(1);
            bool checkeNewRow = false;

            for (int i = 0; i < rowLength; i++)
            {
                int key = -1;
                double productvalue = -1;
                double userRating = -1;
                checkeNewRow = false;

                for (int j = 0; j < colLenght; j++)
                {
                    //Console.WriteLine(data[i, j]);
                    if (!checkeNewRow)
                    {
                        key = int.Parse(data[i, j] + "");
                        checkeNewRow = true;

                    }

                    else if (j == 1)
                    {
                        productvalue = data[i, j];
                    }
                    else if (j == 2)
                    {
                        userRating = data[i, j];
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
        public static double[] GetItemList(double[,] data)
        {
            var item = new List<double>();
            for (int i = 0; i <= data.GetLength(0) - 1 ; i++)
            {
                for (int j = 0; j <= data.GetLength(1) -1; j++)
                {
                    if(!item.Contains(data[i,1]))
                    {
                        item.Add(data[i, 1]);
                    }
                    
                }
            }
            double[] array = item.ToArray();
            Array.Sort(array);
            return array;
        }
    }
}
