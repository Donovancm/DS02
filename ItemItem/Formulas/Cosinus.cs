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

            List<double> items = itemList.OfType<double>().ToList();
            int index = items.BinarySearch(item);
            if (index >= 0)
            {
                indexFoundItem = index + 2;
                if (Matrix[userIndex, indexFoundItem] != 0)
                {
                    return true;
                }
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
            matrixACS = matrix;
            itemlist = itemList;
            double ratingItemI = 0.0;
            double ratingItemJ = 0.0;
            double userAverageRating = 0.0;
            int row = matrix.GetLength(0);
            double[] arraySumItemI = new double[row];
            double[] arraySumItemJ = new double[row];
            double leftDenominator = 0.0;
            double rightDenominator = 0.0;
            int indexItem1 = 0;
            int indexItemJ = 0;
            double userAmount = 0;

            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                List<double> items = itemList.OfType<double>().ToList();
                int indexI = items.BinarySearch(itemI);
                int indexJ = items.BinarySearch(itemJ);
                int userIndex = i;
                if (indexI >= 0)
                {
                    if (matrix[i, indexI + 2] != 0)
                    {
                        // checks combination with itemJ example: 101->102 && 102->101
                        if (CheckItem(itemJ, userIndex))
                        {
                            userAmount++;
                            ratingItemI = matrix[i, indexI + 2];
                            userAverageRating = matrix[i, 1];
                            arraySumItemI[indexItem1] = ratingItemI - userAverageRating;
                            leftDenominator += Math.Pow(ratingItemI - userAverageRating, 2);
                            indexItem1++;

                        }

                    }
                }
                if (indexJ >= 0)
                {
                    if (matrix[i, indexJ + 2] != 0)
                    {
                        if (CheckItem(itemI, userIndex))
                        {
                            ratingItemJ = matrix[i, indexJ + 2];
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
            double[] resultArray = new double[] { result, userAmount};
            return resultArray;
        }
    }
}
