using System;
using System.Runtime.ConstrainedExecution;

namespace vaja3
{
    class Vaja3
    {
        static List<int> kmpNext(string y)
        {
            List<int> kmpNext = new List<int>(y.Length);
            kmpNext.Add(-1);
            kmpNext.Add(0);
            int j = 0;
            for (int i = 2; i < y.Length; i++)
            {
                while (j >= 0 && y[j] != y[i - 1])
                {
                    j = kmpNext[j];
                }
                j++;
                kmpNext.Add(j);
            }
            return kmpNext;
        }
        static void KMP(string x, string y)
        {
            int m = y.Length;
            int n = x.Length;
            var kmp = kmpNext(y);
            List<int> matches = new List<int>();
            int j = 0;

            for (int i = 0; i < n;)
            {
                if (x[i] == y[j])
                {
                    j++;
                    i++;
                }
                else
                {

                    i = i + (j - kmp[j]);
                    j = 0;
                }

                if (j == m)
                {
                    matches.Add(i - j);
                    j = 0;
                }

                if (i + m > n)
                {
                    break;
                }
            }

            foreach (int index in matches)
            {
                Console.WriteLine("Match found at index {0}", index);
            }

            File.WriteAllText("out.txt", string.Join(" ", matches));
        }


        public static List<int> BCH(string y)
        {
            List<int> BCH = Enumerable.Repeat(y.Length + 1, 256).ToList();
            for (int i = 0; i < y.Length; i++)
            {
                BCH[y[i]] = y.Length - i;
            }

            return BCH;
        }
        static void Sunday(string x, string y)
        {
            int m = y.Length;
            int n = x.Length;
            var bhc = BCH(y);
            List<int> matches = new List<int>();
            int j = 0;

            for (int i = 0; i < n;)
            {
                if (x[i] == y[j])
                {
                    j++;
                    i++;
                }
                else
                {
                    i = i + bhc[x[i + m]];
                    j = 0;
                }

                if (j == m)
                {
                    matches.Add(i - j);
                    j = 0;
                }

                if (i + m > n)
                {
                    break;
                }
            }

            foreach (int index in matches)
            {
                Console.WriteLine("Match found at index {0}", index);
            }

            File.WriteAllText("out.txt", string.Join(" ", matches));
        }

        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Mankajoči parametri");
                return;
            }
            string text = File.ReadAllText(args[2]);
            string search = args[1];

            switch (args[0])
            {
                case ("0"):
                    KMP(text, search);
                    break;

                case ("1"):
                    Sunday(text, search);
                    break;

                default:
                    Console.WriteLine("Napačne izbire sortiranja");
                    break;
            }
        }
    }
}
