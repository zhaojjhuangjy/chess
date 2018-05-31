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

        static void Main(string[] args)
        {
            GameShow();
            Console.WriteLine("请输入玩家A的姓名：");
            String playerA = Console.ReadLine();
            PlayerPos[0] = 0;
            Console.WriteLine("请输入玩家B的姓名：");
            String playerB = Console.ReadLine();
            PlayerPos[1] = 0;
            Maps = new int[100];
            InitMap();
            DrawMap();
            Console.ReadKey();
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
                if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
                {
                    Console.Write("<>");
                }
                else if (PlayerPos[0] != PlayerPos[1] && PlayerPos[0] == i)
                {
                    Console.Write("A");
                }
                else if (PlayerPos[0] != PlayerPos[1] && PlayerPos[1] == i)
                {
                    Console.Write("B");
                }
                else
                {
                    if (i < 30)
                    {
                        checkImages(Maps[i]);
                    }
                    else if (i >= 30 && i < 35)
                    {
                        Console.Write("".PadLeft(58));
                        checkImages(Maps[i]);
                        Console.WriteLine();
                    }
                    else if (i >= 35 && i < 65)
                    {
                        checkImages(Maps[i]);
                    }
                    else if (i >= 65 && i < 70)
                    {
                        checkImages(Maps[i]);
                        Console.WriteLine();
                    }
                    else
                    {
                        checkImages(Maps[i]);
                    }
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

        public static void checkImages(int key)
        {
            switch (key)
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
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("□");
                    break;
            }
        }
    }
}
