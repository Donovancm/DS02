using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Matrices
{
    class CreateMatrix
    {
        public static void CalculateAverageRating(double[,] matrix)
        {
            
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                double sumrating = 0;
                double totalItems = 0;
                for (int j = 2; j <= matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i,j] > 0)
                    {
                        totalItems++;
                        sumrating += matrix[i, j];
                    }
                }
                matrix[i, 1] = sumrating / totalItems;
            }
        }
        public static double[,] Create(double[] itemList, Dictionary<int, double[,]> item)
        {
            var matrix = new double[item.Count, itemList.Length +2];
            var index = -1;
            foreach (var Useritem in item)
            {
                index++;
                matrix[index, 0] = Useritem.Key;
                var ratings = item[Useritem.Key];
                for (int i = 0; i <= ratings.GetLength(0)-1; i++)
                {
                    for (int a = 0; a <= itemList.Length - 1; a++)
                    {
                       
                            Console.Write(ratings.GetLength(1) - 1);
                        if (ratings[i, 0] != itemList[a] && a == itemList.Length - 1)
                        {
                            matrix[index, a + 2] = 9999;
                            // set null 
                            break;
                        }
                        else if (ratings[i, 0] == itemList[a])
                        {
                            //set rating and calculate avg rating
                            matrix[index, a + 2] = ratings[i, 1];
                            break;
                        }

                    }
                    
                }

            }
            CalculateAverageRating(matrix);
            return matrix;
        }
    }
}
