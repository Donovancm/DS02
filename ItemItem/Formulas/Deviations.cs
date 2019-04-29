using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Formulas
{
    class Deviations
    {
        public static double[,] devMatrixSimilarity { get; set; }
        public static double[,] devMatrixUserAmount { get; set; }

        public static void CreateDeviationMatrix(double[] itemList, double[,] matrix)
        {
            devMatrixSimilarity = new double[itemList.Length, itemList.Length];
            devMatrixUserAmount = new double[itemList.Length, itemList.Length];
            for (int i = 0; i <= itemList.Length - 1; i++)
            {
                for (int a = 0; a <= itemList.Length - 1; a++)
                {

                    if (itemList[i] == itemList[a])
                    {
                        devMatrixSimilarity[i, a] = 0;
                        devMatrixUserAmount[i, a] = 0;
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
                            double[] resultArray = Formulas.Cosinus.ACS(matrix, item1, item2, itemList);
                            devMatrixSimilarity[i, a] = resultArray[0];
                            devMatrixUserAmount[i, a] = resultArray[1];
                        }

                    }

                }
                //Console.WriteLine(i);
            }
        }
        public static Boolean checkCombination(int itemIndex1, int itemIndex2)
        {
            if (devMatrixSimilarity[itemIndex1, itemIndex2] == 0 && devMatrixSimilarity[itemIndex2, itemIndex1] == 0)
            {
                return true;
            }
            return false;
        }


    }
}
