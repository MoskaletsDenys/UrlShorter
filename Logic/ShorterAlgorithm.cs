using System;
namespace UrlShorter.Logic
{
    public class ShorterAlgorithm
    {

        public static readonly string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
        public static readonly int Base = Alphabet.Length;

        public string Encode(int id)
        {
            if (id == 0) return Alphabet[0].ToString();
            string result = "";
            while (id > 0)
            {
                result += Alphabet[id % Base];
                id /= Base;
            }
            return  result;
        }
        public string Encode()
        {
            string res = String.Empty;
            int count = 6;
            Random rd = new Random();
            while(count>0)
            {
                res += Alphabet[rd.Next(Base)];
                count--;
            }
            return res;
        }
    }
}
