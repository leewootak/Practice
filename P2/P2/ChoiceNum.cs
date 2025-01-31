namespace ChoiceNum
{
    internal class ChoiceNum
    {
        static void Main(string[] args)
        {
            int targetNumber = new Random().Next(1, 101); ;
            int guess = 0;
            int count = 0;

            Console.WriteLine("1부터 100 사이의 숫자를 맞춰보세요.");

            while (guess != targetNumber)
            {
                Console.Write("추측한 숫자를 입력하세요: ");
                guess = int.Parse(Console.ReadLine());
                count++;

                if (guess < targetNumber)
                {
                    Console.WriteLine("좀 더 큰 숫자를 입력하세요.");
                }
                else if (guess > targetNumber)
                {
                    Console.WriteLine("좀 더 작은 숫자를 입력하세요.");
                }
                else
                {
                    Console.WriteLine("축하합니다! 숫자를 맞추셨습니다.");
                    Console.WriteLine("시도한 횟수: " + count);
                }
            }
        }
    }
}
