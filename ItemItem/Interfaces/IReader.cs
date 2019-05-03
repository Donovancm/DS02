using System;
using System.Collections.Generic;
using System.Text;

namespace ItemItem.Interfaces
{
    /// <summary>
    /// Interface for Reading data, interface method is being used for datareader and filereader
    /// </summary>
    interface IReader
    {
        Dictionary<int, double[,]> GetData();
    }
}
