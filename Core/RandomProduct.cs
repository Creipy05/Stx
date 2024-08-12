using Stx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class RandomProduct
    {
        private static readonly char[] BaseChars =
         "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly Dictionary<char, int> CharValues = BaseChars
                   .Select((c, i) => new { Char = c, Index = i })
                   .ToDictionary(c => c.Char, c => c.Index);

        private static int FirstWordLength = 6;
        private static int MinProductNameValue = (int)BaseToLong("".PadLeft(FirstWordLength, 'a'));
        private static int MaxProductNameValue = (int)BaseToLong("".PadLeft(FirstWordLength, 'z'));


        

        public static Product GenerateProduct(int productNumber, int lastProductNumber)
        {
            var rnd = new Random(productNumber);
            var name = RandomProduct.NumberToProductName(productNumber, 1, lastProductNumber);
            var p = new Product()
            {
                //Id = Guid.NewGuid(),
                
                Code = "C" + productNumber.ToString().PadLeft(7,'0'),
                Active = rnd.Next(2) == 0,
                Description = "",//RandomProduct.RandomSentences(rnd.Next(5, 10), rnd.Next(3, 10), rnd),
                
                Name = name + " (" + productNumber + ")",
                Rating = rnd.Next(1, 5),
                
                ReleaseDate = DateTime.SpecifyKind(new DateTime(2000, 01, 01), DateTimeKind.Utc).AddMilliseconds(productNumber * 30).AddHours(productNumber),
                Type = productNumber % 10 == 3? ProductType.Service: ProductType.Product,
            };
            
            if (productNumber % 7 == 6)
            {
                p.Name = null;
                p.Rating = null;
            }
            var actualprice = rnd.Next(5000) + 100 + ((productNumber % 10) == 2 ? 0 : 0.1);
            for (int i = 0;i<1000;i++)
            {
                var ph = new ProductHistory()
                {
                    ValidFrom = (p.ReleaseDate ?? new DateTime(2000, 01, 01)).AddDays(i),
                    Code = p.Code,
                    Active = p.Active,
                    Name = p.Name,
                    Rating = p.Rating,
                    ReleaseDate = p.ReleaseDate,
                    Type = p.Type,
                    Description = p.Description,
                    ProductId = p.Id,
                    Price = actualprice,
                };
                actualprice = actualprice * ((rnd.Next(3) - 1) *0.01 + 1);
                actualprice = Math.Round(actualprice, 2);
                p.History.Add(ph);
            }
            p.Price = actualprice;
            

            return p;

        }
        public static string? NumberToProductName(int productNumber, int firstProductNumber, int lastProductNumber)
        {
            var rnd = new Random(productNumber);
            var productNameValuePercent = (double)(productNumber - firstProductNumber) 
                / (lastProductNumber - firstProductNumber);
            var productNameValue = (int)(productNameValuePercent
                * (MaxProductNameValue - MinProductNameValue)
                + MinProductNameValue);
            try
            {
                var firstWord = LongToBase(productNameValue).PadLeft(FirstWordLength, 'a');
                return FirstCharToUpper(firstWord) + " " + RandomWord(rnd.Next(5) + 3, rnd).ToLower() + " " + RandomWord(rnd.Next(5) + 3, rnd).ToLower();
            } catch
            {
                return null;
            }
        } 
        public static double ProductNameToNumber(string productName, int firstProductNumber, int lastProductNumber)
        {
            

            var productNameIndexOfSpace = productName.IndexOf(" ");
            var firstWord = productNameIndexOfSpace > -1
                ? productName.Substring(0, productNameIndexOfSpace)
                : productName;
            try
            {
                var productNameValue = BaseToLong(firstWord.ToLower());
                var productNameValuePercent = (double)(productNameValue - MinProductNameValue) / (MaxProductNameValue - MinProductNameValue);
                var productNumber = productNameValuePercent * (lastProductNumber - firstProductNumber) + firstProductNumber;
                //return (int)Math.Round(productNumber);
                return productNumber;
            }
            catch
            {
                return 0;
            }
        }
        


        private static char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static char[] numbers = "0123456789".ToCharArray();
        public static string RandomWord(int count, Random rnd)
        {
            string res = "";
            for (int i = 0; i < count; i++)
            {
                res += letters[rnd.Next(0, letters.Length - 1)];
            }
            return res;
        }
        public static string RandomSentence(int count, Random rnd)
        {
            string res = RandomWord(1, rnd).ToUpper();
            for (int i = 0; i < count; i++)
            {
                res += RandomWord(rnd.Next(4) * rnd.Next(4) + rnd.Next(3) + 1, rnd).ToLower();
                if (i != 0)
                {
                    if (i == count - 1)
                    {
                        res += ".";
                    }
                    else
                    {
                        res += " ";
                    }

                }
            }
            return res;
        }
        public static string RandomSentences(int count, int Wordcount, Random rnd)
        {
            string res = "";
            for (int i = 0; i < count; i++)
            {
                res += RandomSentence(Wordcount, rnd);
                if (i != count - 1)
                {
                    res += " ";
                }

            }
            return res;
        }
        public static string RandomNumber(int count, Random rnd)
        {
            string res = "";
            for (int i = 0; i < count; i++)
            {
                res += numbers[rnd.Next(0, numbers.Length - 1)];
            }
            return res;
        }



        public static string LongToBase(long value)
        {
            long targetBase = BaseChars.Length;
            // Determine exact number of characters to use.
            char[] buffer = new char[Math.Max(
                       (int)Math.Ceiling(Math.Log(value + 1, targetBase)), 1)];

            var i = buffer.Length;
            do
            {
                buffer[--i] = BaseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            return new string(buffer, i, buffer.Length - i);
        }

        public static long BaseToLong(string number)
        {
            char[] chrs = number.ToCharArray();
            int m = chrs.Length - 1;
            int n = BaseChars.Length, x;
            long result = 0;
            for (int i = 0; i < chrs.Length; i++)
            {
                x = CharValues[chrs[i]];
                result += x * (long)Math.Pow(n, m--);
            }
            return result;
        }
        
        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };
        public static Guid TestGuid(string s)
        {
            var s2 = s.PadLeft(16, '\0');
            byte[] b = Encoding.UTF8.GetBytes(s2);
            return new Guid(b);


        }
    }
}
