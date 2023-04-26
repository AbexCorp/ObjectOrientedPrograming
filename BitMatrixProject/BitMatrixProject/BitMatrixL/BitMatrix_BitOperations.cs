using System;
using System.Collections;

public partial class BitMatrix
{
    private void CheckIfSameSizes(BitMatrix other)
    {
        if (ReferenceEquals(other, null))
            throw new ArgumentNullException();
        if (NumberOfRows != other.NumberOfRows || NumberOfColumns != other.NumberOfColumns)
            throw new ArgumentException();
    }
    public BitMatrix And(BitMatrix other)
    {
        CheckIfSameSizes(other);
        data = data.And(other.data);
        return this;
    }

    public BitMatrix Or(BitMatrix other)
    {
        CheckIfSameSizes(other);
        data = data.Or(other.data);
        return this;
    }
    public BitMatrix Xor(BitMatrix other)
    {
        CheckIfSameSizes(other);
        data = data.Xor(other.data);
        return this;
    }
    public BitMatrix Not()
    {
        data = data.Not();
        return this;
    }



    private static void CheckIfSameSizes(BitMatrix matrix1, BitMatrix matrix2)
    {
        if (ReferenceEquals(matrix1, null) || ReferenceEquals(matrix2, null))
            throw new ArgumentNullException();
        if (matrix1.NumberOfRows != matrix2.NumberOfRows || matrix1.NumberOfColumns != matrix2.NumberOfColumns)
            throw new ArgumentException();
    }
    private static void CheckIfNull(BitMatrix matrix)
    {
        if (ReferenceEquals(matrix, null))
            throw new ArgumentNullException();
    }

    public static BitMatrix operator &(BitMatrix matrix1, BitMatrix matrix2)
    {
        CheckIfSameSizes(matrix1, matrix2);
        BitMatrix temp = (BitMatrix)matrix1.Clone();
        temp = temp.And((BitMatrix)matrix2.Clone());
        return temp;
    }
    public static BitMatrix operator |(BitMatrix matrix1, BitMatrix matrix2)
    {
        CheckIfSameSizes(matrix1, matrix2);
        BitMatrix temp = (BitMatrix)matrix1.Clone();
        temp = temp.Or((BitMatrix)matrix2.Clone());
        return temp;
    }
    public static BitMatrix operator ^(BitMatrix matrix1, BitMatrix matrix2)
    {
        CheckIfSameSizes(matrix1, matrix2);
        BitMatrix temp = (BitMatrix)matrix1.Clone();
        temp = temp.Xor((BitMatrix)matrix2.Clone());
        return temp;
    }
    public static BitMatrix operator !(BitMatrix matrix)
    {
        CheckIfNull(matrix);
        BitMatrix temp = (BitMatrix)matrix.Clone();
        temp = temp.Not();
        return temp;
    }
}