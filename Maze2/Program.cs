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

// 幅優先探索(再帰なし) breadth-first search (No Recursive)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Maze2
{
    class Program
    {

        public struct Point
        {
            public int X { get; set; }  // 行
            public int Y { get; set; }  // 列
            public int R { get; set; }  // 方向　0:なし　1:右　2:下　3:左　4:上
        }

        static void Main()
        {
            string str;
            for (; (str = Console.ReadLine()) != null;)
            {
                string[] args = str.Split(' ');
                int x = int.Parse(args[0]);
                int y = int.Parse(args[1]);

                var sw = Stopwatch.StartNew();
                MazeSolverBFS solver = new MazeSolverBFS();
                Maze ans = solver.Solve(new Maze(x, y));
                sw.Stop();

                Console.WriteLine("");
                Console.WriteLine("{0}ミリ秒", sw.ElapsedMilliseconds);
                Console.WriteLine("経路の数　　: " + solver.cntGoal);
                Console.WriteLine("最小の回数　: " + solver.minBent);
                //Console.WriteLine(solver.minBent);
            }
        }
        // 幅優先探索(再帰なし) breadth-first search (No Recursive)
        public class MazeSolverBFS
        {

            public int minBent;                         // 曲がった回数(最小)
            public int cntGoal;                         // 見つけた経路の数

            public Maze Solve(Maze maze)
            {
                Queue<Maze> queu = new Queue<Maze>();
                queu.Enqueue(maze.Clone());
                Maze current = maze;

                minBent = maze.xSize * maze.ySize;
                cntGoal = 0;

                while (queu.Count != 0)
                {
                    //キューの先頭からノード currentNode を取り出す
                    current = queu.Dequeue();
                    if (current.IsGoal())
                    {
                        // ゴールを発見
                        cntGoal++;                                      // 探索の回数
                        if (minBent > current.cntBent) minBent = current.cntBent;       // 曲がった回数の最小値
                        current.Print();
                    }
                    else
                    {
                        foreach (var pos in current.NextPositions())
                        {
                            Maze next = current.Clone();
                            next.Footmark(pos);
                            if (current.currPos.R != 0)
                            {
                                if (current.currPos.R != next.currPos.R)
                                {
                                    next.cntBent++;
                                }
                            }
                            if (next.cntBent < minBent) queu.Enqueue(next);
                        }
                    }
                }
                return current;
            }
        }

        // 迷路クラス
        public class Maze
        {
            private char[,] _map;
            private int _xSize;
            private int _ySize;
            private int _cntBent;       // 曲がった回数

            // コンストラクタ
            private Maze()
            {
            }

            // コンストラクタ
            public Maze(int xSize, int ySize)
            {
                ReadMapConsole(xSize);
                _cntBent = 0;
            }

            public int xSize { get { return _xSize; } }
            public int ySize { get { return _ySize; } }
            public Point currPos { get { return _currPos; } }
            public int cntBent
            {
                set { _cntBent = value; }
                get { return _cntBent; }
            }

            // Mapデータをコンソールから読み込む
            private void ReadMapConsole(int lines)
            {
                string[] map = new string[lines];
                for (int i = 0; i < lines; i++)
                {
                    map[i] = Console.ReadLine();
                }
                this._map = new char[map.Length, map[0].Length];
                for (int x = 0; x < map.Length; x++)
                {
                    for (int y = 0; y < map[0].Length; y++)
                    {
                        this._map[x, y] = map[x][y];
                    }
                }
                _xSize = map.Length;
                _ySize = map[0].Length;
                _currPos.X = 0;
                _currPos.Y = 0;
                PrintMark(_currPos, 'S');
                _goalPos.X = _xSize - 1;
                _goalPos.Y = _ySize - 1;
                PrintMark(_goalPos, 'G');
            }

            // 現在の位置
            private Point _currPos;

            // ゴールの位置
            private Point _goalPos;

            // 複製を作成する
            public Maze Clone()
            {
                Maze maze = new Maze();
                maze._map = (char[,])_map.Clone();
                maze._xSize = this._xSize;
                maze._ySize = this._ySize;
                maze._currPos = this._currPos;
                maze._goalPos = this._goalPos;
                maze._cntBent = this._cntBent;
                return maze;
            }

            // posの四方にある道の位置を列挙する
            public IEnumerable<Point> AroundPositions(Point pos)
            {
                if (pos.Y + 1 < _ySize)
                    yield return new Point { X = pos.X, Y = pos.Y + 1, R = 1 };
                if (pos.X + 1 < _xSize)
                    yield return new Point { X = pos.X + 1, Y = pos.Y, R = 2 };
                if (pos.Y > 0)
                    yield return new Point { X = pos.X, Y = pos.Y - 1, R = 3 };
                if (pos.X > 0)
                    yield return new Point { X = pos.X - 1, Y = pos.Y, R = 4 };
            }

            // 次の一歩先がゴールかどうかを調べる。
            public bool IsGoal()
            {
                if (_map[_currPos.X, _currPos.Y] == 'G')
                {
                    return true;
                }
                else
                {

                    IEnumerable<Point> nextPos = AroundPositions(_currPos).Where(p => p.X == _goalPos.X && p.Y == _goalPos.Y);
                    if (nextPos.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        foreach (var pos in nextPos)
                        {
                            if (_currPos.R != 0 && pos.R != _currPos.R)
                                _cntBent++;
                        }
                        return true;
                    }
                }
            }

            // posで指定した位置にフットマークする
            public void Footmark(Point pos)
            {
                _currPos = pos;
                PrintMark(pos, '*');
            }

            // posで指定した位置にマークする
            public void PrintMark(Point pos, char mark)
            {
                _map[pos.X, pos.Y] = mark;
            }

            // posで指定した位置のマークを取り去る
            public void GoBack(Point pos)
            {
                _currPos = pos;
                _map[pos.X, pos.Y] = '.';
            }

            // target で指定した文字がある場所を求める。
            public Point FindPosition(char target)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    for (int y = 0; y < _ySize; y++)
                    {
                        if (_map[x, y] == target)
                            return new Point { X = x, Y = y };
                    }
                }
                throw new ApplicationException();
            }

            // 次に移動できる位置を列挙する
            public IEnumerable<Point> NextPositions()
            {
                return AroundPositions(_currPos).Where(p => _map[p.X, p.Y] == '.');
            }

            // 迷路をConsoleに表示する
            public void Print()
            {
                Console.WriteLine();
                for (int x = 0; x < _xSize; x++)
                {
                    for (int y = 0; y < _ySize; y++)
                    {
                        Console.Write(_map[x, y]);
                    }
                    Console.WriteLine();
                }
            }
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

10 20
.##.##..#..#.#......
....#..#.#.#...#...#
###....#....#...#.#.
##.........#..#..#..
..##.#.##...#..#....
.###.....##.#.###...
...........#..#.###.
.#.##...#...#...#...
.#.##..#.#.#......#.
...#...#......#..#..

15 20
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
.#...#.#........#...

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
