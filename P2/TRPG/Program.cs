namespace TRPG
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            Enter();
            Exit();

            // 첫 선택지 제공
            static void Enter()
            {
                Console.WriteLine("------------------------------------------------------" +
                "\n\n태초 마을에 오신 여러분 환영합니다." +
                "\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다." +
                "\n\n1. 상태 보기" +
                "\n2. 인벤토리" +
                "\n3. 상점" +
                "\n\n------------------------------------------------------" +
                "\n원하시는 행동을 입력해주세요.");
                string choice = Console.ReadLine();

                // 캐릭터 정보
                if (choice == "1")
                {
                    Console.WriteLine("------------------------------------------------------" +
                        $"\n\nLv. 01" +
                        $"\nChad (전사)" +
                        $"\n공격력 : 10" +
                        $"\n방어력 : 5" +
                        $"\n체력 : 100" +
                        $"\nGold : 1500 G" +
                        "\n\n------------------------------------------------------");
                    Console.WriteLine("0. 나가기");
                    Exit();
                }

                // 인벤토리
                else if (choice == "2")
                {
                    Console.WriteLine("------------------------------------------------------" +
                        "\n\n[아이템 목록]" +
                        "\n\n1. 장착 관리");
                    InvenExit();
                }

                // 상점
                else if (choice == "3")
                {

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                }
            }

            // 첫 선택지로 이동
            static void Exit()
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                string back = Console.ReadLine();

                bool exit = false;

                while (!exit)
                {
                    if (back == "0")
                    {
                        exit = true;
                        Enter();
                    }
                                        
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadLine();
                    }
                }            
            }

            // 인벤토리 선택지
            static void InvenExit()
            {
                Console.WriteLine("0. 나가기" +
                    "\n원하시는 행동을 입력해주세요.");
                string invenChoice = Console.ReadLine();

                bool invenExit = false;

                while (!invenExit)
                {
                    if (invenChoice == "1")
                    {                        
                        Equip();
                    }

                    else if (invenChoice == "0")
                    {
                        invenExit = true;
                        Equip();
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadLine();
                    }
                }
            }
            
            static void Equip()
            {
                bool setting = false, isEquip1 = false, isEquip2 = false, isEquip3 = false;

                while (!setting)
                {
                    Console.WriteLine("------------------------------------------------------" +
                            "\n\n[아이템 목록]" +
                            "\n1 무쇠갑옷 | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다." +
                            "\n2 스파르타의 창 | 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다." +
                            "\n3 낡은 검 | 공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다." +
                            "\n0. 나가기");

                    string equipChoice = Console.ReadLine();

                    if (equipChoice == "1")
                    {
                        isEquip1 = true;
                        Console.WriteLine("------------------------------------------------------" +
                            $"\n{1} 장착하였습니다.");
                    }

                    else if (equipChoice == "2")
                    {
                        isEquip2 = true;
                        Console.WriteLine("------------------------------------------------------" +
                            $"\n{2} 장착하였습니다.\n");
                        InvenExit();
                    }

                    else if (equipChoice == "3")
                    {
                        isEquip3 = true;
                        Console.WriteLine("------------------------------------------------------" +
                            $"\n{3} 장착하였습니다.\n");
                        InvenExit();
                    }

                    else if (equipChoice == "0")
                    {
                        setting = true;
                        Exit();
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
