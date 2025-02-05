namespace TRPG
{
    internal class program
    {
        // 캐릭터 클래스
        public class Character
        {
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; }
            public int Def { get; }
            public int Hp { get; set; }
            public float Gold { get; set; }

            public Character(string job, int level, int atk, int def, int hp, float gold)
            {
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
            }
        }

        // 아이템 클래스
        public class Item
        {
            public string Name { get; }
            public string Description { get; } // 아이템 설명
            public int Type { get; } // 무기/방어구 구분
            public int Atk { get; }
            public int Def { get; }
            public float Gold { get; set; }
            public bool IsEquip { get; set; }
            public bool IsBuy { get; set; }
            public static int ItemCount = 0;

            public Item(string name, string description, int type, int atk, int def, float gold, bool isEquip = false)
            {
                Name = name;
                Description = description;
                Type = type;
                Atk = atk;
                Def = def;
                Gold = gold;
                IsEquip = isEquip;
                IsBuy = false;
            }

            // 아이템 로직
            public void ShowDescription(bool itemNum = false, int idx = 0, bool price = true)
            {
                if (itemNum)
                {
                    Console.Write("{0}. ", idx);
                }

                if (IsEquip) // 아이템 착용 시
                {
                    Console.Write("[E]");
                }

                Console.Write(Name + " | ");

                // 장비 장착 시 스탯 추가
                if (Atk != 0)
                {
                    Console.Write($"Atk {(Atk >= 0 ? " + " : "")}{Atk}"); 
                }

                if (Def != 0)
                {
                    Console.Write($"Def {(Def >= 0 ? " + " : "")}{Def}");
                }

                Console.Write(" | " + Description);

                // 품절 상태 확인
                if (price)
                {
                    string itemStatus = IsBuy ? "구매완료" : Gold + " G";
                    Console.WriteLine(" | " + itemStatus);
                }
                Console.WriteLine();
            }

            // 사용할 아이템 세팅
            private static void ItemSetting()
            {
                player = new Character("전사", 1, 10, 5, 100, 5000);
                items = new Item[8];

                AddItem(new Item("회색 츄리닝", "일상에서 입는 갑옷(?)입니다.", 0, 0, 2, 500)); // 첫번째 숫자가 0이면 방어구, 1이면 무기
                AddItem(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 0, 5, 1000));
                AddItem(new Item("무쇠 갑옷", "무쇠로 만들어진 튼튼한 갑옷입니다.", 0, 0, 9, 2000));
                AddItem(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 0, 15, 3500));
                AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 600));
                AddItem(new Item("청동 도끼", "쉽게 볼 수 있는 낡은 검입니다.", 1, 5, 0, 1500));
                AddItem(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, 7, 0, 3000));
                AddItem(new Item("엑스칼리버", "아무나 사용할 수 없는 검입니다.", 1, 10, 0, 5000));

            }

            // 아이템 추가
            static void AddItem(Item item)
            {
                if (Item.ItemCount == 8)
                    return;

                items[Item.ItemCount] = item;
                Item.ItemCount++;
            }

            // ======================================= 실행 =========================================
            static Character player;
            static Item[] items;

            static void Main(string[] args)
            {
                ItemSetting();
                Start();
            }

            // ======================================= 시작 메뉴 =========================================
            static void Start()
            {
                Console.WriteLine("===========================================================");
                Console.WriteLine("\n태초 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식");
                Console.WriteLine("\n===========================================================");
                Console.WriteLine("\n원하시는 행동을 입력해주세요");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Status();
                        break;
                    case "2":
                        Console.Clear();
                        Inventory();
                        break;
                    case "3":
                        Console.Clear();
                        Shop();
                        break;
                    case "4":
                        Console.Clear();
                        Rest();
                        break;
                    default:
                        Console.Clear();
                        Start();
                        break;
                }
            }

            // ======================================= 상태 보기 =========================================
            private static void Status()
            {
                Console.WriteLine("===========================================================");
                Console.WriteLine($"\nLv. {player.Level}");
                Console.WriteLine($"Class. {player.Job}");

                // 장비 스탯 추가
                int plusAtk = GetSumAtk();
                int plusDef = GetSumDef();
                Console.WriteLine($"공격력 : {player.Atk + plusAtk} (+{plusAtk})");
                Console.WriteLine($"방어력 : {player.Def + plusDef} (+{plusDef})");

                Console.WriteLine($"체력 : {player.Hp}");
                Console.WriteLine($"Gold : {player.Gold}");
                Console.WriteLine("\n===========================================================");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Start();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Status();
                        break;
                }
            }

            // 공격력 추가    
            private static int GetSumAtk()
            {
                int sum = 0;

                // 전체 아이템 확인
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (items[i].IsEquip) sum += items[i].Atk; // 장착 상태인 아이템의 Atk 합산
                }
                return sum;
            }

            // 방어력 추가
            private static int GetSumDef()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (items[i].IsEquip) sum += items[i].Def;
                }
                return sum;
            }

            // ======================================= 인벤토리 =========================================
            private static void Inventory()
            {
                Console.WriteLine("===========================================================");
                Console.WriteLine("\n[아이템 목록]");

                for (int i = 0; i < Item.ItemCount; i++) // 아이템의 가짓 수 만큼 출력
                {
                    // 보유 중인 아이템 활성화
                    if (items[i].IsBuy)
                    {
                        items[i].ShowDescription(true, i + 1, false); // 템 번호, 인덱스, 가격 순
                    }
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Start();
                        break;
                    case "1":
                        Console.Clear();
                        Equip();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        Inventory();
                        break;
                }
            }

            // 장착 관리
            private static void Equip()
            {
                Console.Clear();
                Console.WriteLine("[아이템 목록]\n");

                for (int i = 0; i < Item.ItemCount; i++)
                {
                    // 구매한 아이템이 있을 경우
                    if (items[i].IsBuy)
                    {
                        items[i].ShowDescription(true, i + 1, false); // 템 번호, 인덱스, 가격 순
                    }
                    else
                    {
                        Console.WriteLine("-");
                    }
                }

                Console.WriteLine("\n0. 나가기");

                string choice = Console.ReadLine();
                int choiceNum = int.Parse(choice);

                switch (choiceNum)
                {
                    case 0:
                        Console.Clear();
                        Start();
                        break;
                    default:
                        EquipToggle(choiceNum - 1); // 인덱스와 유저 입력 맞추기
                        Equip();
                        break;
                }
            }

            // 장비 장착 및 해제
            private static void EquipToggle(int idx)
            {
                // 타입 별로 하나의 아이템만 장착
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (i != idx && items[i].IsEquip && items[i].Type == items[idx].Type)
                    {
                        items[i].IsEquip = false;
                    }
                }
                // 새 아이템 장착 / 해제
                items[idx].IsEquip = !items[idx].IsEquip;
            }

            // ======================================= 상점 =========================================
            private static void Shop()
            {
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {player.Gold} G");
                Console.WriteLine("\n[장비 목록]\n");

                // 아이템 나열
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    items[i].ShowDescription(false, i + 1);
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("1. 장비 구매");
                Console.WriteLine("2. 장비 판매");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Start();
                        break;
                    case "1":
                        Console.Clear();
                        Buy();
                        break;
                    case "2":
                        Console.Clear();
                        Sell();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Shop();
                        break;
                }
            }

            // 아이템 구매
            private static void Buy()
            {
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {player.Gold} G");
                Console.WriteLine("\n[판매 목록]\n");

                for (int i = 0; i < Item.ItemCount; i++)
                {
                    items[i].ShowDescription(true, i + 1);
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("\n구매할 물품의 번호를 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Shop();
                        break;
                }

                int choiceNum = int.Parse(choice);
                choiceNum -= 1; // 인덱스와 유저 입력 맞추기
                Item selectedItem = items[choiceNum];

                if (selectedItem.IsBuy)
                {
                    Console.Clear();
                    Console.WriteLine("이미 구매한 물품입니다.\n");
                }
                else if (player.Gold >= selectedItem.Gold)
                {
                    Console.Clear();
                    selectedItem.IsBuy = true;
                    player.Gold -= selectedItem.Gold;
                    Console.WriteLine($"{selectedItem.Name} 구매완료\n");
                }
                else if (player.Gold <= selectedItem.Gold)
                {
                    Console.Clear();
                    Console.WriteLine("Gold가 부족합니다.\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                }
                Buy();
            }

            // 아이템 판매
            private static void Sell()
            {
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($" {player.Gold} G");
                Console.WriteLine("\n[보유 목록]\n");

                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (items[i].IsBuy)
                    {
                        items[i].ShowDescription(true, i + 1, false);
                    }
                    else
                    {
                        Console.WriteLine("-");
                    }
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("\n판매할 물품의 번호를 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Shop();
                        break;
                }

                float choiceNum = float.Parse(choice);
                choiceNum -= 1; // 인덱스와 유저 입력 맞추기
                Item selectedItem = items[(int)choiceNum];

                if (selectedItem.IsBuy)
                {
                    Console.Clear();
                    selectedItem.IsBuy = false;
                    player.Gold += (selectedItem.Gold * 0.85f);
                    Console.WriteLine($"{selectedItem.Name} 판매 완료");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                }
                Sell();
            }

            // ======================================= 휴식 =========================================
            private static void Rest()
            {
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
                Console.WriteLine("\n1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Start();
                        break;
                    case "1":
                        Recovery();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

            // 체력 회복
            private static void Recovery()
            {
                if (player.Hp >= 100)
                {
                    Console.Clear();
                    Console.WriteLine("이미 체력이 가득합니다.");
                    Rest();
                }
                else
                {
                    player.Hp = 100;
                    player.Gold -= 500;
                    Console.WriteLine("체력을 회복했습니다.");
                    Rest();
                }
            }
        }
    }
}
