namespace lr5
{
    public class lr1
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
    }

    public class lr2
    {
        public static int Resolve(int n)
        {
            List<int> sequence = new List<int>();
            sequence.Add(1);

            while (true)
            {
                if (sequence.Count == n)
                {
                    return sequence.Last<int>();
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