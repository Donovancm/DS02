﻿using System;
using System.Collections.Generic;

namespace ItemItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new[,] {
                {1,101,2.5},
                { 1,102,3.5},
                { 1,103,3.0},
                { 1,104,3.5},
                { 1,105,2.5},
                { 1,106,3.0},
                { 2,101,3.0},
                { 2,102,3.5},
                { 2,103,1.5},
                { 2,104,5.0},
                { 2,105,3.5},
                { 2,106,3.0},
                { 3,101,2.5},
                { 3,102,3.0},
                { 3,104,3.5},
                { 3,106,4.0},
                { 4,102,3.5},
                { 4,103,3.0},
                { 4,104,4.0},
                { 4,105,2.5},
                { 4,106,4.5},
                { 5,101,3.0},
                { 5,102,4.0},
                { 5,103,2.0},
                { 5,104,3.0},
                { 5,105,2.0},
                { 5,106,3.0},
                { 6,101,3.0},
                { 6,102,4.0},
                { 6,104,5.0},
                { 6,105,3.5},
                { 6,106,3.0},
                { 7,102,4.5},
                { 7,104,4.0},
                { 7,105,1.0}
        };
            Dictionary<int, double[,]> dictionaryBasic = FileReader.GetData(data);
            var itemList = FileReader.GetItemList(data);
            var matrix = Matrices.CreateMatrix.Create(itemList, dictionaryBasic);
            Formulas.Cosinus.ACS(matrix,103,104,itemList);
        }
    }
}
