using System;

class Program
{

    static void Main()
    {
        string str;
        for (; (str = Console.ReadLine()) != null;)
        {
            string[] para = str.Split(' ');

            int N = int.Parse(para[0]);
            int M = int.Parse(para[1]);
            int Ans = 0;
            int cnt = 0;
            int mask = 1;
            int k;
            for (int i = 0; i<=N; i++)
            {
                mask = 1;
                cnt = 0;
                for (int j = 0; j <17; j++, mask = mask << 1)
                {
                    if ((i & mask) != 0)
                    {
                        cnt++;
                    }
                    //Console.WriteLine(i + " && " + mask + " : " + cnt);
                }
                if (cnt == M)
                {
                    Ans++;
                }
            }
            Console.WriteLine(Ans);
        }
    }
}