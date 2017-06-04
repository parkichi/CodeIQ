/********************************************************************************************************** 
【問題】
M×Nのマス目を左上から右下に向かって移動します。移動方向は上下左右のみです。
ただし、マス目上には通れない箇所がいくつかあります。同じ点を2回以上通過しても構いません。

【入力】
標準入力から、1行目に半角スペースで区切られた2つの整数値M, N（1≦M, N≦64）が与えられます。
2行目から（M+1)行目までは長さNの文字列です。文字列は、' .'と'#'で構成されています。
'.'は移動できる部分、'#'は移動できない部分を表します。
2行目の1文字目と、（M+1）行目のN文字目、つまりスタート地点とゴール地点に相当する点は必ず'.'になっています。
また、スタートからゴールに到達する経路が必ず存在するものとします。
 
 
【出力】
マス目の左上から右下まで移動する経路の中で、方向転換する回数の最小値を、標準出力に出力してください。

【入出力サンプル】

Input
2 3
..#
...

Output
1

***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S_Rank
{
    class Program
    {
        static int turn;            // 曲がった回数
        static int min;             // 曲がった回数(最小)
        static int cnt;             // 見つけた経路の数
        static int direction;       // 方向　0:右　1:下　2:左　3:上
        static bool first;          // 初回移動flg

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main()
        {
            string str;
            for (; (str = Console.ReadLine()) != null;)
            {
                string[] args = str.Split(' ');
                int row = int.Parse(args[0]);
                int col = int.Parse(args[1]);
                Point[] path = new Point[row * col];
                for (int i = 0; i < row * col; i++)
                {
                    path[i].X = 0;
                    path[i].Y = 0;
                }
                string[] map = new string[row];
                for (int i = 0; i < row; i++)
                {
                    map[i] = Console.ReadLine();
                }

                Point start = new Point();
                Point goal = new Point();
                start.X = start.Y = 0;
                goal.X = col - 1;
                goal.Y = row - 1;

                min = row * col;
                turn = 0;
                cnt = 0;
                direction = 0;
                first = true;

                NextPos(0, start, goal, ref map, ref path);

                Console.WriteLine("");
                Console.WriteLine("経路の数　　: " + cnt);
                Console.WriteLine("最小の回数　: " + min);
                //Console.WriteLine(min);
            }
        }

        public static bool PathSerach(Point[] path, Point p, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (path[i].X == p.X && path[i].Y == p.Y)
                {
                    return true;
                } 
            }
            return false;
        }

        static void printPath(int len,ref Point[] path,int cnt,int turn)
        {
            Console.WriteLine();
            for (int i = 0; i <= len; i++)
            {
                Console.Write("({0:d}, {1:d}) ", path[i].X, path[i].Y);    // ゴールに到達
            }
            Console.WriteLine();
            Console.WriteLine("経路の数　　: " + cnt);
            Console.WriteLine("曲がった回数: " + turn);

        }
        static int NextPos(int len,Point node,Point goal, ref string[] map, ref Point[] path)
        {
            path[len].X = node.X;
            path[len].Y = node.Y;
            if ((node.X == goal.X) && (node.Y == goal.Y))
            {
                cnt++;
                if (min > turn) min = turn;

                printPath(len, ref path, cnt, turn);
            }
            else
            {
                // 右方向の探索
                if ((node.X < (map[0].Length - 1)) && (map[node.Y][node.X + 1] != '#'))
                {
                    Point next = new Point();
                    next.X = node.X + 1;
                    next.Y = node.Y;
                    if (!PathSerach(path, next, len))
                    {
                        int tTurn = turn;
                        int tDirection = direction;
                        bool tFirst = first;

                        if (direction != 0)
                        {
                            direction = 0;
                            if (!first) turn++;
                        }
                        if (first) first = false;

                        Console.Write("({0:d}, {1:d}) ", next.X, next.Y);    // 次を探索

                        NextPos(len + 1, next, goal, ref map, ref path);

                        turn = tTurn;
                        direction = tDirection;
                        first = tFirst;
                    }
                }
                // 下方向の探索
                if ((node.Y < (map.Length - 1)) && (map[node.Y + 1][node.X] != '#'))
                {
                    Point next = new Point();
                    next.X = node.X;
                    next.Y = node.Y + 1;
                    if (!PathSerach(path, next, len))
                    {
                        int tTurn = turn;
                        int tDirection = direction;
                        bool tFirst = first;

                        if (direction != 1)
                        {
                            direction = 1;
                            if (!first) turn++;
                        }
                        if (first) first = false;

                        Console.Write("({0:d}, {1:d}) ", next.X, next.Y);    // 次を探索

                        NextPos(len + 1, next, goal, ref map, ref path);

                        turn = tTurn;
                        direction = tDirection;
                        first = tFirst;
                    }
                }
                // 左方向の探索
                if ((node.X > 0) && (map[node.Y][node.X-1] != '#'))
                {
                    Point next = new Point();
                    next.X = node.X - 1;
                    next.Y = node.Y;
                    if (!PathSerach(path, next, len))
                    {
                        int tTurn = turn;
                        int tDirection = direction;
                        bool tFirst = first;

                        if (direction != 2)             // 左
                        {
                            direction = 2;              // 左
                            if (!first) turn++;
                        }
                        if (first) first = false;

                        Console.Write("({0:d}, {1:d}) ", next.X, next.Y);    // 次を探索

                        NextPos(len + 1, next, goal, ref map, ref path);

                        turn = tTurn;
                        direction = tDirection;
                        first = tFirst;
                    }
                }
                // 上方向の探索
                if ((node.Y > 0) && (map[node.Y-1][node.X] != '#'))
                {
                    Point next = new Point();
                    next.X = node.X;
                    next.Y = node.Y - 1;
                    if (!PathSerach(path, next, len))
                    {
                        int tTurn = turn;
                        int tDirection = direction;
                        bool tFirst = first;

                        if (direction != 3)             // 上
                        {
                            direction = 3;              // 上
                            if (!first) turn++;
                        }
                        if (first) first = false;

                        Console.Write("({0:d}, {1:d}) ", next.X, next.Y);    // 次を探索

                        NextPos(len + 1, next, goal, ref map, ref path);

                        turn = tTurn;
                        direction = tDirection;
                        first = tFirst;
                    }
                }
                Console.ReadLine();

            }
            return min;
        }

    }
}
/*
 
6 8
......#.
#.#####.
........
..#..#..
..#..###
..#.....

10 15
..##.....#.....
..........##...
.##...##..#....
#....#.......#.
.......#.......
.....#.#..#....
.....#.#..#....
.#............#
...#.##....##..
..........#....

30 20
.##.##..#..#.#......
....#..#.#.#...#...#
###....#....#...#.#.
##.........#..#..#..
..##.#.##...#..#....
.###.....##.#.###...
...........#..#.###.
.#.##...#...#...#...
.#.##..#.#.#.....##.
...#...#..#...#..#..
..###...###..#..#..#
.#..#.#...###.#.#...
#...#..#....#..##.#.
.#...#.#..##.##....#
.#...#.#........#.##
............#.#..#..
...#.#...#...#.....#
....##....#..##.###.
.#..#..###.#..##....
.#.##...###...#.....
.#..#..#.#........#.
#....##.###.###.#..#
#....#.#.##.#.#.##..
.......#.##..#......
........##.....#####
..####..#..#.####...
#.#.#...###...###.##
##..###.#.##...##.##
..#.#....#..##......
..##..#..#..##...#..

*/
