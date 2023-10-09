using System;
using System.IO;

class Program
{
    static int PossibleCombinations(string template)
    {
        int[] possibilities = new int[template.Length];

        for (int i = 0; i < template.Length; i++)
        {
            switch (template[i])
            {
                case '?':
                    possibilities[i] = 10;
                    break;
                case 'a':
                    possibilities[i] = 4; 
                    break;
                case 'c':
                    possibilities[i] = 4;
                    break;
                case 'd':
                    possibilities[i] = 4;
                    break;
                case 'e':
                    possibilities[i] = 4;
                    break;
                case 'f':
                    possibilities[i] = 4;
                    break;
                case 'g':
                    possibilities[i] = 4;
                    break;
                default:
                    possibilities[i] = 1;
                    break;
            }
        }

        int totalPossibilities = 1;
        foreach (int possibility in possibilities)
        {
            totalPossibilities *= possibility;
        }

        return totalPossibilities;
    }

    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        int[] combinations = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            Console.WriteLine($"p{i+1}: {lines[i]}");
            combinations[i] = PossibleCombinations(lines[i]);
        }

        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            int commonCombinations = 1;
            for (int i = 0; i < combinations.Length; i++)
            {
                Console.WriteLine($"Number possible combinations p{i+1}: {combinations[i]}");
                commonCombinations *= combinations[i];

                writer.WriteLine(combinations[i]);
            }
            Console.WriteLine($"Number common combinations: {commonCombinations}");
            writer.WriteLine(commonCombinations);
        }
    }
}