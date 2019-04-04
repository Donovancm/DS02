using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class Cosinus
    {
        public static Boolean checkItem2(int item,double[,] matrix , int userIndex, double[] itemList)
        {
            int indexFoundItem = 0;
            for (int a = 0; a <= itemList.Length - 1; a++)
            {
                if (itemList[a] == item)
                {
                    indexFoundItem = a + 2;
                    break;
                }
            }
            if (matrix[userIndex, indexFoundItem] != 0)
            {
                return true;
            }
            return false;
        }
        public static Boolean checkitem1(int item, double[,] matrix, int userIndex, double[] itemList)
        {
            int indexFoundItem = 0;
            for (int a = 0; a <= itemList.Length-1; a++)
            {
                if (itemList[a] == item)
                {
                    indexFoundItem = a + 2;
                    break;
                }
            }
            if (matrix[userIndex,indexFoundItem] !=0)
            {
                return true;
            }
            return false;
        }
        public static double[] ACS(double[,] matrix, int item1, int item2, double[] itemList)
        {
            double ratingItem1 = 0.0;
            double ratingItem2 = 0.0;
            double userAverageRating = 0.0;
            double upperLeft = 0.0;
            double upperRight = 0.0;
            double lowerLeft = 0.0;
            double lowerRight = 0.0;
            int row = matrix.GetLength(0);
            double[] arrayItem1 = new double[row];
            double[] arrayItem2 = new double[row];
            double[] arrayLowerLeft1 = new double[row];
            double[] arrayLowerLeft2 = new double[row];
            int indexItem1 = 0;
            int indexItem2 = 0;
            double userAmount = 0;

            for (int i = 0; i <= matrix.GetLength(0) -1; i++)
            {
                for (int j = 0; j <= itemList.Length -1; j++)
                {
                    int userIndex = i;
                    if (itemList[j] == item1 && matrix[i,j+2] != 0)
                    {
                       
                        if (checkItem2(item2, matrix, userIndex, itemList))
                        {
                            userAmount++;
                            ratingItem1 = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arrayItem1[indexItem1] = ratingItem1 - userAverageRating;
                            arrayLowerLeft1[indexItem1] = Math.Sqrt(Math.Pow(ratingItem1 - userAverageRating, 2));
                            indexItem1++;
                        }

                    }
                    else if (itemList[j] == item2 && matrix[i, j + 2] != 0)
                    {
                        if (checkitem1(item1, matrix, userIndex, itemList))
                        {
                            ratingItem2 = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arrayItem2[indexItem2] = ratingItem2 - userAverageRating;
                            arrayLowerLeft2[indexItem2] = Math.Sqrt(Math.Pow(ratingItem2 - userAverageRating, 2));
                            indexItem2++;
                        }
                    }
                }
            }
            double upper = 0;
            double lower = 0;
            for (int a = 0; a < arrayItem1.Length - 1; a++)
            {
                upper += arrayItem1[a] * arrayItem2[a];
            }
            for (int a = 0; a < arrayLowerLeft1.Length - 1; a++)
            {
                lower += arrayLowerLeft1[a] * arrayLowerLeft2[a];
            }

            //var upper = upperLeft * upperRight;
            //var lower = lowerLeft * lowerRight;
            var result = upper / lower;
            double[] resultArray = new double[] { result, userAmount};
            return resultArray;
        }
    }
}
