using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Slave.Application.Encript
{
    public static class GrayCodeProvider
    {
        public static string GetMD5Hash(this string input)
        {
            string result = "";
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            foreach (var b in hash)
            {
                result += b.ToString("x2");
            }
            return result;
        }

        public static string ToBase36(this int code)
        {
            return Base36Notation.Encode(code, 8);
        }

        public static string ToBase36(this long code)
        {
            return Base36Notation.Encode(code, 8);
        }

        public static string ToGrayCode(this int code)
        {
            return Base36Notation.Encode(code, 8);
        }

        public static string ToGrayCode(this long code)
        {
            return Base36Notation.Encode(code, 8);
        }

        public static string NextGrayCode(this string grayCode)
        {
            return grayCode.NextGrayCode(12);
        }

        public static string NextGrayCode(this string grayCode, Order order)
        {
            return grayCode.NextGrayCode(12, order);
        }

        private static string NextGrayCode(this string grayCode, byte numberLength, Order order = Order.Gray)
        {
            if (numberLength < 1 || numberLength > 12) //limeted by "long" size
                throw new ArgumentOutOfRangeException("numberLength", numberLength, "parameter value have to be in [1, 12] range.");
            long nextCode = 0;
            string result;

            if (!string.IsNullOrWhiteSpace(grayCode))
            {
                var lastNumber10 = Base36Notation.Decode(grayCode);
                switch (order)
                {
                    case Order.Gray:
                        nextCode = GrayCoder.NextCode(lastNumber10);
                        break;
                    case Order.ReversedBits:
                        nextCode = ReversedBitsCoder.NextCode(lastNumber10, numberLength * 5); // 2^5 = 32 ≈ 36
                        break;
                }
                result = Base36Notation.Encode(nextCode, numberLength);
                if (result.Length <= numberLength)
                    return result;
            }

            switch (order)
            {
                case Order.Gray:
                    nextCode = GrayCoder.Encode(0);
                    break;
                case Order.ReversedBits:
                    nextCode = ReversedBitsCoder.Encode(0, numberLength * 5);
                    break;
            }

            result = Base36Notation.Encode(nextCode, numberLength);
            return result;
        }

        private static class ReversedBitsCoder
        {
            public static long NextCode(long lastCode, int bitCount)
            {
                return Encode(Decode(lastCode, bitCount) + 1, bitCount);
            }

            public static long Encode(long num, int bitCount)
            {
                return ReverseBits(num, bitCount);
            }

            private static long Decode(long num, int bitCount)
            {
                return ReverseBits(num, bitCount);
            }

            private static long ReverseBits(long x, int bitCount)
            {
                var bytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                new BitArray(new BitArray(BitConverter.GetBytes(x)).Cast<bool>().Take(bitCount).Reverse().Concat(Enumerable.Repeat(false, 64 - bitCount)).ToArray()).CopyTo(bytes, 0); // 64 = size of long
                return BitConverter.ToInt64(bytes, 0);
            }
        }

        private static class Base36Notation
        {
            private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            public static uint Decode(string number36)
            {
                return PositionalNotations.Decode(number36.Select(c => (byte)Array.IndexOf(Digits, c)), 36);
            }

            public static string Encode(long number10, int length)
            {
                return string.Join("", PositionalNotations.Encode(number10, 36).Select(i => Digits[i])).PadLeft(length, '0');
            }
        }

        private static class PositionalNotations // http://en.wikipedia.org/wiki/Positional_notation
        {
            public static uint Decode(IEnumerable<byte> digits, byte theBase)
            {
                // ∑aᵢb^i
                uint result = 0;
                uint poweredBase = 1;
                foreach (var a in digits.Reverse())
                {
                    result += a * poweredBase;
                    poweredBase *= theBase;
                }
                return result;
            }

            public static IEnumerable<int> Encode(long number10, int theBase)
            {
                var result = new List<int>();
                while (number10 != 0)
                {
                    var modulo = (int)(number10 % theBase);
                    number10 /= theBase;
                    result.Add(modulo);
                }
                result.Reverse();
                return result;
            }
        }

        private static class GrayCoder // http://en.wikipedia.org/wiki/Gray_code
        {
            public static long NextCode(uint lastCode)
            {
                return Encode(Decode(lastCode) + 1);
            }

            public static long Encode(uint num)
            {
                return (num >> 1) ^ num;
            }

            private static uint Decode(uint num)
            {
                uint mask;
                for (mask = num >> 1; mask != 0; mask = mask >> 1)
                {
                    num = num ^ mask;
                }
                return num;
            }
        }
    }

    public enum Order
    {
        Gray,
        ReversedBits
    }
}
