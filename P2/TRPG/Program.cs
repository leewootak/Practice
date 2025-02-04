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
            public int Gold { get; set; }

            public Character(string job, int level, int atk, int def, int hp, int gold)
            {
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
            }
        }

        // 아이템 인터페이스
        public interface IItem
        {
            string Name { get; }
            void Use(Character warrior);
        }

        // 아이템 클래스
        public class Item
        {
            public string Name { get; }
            public string Description { get; } // 설명
            public int Type { get; } // 무기 or 방어구
            public int Atk { get; }
            public int Def { get; }
            public int Gold { get; set; }
            public bool IsEquip { get; set; }
            public bool IsBuy { get; set; }
            public static int ItemCount = 0;

            public Item(string name, string description, int type, int atk, int def, int gold, bool isEquip = false)
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
                if (itemNum) // 7-4 아이템 장착관리 번호 컬러
                {
                    Console.Write("{0}. ", idx);
                }
                if (IsEquip) // 아이템 착용 시
                {
                    Console.Write("[E]");
                }
                Console.Write(" | ");

                // 장비 장착 시 스탯 추가
                if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? " + " : "")}{Atk}");
                if (Def != 0) Console.Write($"Def {(Def >= 0 ? " + " : "")}{Def}");

                Console.Write(" | " + Description);

                // 품절 상태 확인
                if (price)
                {
                    string itemStatus = IsBuy ? "구매완료" : Gold + " G";
                    Console.WriteLine(" | " + itemStatus);
                }
                Console.WriteLine();
            }

            // ======================================= 실행 =========================================

            static Character _player;
            static Item[] _items;

            static void Main(string[] args)
            {
                DataSetting();
                Start();
            }

            // 사용할 데이터 세팅
            private static void DataSetting()
            {
                _player = new Character("전사", 1, 10, 5, 100, 5000);

                _items = new Item[6];

                AddItem(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 0, 9, 1000)); // 맨 앞의 숫자가 0이면 방어구, 1이면 무기
                AddItem(new Item("무쇠 갑옷", "무쇠로 만들어진 튼튼한 갑옷입니다.", 0, 0, 5, 2000));
                AddItem(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 0, 15, 3500));
                AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 600));
                AddItem(new Item("청동 도끼", "쉽게 볼 수 있는 낡은 검입니다.", 1, 5, 0, 1500));
                AddItem(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, 7, 0, 3000));
            }

            // 아이템 추가
            static void AddItem(Item item)
            {
                if (Item.ItemCount == 6)
                    return;

                _items[Item.ItemCount] = item;
                Item.ItemCount++;
            }

            // ======================================= 시작 메뉴 =========================================
            static void Start()
            {
                Console.Clear();
                Console.WriteLine("===========================================================" +
                "\n\n태초 마을에 오신 여러분 환영합니다." +
                "\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다." +
                "\n\n1. 상태 보기" +
                "\n2. 인벤토리" +
                "\n3. 상점" +
                "\n\n===========================================================");

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
                Console.WriteLine($"\nLv. {_player.Level}");
                Console.WriteLine($"Class. {_player.Job}");

                // 장비 스탯 추가
                int plusAtk = GetSumAtk();
                int plusDef = GetSumDef();
                Console.WriteLine($"공격력 : {_player.Atk + plusAtk} (+{plusAtk})");
                Console.WriteLine($"방어력 : {_player.Def + plusDef} (+{plusDef})");

                Console.WriteLine($"체력 : {_player.Hp}");
                Console.WriteLine($"Gold : {_player.Gold}");
                Console.WriteLine("\n===========================================================");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Start();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Status();
                        break;

                }
            }

            // 추가 공격력    
            private static int GetSumAtk()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCount; i++)   // 아이템 전부 확인
                {
                    if (_items[i].IsEquip) sum += _items[i].Atk; // 장착 상태인 아이템의 Atk 합산
                }
                return sum; // sum에 값 반환
            }

            // 추가 방어력
            private static int GetSumDef()
            {
                int sum = 0;
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (_items[i].IsEquip) sum += _items[i].Def;
                }
                return sum;
            }

            // ======================================= 인벤토리 =========================================
            private static void Inventory()
            {
                Console.WriteLine("===========================================================" +
                        "\n\n[아이템 목록]");

                for (int i = 0; i < Item.ItemCount; i++) // 아이템의 Itemcnt 반복문, 아이템의 가짓 수 만큼 출력되어 보임
                {
                    if (_items[i].IsBuy) // 구매한 아이템 이라면 표시
                    {
                        _items[i].ShowDescription(true, i + 1, false);
                    }
                }

                Console.WriteLine(
                        "\n\n0. 나가기" +
                        "\n1. 장착 관리" +
                        "\n\n원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
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
                Console.WriteLine("[아이템 목록]\n\n");

                for (int i = 0; i < Item.ItemCount; i++)
                {
                    if (_items[i].IsBuy) // 구매한 아이템이 있을 경우 표시
                    {
                        _items[i].ShowDescription(true, i + 1, false); // 템 숫자, 인덱스, 가격 순
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
                        EquipToggle(choiceNum - 1); // 장비 인덱스와 유저 입력 맞추기
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
                    if (i != idx && _items[i].IsEquip && _items[i].Type == _items[idx].Type)
                    {
                        _items[i].IsEquip = false; // 기존 아이템 해제
                    }
                }
                // 새 아이템 장착 / 해제
                _items[idx].IsEquip = !_items[idx].IsEquip;

            }


            // ======================================= 상점 =========================================
            private static void Shop()
            {
                Console.WriteLine("\n[보유 골드]" +
                    $"\n {_player.Gold} G" +
                    "\n\n[아이템 목록]\n");

                // 아이템 나열
                for (int i = 0; i < Item.ItemCount; i++)
                {
                    _items[i].ShowDescription(false, i + 1);
                }

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Start();
                        break;
                    case "1":
                        Console.Clear();
                        Buy();
                        break;
                    default:
                        Console.Clear();
                        Shop();
                        break;
                }
            }

            // 아이템 구매
            private static void Buy()
            {
                Console.WriteLine("\n[보유 골드]" +
                    $"\n {_player.Gold} G" +
                    "\n\n[판매 목록]\n");

                for (int i = 0; i < Item.ItemCount; i++)
                {
                    _items[i].ShowDescription(true, i + 1);
                }
                Console.WriteLine("\n0. 나가기" +
                    "\n\n원하는 항목을 선택해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Start();
                        break;
                }

                int choiceNum = int.Parse(choice);
                choiceNum -= 1; // 인덱스는 0부터 시작하므로 사용자 입력에서 1빼기
                Item selectedItem = _items[choiceNum];

                if (selectedItem.IsBuy)
                {
                    Console.Clear();
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (_player.Gold >= selectedItem.Gold)
                {
                    Console.Clear();
                    selectedItem.IsBuy = true; // 구매 표시 활성화
                    _player.Gold -= selectedItem.Gold; // 골드 감소
                    Console.WriteLine($"{selectedItem.Name} 구매완료");
                }
                else if (_player.Gold <= selectedItem.Gold) // 골드가 부족한 경우
                {
                    Console.Clear();
                    Console.WriteLine("\nGold가 부족합니다.");
                }
                else // 그 외 다른 것 입력
                {
                    Console.Clear();
                    Console.WriteLine("\n잘못된 입력입니다.");
                }
                Buy();
            }
        }
    }
}
