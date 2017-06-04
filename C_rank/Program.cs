using System;

class Program
{

    static void Main()
    {
        string str;
        bool rep = false;
        for (; (str=Console.ReadLine())!= null;)
        {
            int i = 0;
            do
            {
                if (str.Length < 2) break;
                if (rep)
                {
                    i = 0;
                    rep = false;
                }

                if (Math.Abs(str[i] - str[i + 1]) == 1)
                {
                    str = str.Remove(i, 2);
                    //Console.WriteLine(i + " " + (str.Length) + " " + str);
                    rep = true;
                    i = 0;
                }
                else
                {
                    i++;
                }
            } while (i < (str.Length - 1));

            Console.WriteLine(str);
        }
    }
}
