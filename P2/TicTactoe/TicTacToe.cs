namespace TicTactoe
{
    internal class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;
        static int flag = 0;

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("플레이어 1: O 와 플레이어 2: X");
                Console.WriteLine("\n");

                if (player == 1)
                {
                    Console.WriteLine("P1 턴");
                }
                else
                {
                    Console.WriteLine("P2 턴");
                }

                Console.WriteLine("\n");
                Board();
                Console.WriteLine("\n");

                Console.Write("입력: ");
                string input = Console.ReadLine();

                // 입력 받은 문자열이 int형으로 변환 가능한지에 대한 bool 값
                bool res = int.TryParse(input, out choice);

                if (res == true)
                {
                    if (arr[choice] != 'X' && arr[choice] != 'O')
                    {
                        if (player == 1)
                        {
                            arr[choice] = 'O';
                        }
                        else
                        {
                            arr[choice] = 'X';
                        }

                        player++;
                    }
                    else
                    {
                        Console.WriteLine("이미 선택된 칸입니다.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("0~9 사이의 숫자를 입력해주세요.");
                }

                flag = CheckWin();
            }
            while (flag != -1 && flag != 1);

            if (flag == 1)
            {
                Console.WriteLine("플레이어 {0}이(가) 이겼습니다.", (player % 2) + 1);
            }
            else
            {
                Console.WriteLine("무승부");
            }

            Console.ReadLine();
        }

        static void Board()
            {
                Console.WriteLine(" {0} {1} {2} ", arr[0], arr[1], arr[2]);
                Console.WriteLine(" {0} {1} {2} ", arr[3], arr[4], arr[5]);
                Console.WriteLine(" {0} {1} {2} ", arr[6], arr[7], arr[8]);
            }

        static int CheckWin()
        {
            {
                // 가로 승리 조건
                if (arr[0] == arr[1] && arr[1] == arr[2])
                {
                    return 1;
                }
                else if (arr[3] == arr[4] && arr[4] == arr[5])
                {
                    return 1;
                }
                else if (arr[6] == arr[7] && arr[7] == arr[8])
                {
                    return 1;
                }

                // 세로 승리 조건
                else if (arr[0] == arr[3] && arr[3] == arr[6])
                {
                    return 1;
                }
                else if (arr[1] == arr[4] && arr[4] == arr[7])
                {
                    return 1;
                }
                else if (arr[2] == arr[5] && arr[5] == arr[8])
                {
                    return 1;
                }

                // 대각선 승리조건
                else if (arr[0] == arr[4] && arr[4] == arr[8])
                {
                    return 1;
                }
                else if (arr[2] == arr[4] && arr[4] == arr[6])
                {
                    return 1;
                }

                // 무승부
                else if (arr[0] != '0' && arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' &&
                    arr[6] != '6' && arr[7] != '7' && arr[8] != '8')
                {
                    return -1;
                }
                else { return 0; }
            }
        }
    }
}