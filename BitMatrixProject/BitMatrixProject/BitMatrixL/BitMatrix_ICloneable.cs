using System;
using System.Collections;

public partial class BitMatrix : ICloneable
{
    public object Clone()
    {
        BitMatrix clonedMatrix = new BitMatrix(NumberOfRows, NumberOfColumns);
        for(int i = 0; i < clonedMatrix.data.Length; i++)
        {
            clonedMatrix.data[i] = data[i];
        }
        return clonedMatrix;
    }
}