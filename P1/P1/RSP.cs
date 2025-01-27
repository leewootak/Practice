namespace P1
{
    internal class RSP
    {
        static void Main(string[] args)
        {
            string[] choices = { "가위", "바위", "보" };
            string playerChoice = "";
            string computerChoice = choices[new Random().Next(0, 3)];

            while (playerChoice != computerChoice)
            {
                Console.Write("가위, 바위, 보 중 하나를 선택하세요: ");
                playerChoice = Console.ReadLine();

                Console.WriteLine("컴퓨터: " + computerChoice);

                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("비겼습니다!");
                }
                else if ((playerChoice == "가위" && computerChoice == "보") ||
                         (playerChoice == "바위" && computerChoice == "가위") ||
                         (playerChoice == "보" && computerChoice == "바위"))
                {
                    Console.WriteLine("플레이어 승리!");
                }
                else
                {
                    Console.WriteLine("컴퓨터 승리!");
                }
            }
        }
    }
}
