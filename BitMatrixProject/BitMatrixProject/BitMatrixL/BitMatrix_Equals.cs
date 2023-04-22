using System;
using System.Collections;

public partial class BitMatrix : IEquatable<BitMatrix>
{
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, null)) return false;
        if (ReferenceEquals(obj, null)) return false;

        if (obj is BitMatrix) return Equals(obj);
        return false;
    }
    public bool Equals(BitMatrix bitMatrix)
    {
        if(ReferenceEquals(this, null)) return false;
        if(ReferenceEquals(bitMatrix, null)) return false;

        if (NumberOfColumns != bitMatrix.NumberOfColumns || NumberOfRows != bitMatrix.NumberOfRows) return false;

        for (int i = 0; i < data.Length; i++)
        {
            switch(data[i] == bitMatrix.data[i] ? true: false)
            {
                case true:
                    continue;
                case false:
                    return false;
            }
        }

        return true;
    }
    public override int GetHashCode() => HashCode.Combine(NumberOfColumns, NumberOfRows, data.GetHashCode());

    public static bool operator ==(BitMatrix bitMatrix1, BitMatrix bitMatrix2)
    {
        if(ReferenceEquals(bitMatrix1, null) && ReferenceEquals(bitMatrix2, null))
            return true;
        if (ReferenceEquals(bitMatrix1, null) || ReferenceEquals(bitMatrix2, null))
            return false;
        return bitMatrix1.Equals(bitMatrix2);
    }
    public static bool operator !=(BitMatrix bitMatrix1, BitMatrix bitMatrix2)
    {
        if (ReferenceEquals(bitMatrix1, null) && ReferenceEquals(bitMatrix2, null))
            return false;
        if (ReferenceEquals(bitMatrix1, null) || ReferenceEquals(bitMatrix2, null))
            return true;
        return !bitMatrix1.Equals(bitMatrix2);
    }
}