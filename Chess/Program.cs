using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        private static int[] Maps = null;
        private static int[] PlayerPos = new int[2];
        private static String[] PlayerName = new String[2];
        private static Boolean[] PlayerPause = new Boolean[2];

        static void Main(string[] args)
        {
            GameShow();
            Console.WriteLine("请输入玩家A的姓名：");
            PlayerName[0] = Console.ReadLine();
            while(PlayerName[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入");
                PlayerName[0] = Console.ReadLine();
            }
            PlayerPos[0] = 0;
            Console.WriteLine("请输入玩家B的姓名：");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == "" | PlayerName[1] == PlayerName[0])
            {
                if(PlayerName[1] == "")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入");
                    PlayerName[1] = Console.ReadLine();
                }
                if (PlayerName[1] == PlayerName[0])
                {
                    Console.WriteLine("玩家B的姓名不能与玩家A相同，请重新输入");
                    PlayerName[1] = Console.ReadLine();
                }
            }
            PlayerPos[1] = 0;
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用A表示，{1}的士兵为B表示。", PlayerName[0], PlayerName[1]);
            Maps = new int[100];
            InitMap();
            DrawMap();

            //游戏进行中
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (PlayerPause[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    PlayerPause[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家 {0} 赢得了对战，战胜了玩家 {1}", PlayerName[0], PlayerName[1]);
                }
                if (PlayerPause[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    PlayerPause[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家 {0} 赢得了对战，战胜了玩家 {1}", PlayerName[1], PlayerName[0]);
                }
            }

            Console.ReadKey();
        }

        private static void PlayGame(int playNum)
        {
            Console.WriteLine("玩家 {0} 按任意键开始掷骰子", PlayerName[playNum]);
            Console.ReadKey(true);
            Random r = new Random();
            int rNum = r.Next(1, 7);
            Console.WriteLine("玩家 {0} 掷出了 {1}", PlayerName[playNum], rNum);
            PlayerPos[playNum] += rNum;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("玩家 {0} 按任意键开始行动", PlayerName[playNum]);
            Console.ReadKey(true);
            Console.WriteLine("玩家 {0} 行动结束", PlayerName[playNum]);
            Console.ReadKey(true);
            //可能的情况
            if (PlayerPos[playNum] == PlayerPos[1 - playNum])
            {
                Console.WriteLine("玩家 {0} 踩到了玩家 {1}，玩家 {2} 退6格", PlayerName[playNum], PlayerName[1 - playNum], PlayerName[1 - playNum]);
                PlayerPos[1 - playNum] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else
            {
                switch (Maps[PlayerPos[playNum]])
                {
                    case 0:
                        Console.WriteLine("玩家 {0} 踩到了 方块，安全。", PlayerName[playNum]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家 {0} 踩到了 幸运轮盘，请选择：1、交换位置，2、轰炸对方", PlayerName[playNum]);
                        String input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家 {0} 选择与玩家 {1} 交换位置", PlayerName[playNum], PlayerName[1 - playNum]);
                                Console.ReadKey(true);
                                int tempint = PlayerPos[playNum];
                                PlayerPos[playNum] = PlayerPos[1 - playNum];
                                PlayerPos[1 - playNum] = tempint;
                                Console.WriteLine("交换完成！按任意键继续！");
                                ChangePos();
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家 {0} 选择轰炸玩家 {1},玩家 {2} 退6格", PlayerName[playNum], PlayerName[1 - playNum], PlayerName[1 - playNum]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playNum] -= 6;
                                Console.WriteLine("玩家 {0} 退了6格", PlayerName[1 - playNum]);
                                ChangePos();
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2：1、交换位置，2、轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("玩家 {0} 踩到地雷，退6格", PlayerName[playNum]);
                        PlayerPos[playNum] -= 6;
                        ChangePos();
                        Console.WriteLine("玩家 {0} 退了6格", PlayerName[playNum]);
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("玩家 {0} 踩到暂停，暂停一回合", PlayerName[playNum]);
                        PlayerPause[playNum] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家 {0} 踩到时空隧道，进10格", PlayerName[playNum]);
                        PlayerPos[playNum] += 10;
                        ChangePos();
                        Console.WriteLine("玩家 {0} 进了10格", PlayerName[playNum]);
                        Console.ReadKey(true);
                        break;
                }
            }
            ChangePos();
            Console.Clear();
            DrawMap();
        }

        private static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] > 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] > 99)
            {
                PlayerPos[1] = 99;
            }
        }

        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("****************飞行棋游戏*****************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*******************************************");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void InitMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//◎
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//☆
            int[] pause = { 9, 27, 60, 93 };//▲
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//卐
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel.Length] = 4;
            }
        }

        public static void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘◎ 地雷☆ 暂停▲ 时空隧道卐");
            //□
            for (int i = 0; i < Maps.Length; i++)
            {
                
                if (i < 30)
                {
                    checkImages(i);
                }
                else if (i >= 30 && i < 35)
                {
                    Console.Write("".PadLeft(58));
                    checkImages(i);
                    Console.WriteLine();
                }
                else if (i >= 35 && i < 65)
                {
                    checkImages(35 + 64 - i);
                }
                else if (i >= 65 && i < 70)
                {
                    checkImages(i);
                    Console.WriteLine();
                }
                else
                {
                    checkImages(i);
                }

                if (i == 29)
                {
                    Console.WriteLine();
                }
                if (i == 64)
                {
                    Console.WriteLine();
                }
                if (i == Maps.Length - 1)
                {
                    Console.WriteLine();
                }
            }
        }

        public static void checkImages(int index)
        {
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == index)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("<>");
            }
            else if (PlayerPos[0] != PlayerPos[1] && PlayerPos[0] == index)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("A");
            }
            else if (PlayerPos[0] != PlayerPos[1] && PlayerPos[1] == index)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("B");
            }
            else
            {
                switch (Maps[index])
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("◎");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("☆");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("▲");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("卐");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("□");
                        break;
                }
            }
        }
    }
}
