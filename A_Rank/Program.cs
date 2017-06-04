using System;

namespace A_Rank
{
    class Program
    {
        const int N = 6;                                            // 硬貨の種類
        const int MAX = 1000;                                       // 使用可能な効果の枚数
        static int[] unit = new int[N]{ 1, 5, 10, 50, 100, 500 };   // 硬貨の単位
        static int[] result = new int[N];                           // 使用した硬貨の枚数

        static int MakeSum(int rest, int uidx)                      // uidx以下の硬貨で金額restを実現する組合せ
        {
            int i, cnt;
            if (rest == 0)
            {
            /*    for (i = 0; i < N; i++)
                {
                    if (result[i] > 0) Console.Write("{0:d}円 * {1:d} ", unit[i], result[i]);
                }
                Console.WriteLine();*/
                return 1;
            }
            else
            {
                cnt = 0;
                for (i = uidx; i >= 0; i--)
                {
                    if (rest >= unit[i] && result[i] < MAX)
                    {
                        result[i]++;                       // 使用した通貨i の枚数を1つ増やす
                        cnt += MakeSum(rest - unit[i], i); // 通貨iを使ったという条件での組み合わせの数を求める
                        result[i]--;                       // 通貨iの使用枚数を元に戻す
                    }
                }
                return cnt; // 解の個数を返す
            }
        }

        static void Main(string[] args)
        {
            string str;
            for (; (str = Console.ReadLine()) != null;)
            {
                int sum = int.Parse(str);                  // 硬貨の組み合わせで実現する金額
                int i;
                int cnt;
                for (i = 0; i < N; i++) result[i] = 0;     // 硬貨の枚数を初期化
                cnt = MakeSum(sum, N - 1);                  // 組合せの解の数
                Console.WriteLine(cnt);
            }
        }
    }
}
