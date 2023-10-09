using System;
using System.IO;
using DK;

class Program
{
    static void Main()
    {
        using (StreamReader reader = new StreamReader("input.txt"))
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            string[] input = reader.ReadLine().Split();
            int N = int.Parse(input[0]);
            int x1 = int.Parse(input[1]);
            int y1 = int.Parse(input[2]);
            int x2 = int.Parse(input[3]);
            int y2 = int.Parse(input[4]);

            int shortestPath = FloydWarshell.Search(N, x1, y1, x2, y2);

            writer.WriteLine(shortestPath);
        }
    }
}
    