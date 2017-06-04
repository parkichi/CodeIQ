using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 源泉徴収
{
    class Program
    {
        static void Main(string[] args)
        {
            double _料率 = 0.137;
            double _税率;
            const int MAX = 1000000;
            int[] array = new int[MAX];
            for (int _総支給額 = 0; _総支給額 < MAX; _総支給額++)
            {
                int _社会保険料 = (int)((_総支給額 / 1000) * 1000 * _料率 + 0.5);
                int _課税対象額 = _総支給額 - _社会保険料;
                if (_課税対象額 < 68000)
                {
                    _税率 = 0.0;
                }
                else if (_課税対象額 < 79000)
                {
                    _税率 = 0.02042;
                }
                else if (_課税対象額 < 252000)
                {
                    _税率 = 0.04084;
                }
                else
                {
                    _税率 = 0.06126;
                }
                int _所得税 = (int)(_課税対象額 * _税率);
                int _源泉徴収額 = _社会保険料 + _所得税;
                int _実支給額 = _総支給額 - _源泉徴収額;
                array[_実支給額] = _源泉徴収額;
            }

            string str;
            for (; (str = Console.ReadLine()) != null;)
            {
                int _予想支給額 = Int32.Parse(str);

                Console.WriteLine(array[_予想支給額]);
            }
        }
    }
}
