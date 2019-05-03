using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemItem.Formulas
{
    class Cosinus
    {
        static double[] itemlist = null;
        static double[,] matrixACS = null;
        /// <summary>
        ///  Checks whether item id exist in user rating
        /// </summary>
        /// <param name="item">item id</param>
        /// <param name="userIndex">user id</param>
        /// <returns>Boolean</returns>
        public static Boolean CheckItem(int item, int userIndex)
        {

            double[] itemList = itemlist;
            double[,] Matrix = matrixACS;

            int indexFoundItem = 0;
            for (int a = 0; a <= itemList.Length - 1; a++)
            {
                if (itemList[a] == item)
                {
                    indexFoundItem = a + 2;
                    break;
                }
            }
            if (Matrix[userIndex, indexFoundItem] != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  Performs the Adjusted Cosinus Formula
        /// </summary>
        /// <param name="matrix">table</param>
        /// <param name="itemI">product I</param>
        /// <param name="itemJ">product J</param>
        /// <param name="itemList">list of items</param>
        /// <returns>double array</returns>
        public static double[] ACS(double[,] matrix, int itemI, int itemJ, double[] itemList)
        {
            itemlist = itemList;
            matrixACS = matrix;
            double ratingItemI = 0.0;
            double ratingItemJ = 0.0;
            double userAverageRating = 0.0;
            int row = matrix.GetLength(0);

            double[] arraySumItemI = new double[row];
            double[] arraySumItemJ = new double[row];
            double leftDenominator = 0.0;
            double rightDenominator = 0.0;
            int indexItemI = 0;
            int indexItemJ = 0;
            double userAmount = 0;

            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= itemList.Length - 1; j++)
                {
                    int userIndex = i;
                    if (itemList[j] == itemI && matrix[i, j + 2] != 0)
                    {

                        if (CheckItem(itemJ,userIndex))
                        {
                            userAmount++;
                            ratingItemI = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arraySumItemI[indexItemI] = ratingItemI - userAverageRating;
                            leftDenominator += Math.Pow(ratingItemI - userAverageRating, 2);
                            indexItemI++;
                        }

                    }
                    else if (itemList[j] == itemJ && matrix[i, j + 2] != 0)
                    {
                        if (CheckItem(itemI,userIndex))
                        {
                            ratingItemJ = matrix[i, j + 2];
                            userAverageRating = matrix[i, 1];
                            arraySumItemJ[indexItemJ] = ratingItemJ - userAverageRating;
                            rightDenominator += Math.Pow(ratingItemJ - userAverageRating, 2);
                            indexItemJ++;
                        }
                    }
                }
            }
            double numerator = 0;
            double denominator = Math.Sqrt(leftDenominator) * Math.Sqrt(rightDenominator);
            for (int a = 0; a < arraySumItemI.Length - 1; a++)
            {
                numerator += arraySumItemI[a] * arraySumItemJ[a];
            }
            var result = numerator / denominator;
            double[] resultArray = new double[] { result, userAmount };
            return resultArray;
        }
    }
}
