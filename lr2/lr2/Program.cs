using System;
using System.IO;
using System.Net.Http.Headers;

class Program
{
    static void Main()
    {
        // Читаємо n з файлу INPUT.TXT
        int n = int.Parse(File.ReadAllText("input.txt"));

        List<int> sequence = new List<int>();
        sequence.Add(1);

        while (true)
        {
            if (sequence.Count == n)
            {
                File.WriteAllText("output.txt", sequence.Last<int>().ToString());
                break;
            }
            int next = FindNextElement(sequence);
            Console.WriteLine(next.ToString());
            sequence.Add(next);
        }
    }

    static int FindNextElement(List<int> sequence)
    {
        int[] factors = new int[3] {2, 3, 5};

        int currentLast = sequence.Last<int>();
        int nextElement = currentLast * factors[0];

        for (int i = 0; i < sequence.Count; i++)
        {
            foreach(int factor in factors)
            {
                if (sequence[i] * factor > currentLast && sequence[i] * factor < nextElement)
                {
                    nextElement = sequence[i] * factor;
                }
            }
        }
        return nextElement;
    }

    /*static int FindNextElement(int current)
    {
        // Шукаємо наступний елемент послідовності
        for (int i = 2; ; i++)
        {
            int candidate = current * i;

            if (IsPrime(i) && candidate > current)
            {
                return candidate;
            }
        }
    }*/

    static bool IsPrime(int number)
    {
        // Перевіряємо, чи є число простим
        //if (number <= 1) return false;
        //if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0) return false;
        }

        return true;
    }
}