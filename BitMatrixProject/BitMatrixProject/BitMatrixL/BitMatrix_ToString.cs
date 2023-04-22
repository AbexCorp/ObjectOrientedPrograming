using System;
using System.Collections;
using System.Text;

public partial class BitMatrix
{
    public override string ToString()
    {
        StringBuilder completeArray = new StringBuilder();
        for(int i = 0; i < NumberOfRows; i++)
        {
            for(int j = 0; j < NumberOfColumns; j++)
            {
                completeArray.Append(BoolToBit(data[i+j]));
            }
            completeArray.Append(Environment.NewLine);
        }
        return completeArray.ToString();
    }
}