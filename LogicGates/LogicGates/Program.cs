using System;

namespace LogicGate
{
    class Program
    {
        static void Main()
        {
            AdderTest();
        }
        public static void AdderTest()
        {
            Dictionary<int, string> numbers = new Dictionary<int, string>
            {
                { 0, "0000" },
                { 1, "0001" },
                { 2, "0010" },
                { 3, "0011" },
                { 4, "0100" },
                { 5, "1011" },
                { 6, "1100" },
                { 7, "1101" },
                { 8, "1110" },
                { 9, "1111" }
            };

            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    bool overflow = (i + j > 9);
                    string expectedOutput = overflow ? "00001" : $"{numbers[i + j]}0";
                    string output = Adder(numbers[i], numbers[j]);
                    if(overflow)
                    {
                        if(output[4] != '1')
                        {
                            Console.WriteLine();
                            Console.WriteLine($" Failed {i}+{j} Output: {output} exOut: {expectedOutput} ");
                            continue;
                        }
                    }
                    else
                    {
                        if(output != expectedOutput)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Failed {i}+{j} Output: {output} exOut: {expectedOutput} ");
                            continue;
                        }
                    }
                    Console.Write(".");
                }
            }
        }

        public static string Adder(string numA, string numB)
        {
            bool a1 = numA[3] == '1';
            bool a2 = numA[2] == '1';
            bool a3 = numA[1] == '1';
            bool a4 = numA[0] == '1';
            
            bool b1 = numB[3] == '1';
            bool b2 = numB[2] == '1';
            bool b3 = numB[1] == '1';
            bool b4 = numB[0] == '1';



            bool xor1 = a1 ^ b1;
            bool xor2 = a2 ^ b2;
            bool xor3 = a3 ^ b3;
            bool xor4 = a4 ^ b4;

            bool and1 = a1 && b1;
            bool and2 = a2 && b2;
            bool and3 = a3 && b3;
            bool and4 = a4 && b4;


            bool xor5 = and1 ^ xor2;
            bool and5 = and1 && xor2;

            bool or1 = and5 || and2;
            bool xor6 = or1 ^ xor3;
            bool and6 = or1 && xor3;

            bool or2 = and6 || and3;
            bool xor7 = or2 ^ xor4;
            bool and7 = or2 && xor4;
            bool or3 = and7 || and4;



            bool o1 = xor1;
            bool o2 = xor5;
            bool o3 = xor6;
            bool o4 = xor7;
            bool co = or3;

            return $"{Convert.ToInt32(o4)}{Convert.ToInt32(o3)}{Convert.ToInt32(o2)}{Convert.ToInt32(o1)}{Convert.ToInt32(co)}";
        }



        public static void ComparatorTester() //100 - A<B     010 - A=B     001 - A>B
        {
            int passed = 0;
            int failed = 0;
            for(int a = 0; a < 16; a++)
            {
                for(int b = 0; b < 16; b++)
                {
                    string expectedResult = "";
                    if (a > b) expectedResult = "001";
                    else if (a < b) expectedResult = "100";
                    else if (a == b) expectedResult = "010";

                    string aNum = Convert.ToString(a, 2);
                    string bNum = Convert.ToString(b, 2);
                    aNum = aNum.PadLeft(4, '0');
                    bNum = bNum.PadLeft(4, '0');

                    string compare = Comparator(aNum, bNum);
                    if (expectedResult != compare)
                    {
                        Console.WriteLine($"A-{a} {aNum}   B-{b} {bNum}");
                        Console.WriteLine($"Expected-{expectedResult} Recieved-{compare}");
                        Console.WriteLine();
                        failed++;
                    }
                    else
                    {
                        Console.Write('.');
                        passed++;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Passed: {passed}  Failed: {failed}");
        }
        public static string Comparator(string numA, string numB)
        {
            bool a1 = numA[3] != '1' && numB[3] == '1';
            bool b1 = numB[3] != '1' && numA[3] == '1';
            
            bool a2 = numA[2] != '1' && numB[2] == '1';
            bool b2 = numB[2] != '1' && numA[2] == '1';
            
            bool a3 = numA[1] != '1' && numB[1] == '1';
            bool b3 = numB[1] != '1' && numA[1] == '1';
            
            bool a4 = numA[0] != '1' && numB[0] == '1';
            bool b4 = numB[0] != '1' && numA[0] == '1';

            bool nor4 = !(a4 || b4);
            bool nor3 = !(a3 || b3);
            bool nor2 = !(a2 || b2);
            bool nor1 = !(a1 || b1);

            bool and1 = nor4 && nor3;
            bool and4 = nor4 && b3;
            bool and3 = nor4 && a3;
            bool and2 = and1 && nor2;

            bool and5 = and2 && nor1;
            bool and6 = and2 && b1;
            bool and7 = and1 && a2;
            bool and8 = and1 && b2;
            bool and9 = and2 && a1;

            bool aIsBigger = b4 || and4 || and8 || and6;
            bool aIsSmaller = a4 || and3 || and7 || and9;
            bool aIsEqual = and5;

            return $"{Convert.ToInt32(aIsSmaller)}{Convert.ToInt32(aIsEqual)}{Convert.ToInt32(aIsBigger)}";
        }



        public static void TestParityChecker()
        {
            string[] tests =
            {
                "00000",
                "00001",
                "00010",
                "00011",
                "00100",
                "00101",
                "00110",
                "00111",
                "01000",
                "01001",
                "01010",
                "01011",
                "01100",
                "01101",
                "01110",
                "01111",
                "10000",
                "10001",
                "10010",
                "10011",
                "10100",
                "10101",
                "10110",
                "10111",
                "11000",
                "11001",
                "11010",
                "11011",
                "11100",
                "11101",
                "11110",
                "11111"
            };
            char[] expectedOutputs =
            {
                '0',
                '1',
                '1',
                '0',
                '1',
                '0',
                '0',
                '1',
                '1',
                '0',
                '0',
                '1',
                '0',
                '1',
                '1',
                '0',
                '1',
                '0',
                '0',
                '1',
                '0',
                '1',
                '1',
                '0',
                '0',
                '1',
                '1',
                '0',
                '1',
                '0',
                '0',
                '1'
            };

            for(int i = 0; i < 32; i++)
            {
                bool exO = expectedOutputs[i] == '1';
                if (exO == ParityChecker(tests[i]))
                    Console.WriteLine("Pass");
                else
                    Console.WriteLine($"Expected: {exO}, got: {ParityChecker(tests[i])}, input {tests[i]}");
            }
        }

        public static bool ParityChecker(string input)
        {
            bool i1 = input[0] == '1';
            bool i2 = input[1] == '1';
            bool i3 = input[2] == '1';
            bool i4 = input[3] == '1';
            bool p = input[4] == '1';

            i1 ^= i2;
            i3 ^= i4;

            i1 ^= i3;

            return i1 ^ p;
        }
    }
}