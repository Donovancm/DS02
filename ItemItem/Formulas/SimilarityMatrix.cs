using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class SimilarityMatrix
    {
        public static double[,] MatrixSimilarityValues { get; set; }
        public static double[,] MatrixSimilarityUserAmount { get; set; }

        public static void CreateSimilarityMatrix(double[] itemList, double[,] matrix)
        {
            MatrixSimilarityValues = new double[itemList.Length, itemList.Length];
            MatrixSimilarityUserAmount = new double[itemList.Length, itemList.Length];
            for (int i = 0; i <= itemList.Length - 1; i++)
            {
                for (int a = 0; a <= itemList.Length - 1; a++)
                {
                    if (itemList[i] == itemList[a])
                    {
                        MatrixSimilarityValues[i, a] = 0;
                        MatrixSimilarityUserAmount[i, a] = 0;
                        // set null 
                    }
                    else if (itemList[i] != itemList[a])
                    {
                        //set rating and calculate avg rating
                        // check if combination exist if false
                        if (checkCombination(i, a))
                        {
                            int item1 = int.Parse(itemList[i] + "");
                            int item2 = int.Parse(itemList[a] + "");
                            double[] resultArray = Cosinus.ACS(matrix, item1, item2, itemList);
                            MatrixSimilarityValues[i, a] = resultArray[0];
                            MatrixSimilarityUserAmount[i, a] = resultArray[1];
                        }
                        else
                        {
                            int item1 = int.Parse(itemList[i] + "");
                            int item2 = int.Parse(itemList[a] + "");
                            double[] resultArray = Cosinus.ACS(matrix, item1, item2, itemList);
                            MatrixSimilarityValues[i, a] = Math.Abs(resultArray[0]);
                            MatrixSimilarityUserAmount[i, a] = resultArray[1];
                        }

                    }

                }
               
            }
        }
        public static Boolean checkCombination(int itemIndex1, int itemIndex2)
        {
            if (MatrixSimilarityValues[itemIndex1, itemIndex2] == 0 && MatrixSimilarityValues[itemIndex2, itemIndex1] == 0)
            {
                return true;
            }
            return false;
        }


    }
}
