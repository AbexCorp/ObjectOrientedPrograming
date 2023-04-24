using System;
using System.Collections;

public partial class BitMatrix
{
    public static explicit operator BitMatrix(int[,] array)
    {
        BitMatrix bitMatrix = new BitMatrix(array.GetLength(0), array.GetLength(1));

        int counter = 0;
        for(int i = 0; i < array.GetLength(0); i++)
        {
            for(int j = 0; j < array.GetLength(1); j++)
            {
                bitMatrix.data[counter] = array[i, j] == 0 ? false : true;
                counter++;
            }
        }
        return bitMatrix;
    }

    public static implicit operator int[,](BitMatrix bitMatrix)
    {
        int[,] convertedArray = new int[bitMatrix.NumberOfRows,bitMatrix.NumberOfColumns];

        int counter = 0;
        for (int i = 0; i < bitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < bitMatrix.NumberOfColumns; j++)
            {
                convertedArray[i,j] = BoolToBit(bitMatrix.data[counter]);
                counter++;
            }
        }
        return convertedArray;
    }



    public static explicit operator BitMatrix(bool[,] array)
    {
        if(ReferenceEquals(array, null))
            throw new NullReferenceException();
        if (array.GetLength(0) < 1 || array.GetLength(1) < 1)
            throw new ArgumentOutOfRangeException();

        BitMatrix bitMatrix = new BitMatrix(array.GetLength(0), array.GetLength(1));

        int counter = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                bitMatrix.data[counter] = array[i, j];
                counter++;
            }
        }
        return bitMatrix;
    }

    public static implicit operator bool[,](BitMatrix bitMatrix)
    {
        bool[,] convertedArray = new bool[bitMatrix.NumberOfRows, bitMatrix.NumberOfColumns];

        int counter = 0;
        for (int i = 0; i < bitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < bitMatrix.NumberOfColumns; j++)
            {
                convertedArray[i, j] = bitMatrix.data[counter];
                counter++;
            }
        }
        return convertedArray;
    }



    public static explicit operator BitArray(BitMatrix bitMatrix)
    {
        BitArray convertedArray = new BitArray(bitMatrix.NumberOfRows * bitMatrix.NumberOfColumns, false);

        int counter = 0;
        for (int i = 0; i < bitMatrix.NumberOfRows; i++)
        {
            for (int j = 0; j < bitMatrix.NumberOfColumns; j++)
            {
                convertedArray[counter] = bitMatrix.data[counter];
                counter++;
            }
        }
        return convertedArray;
    }
}