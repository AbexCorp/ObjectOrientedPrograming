using System;
using System.Collections;

public partial class BitMatrix
{
    public static BitMatrix Parse(string s)
    {
        if (ReferenceEquals(s, null) || s == "")
            throw new ArgumentNullException();

        string[] lines = s.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int rows = lines.Length;
        int columns = lines[0].Length;
        
        for(int i = 0; i < rows; i++)
        {
            if (lines[i].Length != columns)
                throw new FormatException();
        }

        BitMatrix parsedMatrix = new BitMatrix(rows, columns);


        int counter = 0;
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                switch (lines[i][j])
                {
                    case '1':
                        parsedMatrix.data[counter] = true;
                        break;
                    case '0':
                        parsedMatrix.data[counter] = false;
                        break;

                    default:
                        throw new FormatException();
                }
                counter++;
            }
        }

        return parsedMatrix;
    }

    public static bool TryParse(string s, out BitMatrix result)
    {
        try
        {
            result = BitMatrix.Parse(s);
        }
        catch(Exception)
        {
            result = null;
            return false;
        }
        return true;
    }
}