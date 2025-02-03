namespace ChoiceNum2
{
    internal class ChoiceNum2
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[3];
            Random random = new Random();
            int attempt = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(1, 10);
            }

            while (true)
            {
                Console.WriteLine("3개의 숫자를 입력하세요 (1~9)");
                int[] guesses = new int[3];

                for (int i = 0; i < guesses.Length; i++)
                {
                    guesses[i] = int.Parse(Console.ReadLine());
                }

                int correct = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    for (int j = 0; j < guesses.Length; j++)
                    {
                        if (numbers[i] == guesses[j])
                        {
                            correct++;
                            break;
                        }
                    }
                }

                attempt++;
                Console.WriteLine("시도: " + attempt + "회, 맞춘 개수: " + correct + "개");

                if (correct == 3)
                {
                    Console.WriteLine("정답입니다");
                    break;
                }
            }
        }
    }
}