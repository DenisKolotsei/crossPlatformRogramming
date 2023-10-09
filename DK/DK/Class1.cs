namespace DK
{
    public static class FloydWarshell
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