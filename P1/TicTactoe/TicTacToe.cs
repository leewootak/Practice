namespace TicTactoe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] board = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("□ ");
                }
                Console.WriteLine();
            }
        }
    }
}

// 추후에 진행
