using System;
using System.Collections.Generic;
using System.Linq;//Union

namespace Lab2Reshuffles
{
    class Program
    {
        static void Main(string[] args)
        {
            string Alph = "0123456789ABCDEF";
            string InpStr = "";
            bool isCorrectString = false;

            Console.WriteLine("Введите шифруемую строку алфавит {0-9,A-F}:");
            while (!isCorrectString)
            {
                InpStr = Console.ReadLine();
                isCorrectString = isInAlph(InpStr, Alph);
                if (isCorrectString == false)
                    Console.WriteLine("Ошибка: Введите строку в алфавите {0-9,A-F}:");
            }
            int[] SwapMap = genSwapMap(4);
           /* foreach (int i in SwapMap) {
                Console.Write(i.ToString() + "\t");
            }*/
            string EncodedStr = Encode(InpStr, SwapMap);
            Console.WriteLine("Закодированная строка: " + EncodedStr);
            string DecodedStr = Decode(EncodedStr, SwapMap);
            Console.WriteLine("Декодированная строка: " + DecodedStr);
            Console.ReadKey();

        }
        static string getAlph(string str)
        {
            string res = "";
            char[] Alph = str.Union(str).ToArray();
            Alph = Alph.OrderBy(c => c).ToArray();
            res = new string(Alph);
            return res;
        }
        static bool isInAlph(string str, string Alph)
        {
            foreach (char ch in str)
                if (!Alph.Contains(ch))
                    return false;
            return true;
        }
        static int[] genSwapMap(int randSeed = 5)
        {
            int[] ResultMap = new int[16];
            List<int> BasicSequence = new List<int>();
            for (int i = 0; i < 16; i++)
                BasicSequence.Add(i);
            Random Randomizer = new Random(randSeed);
            for (int i = 0; i < 16; i++)
            {
                int randIndex = Randomizer.Next(0, BasicSequence.Count);
                ResultMap[i] = BasicSequence[randIndex];
                BasicSequence.RemoveAt(randIndex);
            }
            return ResultMap;
        }

        static string Encode(string InpStr, int[] SwapMap)
        {
            string ResStr = "";
            string EnsStr = InpStr;
            //Дополняем нулями
            while (EnsStr.Length % 16 != 0)
            {
                EnsStr = EnsStr + "0";
            }
            //Поблочно заменяем символы
            while (EnsStr.Length > 0)
            {
                string sBlock = EnsStr.Substring(0, 16);
                EnsStr = EnsStr.Remove(0, 16);
                ResStr += SwapBlock(sBlock, SwapMap);
            }
            return ResStr;
        }
        private static string SwapBlock(string sBlock, int[] SwapMap)
        {
            char[] chBlock = new char[16];
            for (int i = 0; i < sBlock.Length; i++)
                chBlock[i] = sBlock[SwapMap[i]];
            return new string(chBlock);
        }
        static string Decode(string InpStr, int[] SwapMap)
        {
            string ResStr = "";
            string EnsStr = InpStr;
            //Получение обратного словаря:
            int[] NewSwapMap = new int[16];
            for (int i = 0; i < 16; i++)
                NewSwapMap[SwapMap[i]] = i;

            //Декодирование
            while (EnsStr.Length > 0)
            {
                string sBlock = EnsStr.Substring(0, 16);
                EnsStr = EnsStr.Remove(0, 16);
                ResStr += SwapBlock(sBlock, NewSwapMap);
            }
            return ResStr;
        }
    }
}
