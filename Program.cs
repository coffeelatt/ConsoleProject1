namespace gamemaker5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameover = false;//반복문 종료조건 = 게임 종료조건
            Position playerpos;
            Position hidden;
            Position button;

            hidden.x = new Random().Next(1, 8);
            hidden.y = new Random().Next(1, 8);

            button.x = new Random().Next(1, 8);
            button.y = new Random().Next(1, 8);


            char[,] map;
            //게임 구성 단위요소
            // start 키입력 render update end  (맵랜더링,캐릭터랜더링) 타이틀 첫화면
            Tatle(); // 첫화면
            Start(out playerpos, out map, hidden);
            while (gameover == false)
            {
                Position minion;
                Random mx = new Random();
                Random my = new Random();
                minion.x = mx.Next(1, 8);
                minion.y = my.Next(1, 8);
                Render(playerpos, map, hidden, minion);
                ConsoleKey key = Key();
                Update(ref playerpos, key, map, ref hidden, ref gameover, ref minion, button);
            }
            End(); //종료되는 조건


        }
        struct Position //좌표
        {
            public int x; public int y;
        }
        static void Start(out Position playerpos, out char[,] map, Position hidden)
        //게임 초기 구동조건
        {
            Console.CursorVisible = false; //커서 깜빡임on/off
            playerpos.x = 1; playerpos.y = 1;//시작좌표

            map = new char[,] //맵 설정
            {//10,10작성
                {'□','#','#','#','#','#','#','#','#','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#','#','#','#','#','#','#','#','#','#'}

            };
        }
        static void End()
        //종료작업
        {
            Console.Clear();
            Console.WriteLine("CLEAR");
        }
        static ConsoleKey Key()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            return key;
        }
        static void Tatle()//초기 타이틀 화면
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("      레전드 Rpg       ");
            Console.WriteLine("-----------------------");
            Console.WriteLine("");
            Console.WriteLine("  아무키나 입력하세요  ");

            Console.ReadKey(true);
            Console.Clear();

        }
        static void Render(Position playerpos, char[,] map, Position hidden, Position minion)
        {
            //Console.Clear();  
            Console.SetCursorPosition(0, 0);

            MapRender(map);
            HiddenRender(hidden);
            CharRender(playerpos);
            MinionRender(minion);


        }
        static void MapRender(char[,] map)
        {
            //맵 출력
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);

                }
                Console.WriteLine();
            }


        }
        static void CharRender(Position playerpos)
        {
            //플레이어 위치로 커서 옮기기 
            Console.SetCursorPosition(playerpos.x, playerpos.y);
            //플레이어 출력
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('p');
            Console.ResetColor();  // 플레이어 잔상잡기 ??


        }
        static void HiddenRender(Position hidden)
        {
            Console.SetCursorPosition(hidden.x, hidden.y);




        }
        static void MinionRender(Position minion)
        {
            Console.SetCursorPosition(minion.x, minion.y);
            Console.WriteLine('M');
            

        }
        static void Hiddenopen(Position playerPos, ref Position hidden, ConsoleKey key)// 히든포인트 진입 이벤트
        {

            int two;

            if ((playerPos.x == hidden.x) && (playerPos.y == hidden.y))

            {

                while (true)// 일단 돌리고 브레이크로 나온다 굳굳
                {
                    Console.Clear();
                    Console.WriteLine("기이한 노인을 만나셨습니다.");
                    Console.WriteLine("1.대화한다. 2.공격한다 3.도망친다.");

                    string one = Console.ReadLine();
                    int.TryParse(one, out two);
                    if (two == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("이곳에 이방인이 찾아오다니..");
                        Console.WriteLine("이곳은 출구가 없어 나갈수가 없지");
                        Console.WriteLine("요근래 어딘가에 버튼이 있는걸 알아냈지만..");
                        Console.WriteLine("난 이미 늙어버렸지만 자네라면 찾을수 있을까..?");
                        Console.WriteLine("아무 키나 입력하세요(b).");
                        Console.ReadKey();
                    }
                    else if (two == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("노인이 죽었습니다.");
                        hidden.x = 0;
                        hidden.y = 0;
                        Console.WriteLine("아무 키나 입력하세요.");
                        Console.ReadKey();
                        break;
                    }
                    else if (two == 3)
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        static void Update(ref Position playerpos, ConsoleKey key, char[,] map, ref Position hidden, ref bool gameover, ref Position minion, Position button)
        {
            Move(ref playerpos, key, map);

            Hiddenopen(playerpos, ref hidden, key);

            Minion(playerpos, ref minion, key);

            bool su = Exit(playerpos, button, key);

            if (su)
            {
                gameover = true;
            }


            static void Move(ref Position playerpos, ConsoleKey key, char[,] map)
            {
                switch (key)
                { //이동
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (map[playerpos.y, playerpos.x + 1] == ' ')
                        { playerpos.x++; }
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (map[playerpos.y, playerpos.x - 1] == ' ')
                        { playerpos.x--; }
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (map[playerpos.y - 1, playerpos.x] == ' ')
                        { playerpos.y--; }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (map[playerpos.y + 1, playerpos.x] == ' ')
                        { playerpos.y++; }
                        break;
                }

            }
            static void Minion(Position playerpos, ref Position minion, ConsoleKey key)// 몬스터 진입 이벤트)
            {

                int two;

                if ((playerpos.x == minion.x) && (playerpos.y == minion.y))

                {

                    while (true)// 일단 돌리고 브레이크로 나온다 굳굳
                    {
                        Console.Clear();
                        Console.WriteLine("몬스터와 마주쳤습니다.");
                        Console.WriteLine("1.공격한다 2.도망친다.");

                        string one = Console.ReadLine();
                        int.TryParse(one, out two);
                        if (two == 1)
                        {
                            Console.Clear(); //전투 콘솔 생성후 추가보정. 
                            Console.WriteLine("이곳에 이방인이 찾아오다니..");
                            Console.WriteLine("이곳은 출구가 없어 나갈수가 없지");
                            Console.WriteLine("요근래 어딘가에 버튼이 있는걸 알아냈지만..");
                            Console.WriteLine("난 이미 늙어버렸지만 자네라면 찾을수 있을까..?");
                            Console.WriteLine("아무 키나 입력하세요(b).");
                            Console.ReadKey();
                        }
                        else if (two == 2)
                        {

                            Random a = new Random();
                            a.Next(1, 3);
                            int sixty = a.Next(1, 3);
                            if (sixty == 1 || sixty == 2)
                            {
                                Console.WriteLine("도망쳤습니다.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                        }
                        Console.WriteLine("도망치지 못하였습니다."); // 캐릭터화 후 추가보정 
                        Console.ReadKey();
                    }
                }


            }
            static bool Exit(Position playerpos, Position button, ConsoleKey key)
            //hidden 과 동일하지만 지점과 지점이 겹쳐서 되는것이 아닌 지점과 지점이 겹친후 b버튼을 누르면 작동.
            {
                Console.SetCursorPosition(button.x, button.y); // 좌표값 + 키 입력까지 맞아야 클리어.
                bool su = (playerpos.x == button.x) && (playerpos.y == button.y) && (key == ConsoleKey.B);
                {
                    return su;
                }
            }



        }
    }
}