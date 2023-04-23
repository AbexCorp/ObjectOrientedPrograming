using System;
using System.Collections;
using System.Collections.Generic;

public partial class BitMatrix : IEnumerable<int>
{
    public int this[int row, int col]
    {
        get 
        {
            if (row < 0 || col < 0 || row >= NumberOfRows || col >= NumberOfColumns)
                throw new IndexOutOfRangeException();
            return BoolToBit(data[row * NumberOfColumns + col]);
        }
        set
        {
            if (row < 0 || col < 0 || row >= NumberOfRows || col >= NumberOfColumns)
                throw new IndexOutOfRangeException();
            data[row * NumberOfColumns + col] = BitToBool(value);
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        for(int i = 0; i < NumberOfRows * NumberOfColumns; i++)
        {
            yield return BoolToBit(data[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}