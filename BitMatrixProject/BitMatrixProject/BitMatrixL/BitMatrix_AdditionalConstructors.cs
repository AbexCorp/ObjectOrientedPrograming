using System;
using System.Collections;
using System.Text;

public partial class BitMatrix
{
    public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
    {
        data = new BitArray(numberOfRows * numberOfColumns);
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;


        if (bits == null)
        {
            for (int i = 0; i < NumberOfRows * numberOfColumns; i++)
            {
                data[i] = false;
            }
        }
        else
        {
            int counter = 0;
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    try
                    {
                        data[counter] = BitToBool(bits[counter]);
                        counter++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        data[counter] = false;
                        counter++;
                    }
                }
            }
        }
    }

    public BitMatrix(int[,] bits)
    {
        if (bits == null)
            throw new NullReferenceException();
        if (bits.GetLength(0) < 1 || bits.GetLength(1) < 1)
            throw new ArgumentOutOfRangeException();

        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new BitArray(NumberOfRows * NumberOfColumns);

        int counter = 0;
        for(int i = 0; i < NumberOfRows; i++)
        {
            for(int j = 0; j < NumberOfColumns; j++)
            {
                data[counter] = BitToBool(bits[i, j]);
                counter++;
            }
        }
    }

    public BitMatrix(bool[,] bits)
    {
        if (bits == null)
            throw new NullReferenceException();
        if (bits.GetLength(0) < 1 || bits.GetLength(1) < 1)
            throw new ArgumentOutOfRangeException();

        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new BitArray(NumberOfRows * NumberOfColumns);

        int counter = 0;
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                data[counter] = bits[i, j];
                counter++;
            }
        }
    }
}