using ItemItem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ItemItem
{
    class Datareader : IReader
    {
        public Dictionary<int, double[,]> GetData()
        {
            var dictionary = new Dictionary<int, double[,]>();

            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("C:/Users/Donovan/source/repos/ItemItem/ItemItem/Files/u.data"))
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
                int user_id = int.Parse(userItem[0]);
                double product_id = double.Parse(userItem[1]);
                double rating = double.Parse(userItem[2]);
                var userdata = new double[,]
                {
                            {product_id,rating}
                };
                if (dictionary.ContainsKey(user_id))
                {
                    // great new 2d array with 1 extra row en readd old 2d array data to new array data and add new useritem data 

                    var data2 = new double[,] { };
                    data2 = dictionary[user_id];
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
                    data3[rowLengthdata3, 0] = product_id;
                    data3[rowLengthdata3, 1] = rating;
                    dictionary[user_id] = data3;
                }
                else
                {
                    dictionary.Add(user_id, userdata);
                }
            }
            return dictionary;
        }
    }
}
