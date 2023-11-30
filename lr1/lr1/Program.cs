using System;
using System.IO;

class Program
{
    static Dictionary<char, int[]> dict = new Dictionary<char, int[]>() {
            {'0', new int[]{0}},
            {'1', new int[]{1}},
            {'2', new int[]{2}},
            {'3', new int[]{3}},
            {'4', new int[]{4}},
            {'5', new int[]{5}},
            {'6', new int[]{6}},
            {'7', new int[]{7}},
            {'8', new int[]{8}},
            {'9', new int[]{9}},
            {'a', new int[]{0, 1, 2, 3}},
            {'b', new int[]{1, 2, 3, 4}},
            {'c', new int[]{2, 3, 4, 5}},
            {'d', new int[]{3, 4, 5, 6}},
            {'e', new int[]{4, 5, 6, 7}},
            {'f', new int[]{5, 6, 7, 8}},
            {'g', new int[]{6, 7, 8, 9}},
            {'?', new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, }},
        };

    static public int Solve(string str1, string str2)
    {
        if (str1.Length != str2.Length)
        {
            throw new Exception("Не одинаковая длинна строк");
        }

        int mul = 1;

        int[][] arr1 = getArrayComb(str1);
        int[][] arr2 = getArrayComb(str2);

        for (int i = 0; i < arr1.Length; i++)
        {
            mul *= getNum(arr1[i], arr2[i]);
        }
        return mul;
    }

    static int[][] getArrayComb(string str)
    {
        int[][] arr = new int[str.Length][];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = dict[str[i]];
        }
        return arr;
    }

    static int getNum(int[] ar1, int[] ar2)
    {
        int counter = 0;
        for (int i = 0; i < ar1.Length; i++)
        {
            if (ar2.Contains(ar1[i]))
            {
                counter++;
            }
        }
        return counter;
    }

    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        if (lines.Length != 2) throw new Exception("more than two lines");
        if (lines[0].Length != lines[1].Length) throw new Exception("lines have diferent length");

        int res = Solve(lines[0], lines[1]);

        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            writer.WriteLine(res);
        }
    }
}