namespace lr4Lib
{
    public class lr1
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

        public static void Resolve(string pathInput, string pathOutput)
        {
            string[] lines = File.ReadAllLines(pathInput);
            int[] combinations = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                combinations[i] = PossibleCombinations(lines[i]);
            }

            using (StreamWriter writer = new StreamWriter(pathOutput))
            {
                int commonCombinations = 1;
                for (int i = 0; i < combinations.Length; i++)
                {
                    commonCombinations *= combinations[i];

                    writer.WriteLine(combinations[i]);
                }
                writer.WriteLine(commonCombinations);
            }
        }
    }

    public class lr2
    {
        public static void Resolve(string pathInput, string pathOutput)
        {
            // Читаємо n з файлу INPUT.TXT
            int n = int.Parse(File.ReadAllText(pathInput));

            List<int> sequence = new List<int>();
            sequence.Add(1);

            while (true)
            {
                if (sequence.Count == n)
                {
                    File.WriteAllText(pathOutput, sequence.Last<int>().ToString());
                    break;
                }
                int next = FindNextElement(sequence);
                sequence.Add(next);
            }
        }

        static int FindNextElement(List<int> sequence)
        {
            int[] factors = new int[3] { 2, 3, 5 };

            int currentLast = sequence.Last<int>();
            int nextElement = currentLast * factors[0];

            for (int i = 0; i < sequence.Count; i++)
            {
                foreach (int factor in factors)
                {
                    if (sequence[i] * factor > currentLast && sequence[i] * factor < nextElement)
                    {
                        nextElement = sequence[i] * factor;
                    }
                }
            }
            return nextElement;
        }
    }

    public class lr3
    {
        public static void Resolve(string pathInput, string pathOutput)
        {
            using (StreamReader reader = new StreamReader(pathInput))
            using (StreamWriter writer = new StreamWriter(pathOutput))
            {
                string[] input = reader.ReadLine().Split();
                int N = int.Parse(input[0]);
                int x1 = int.Parse(input[1]);
                int y1 = int.Parse(input[2]);
                int x2 = int.Parse(input[3]);
                int y2 = int.Parse(input[4]);

                int shortestPath = Search(N, x1, y1, x2, y2);

                writer.WriteLine(shortestPath);
            }
        }

        public static int Search(int N, int x1, int y1, int x2, int y2)
        {
            int[,] distance = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }

            x1--;
            y1--;
            x2--;
            y2--;

            distance[x1, y1] = 0;

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((x1, y1));

            while (queue.Count > 0)
            {
                (int x, int y) = queue.Dequeue();

                int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
                int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

                for (int i = 0; i < 8; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];

                    if (newX >= 0 && newX < N && newY >= 0 && newY < N)
                    {
                        if (distance[newX, newY] == int.MaxValue)
                        {
                            distance[newX, newY] = distance[x, y] + 1;
                            queue.Enqueue((newX, newY));
                        }
                    }
                }
            }

            int shortestPath = distance[x2, y2];
            return shortestPath;
        }
    }
}