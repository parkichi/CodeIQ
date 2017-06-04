using System;

class Program
{

    static void Main()
    {
        string str;
        for (; (str = Console.ReadLine()) != null;)
        {
            int n = int.Parse(str);
            str = Console.ReadLine();
            string[] strs = str.Split(' ');
            bool judge = false;

            for (int i = 0; i <  n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if ((int.Parse(strs[i]) + int.Parse(strs[j])) == 256)
                    {
                        judge = true;
                        break;
                    }
                }
            }
            Console.WriteLine(judge ? "yes" : "no");
         }
    }
}
