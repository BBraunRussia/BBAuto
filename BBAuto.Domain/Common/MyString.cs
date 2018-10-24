using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Common
{
    public static class MyString
    {
        private const int COUNT_DIGIT_IN_DISCHARGE = 3;
        
        public static string GetFormatedDigitInteger(string value)
        {
            if (IsEmpty(value))
                return value;

            return GetDigitWithSpace(value);
        }

        public static string GetFormatedDigit(string value)
        {
            if (IsEmpty(value))
                return value;

            return GetDigitWithSpace(value) + GetFactionPart(value);
        }

        private static bool IsEmpty(string value)
        {
            return value == string.Empty;
        }

        private static string GetFactionPart(string value)
        {
            string[] splitValue = SplitString(value);

            if (splitValue.Count() == 2)
                return "." + splitValue[1];

            return ".00";
        }

        private static string GetDigitWithSpace(string value)
        {
            string integerPart = GetIntegerPart(value);

            Char[] charArray = GetDigitWithSpaceReverse(integerPart, integerPart.Count() - 1).Trim().ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        private static string GetIntegerPart(string value)
        {
            return SplitString(value)[0];
        }

        private static string[] SplitString(string value)
        {
            return value.Replace(" ", "").Replace(",", ".").Split('.');
        }

        private static string GetDigitWithSpaceReverse(string integerPart, int maxIndex)
        {
            if (maxIndex < 0)
                return "";

            if ((integerPart.Count() - 1 - maxIndex) % COUNT_DIGIT_IN_DISCHARGE == 2)
                return integerPart[maxIndex].ToString() + " " + GetDigitWithSpaceReverse(integerPart, maxIndex - 1);

            return integerPart[maxIndex].ToString() + GetDigitWithSpaceReverse(integerPart, maxIndex - 1);
        }
    }
}
