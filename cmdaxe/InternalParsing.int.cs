namespace cmdaxe
{
    internal static partial class InternalParsing
    {
        #pragma warning disable CS0675

        /// <summary>Attempts to parse the specified string to an 8-bit unsigned integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(byte), displayName: "8-bit unsigned integer")]
        public static bool TryParseUInt8(string s, out object result)
        {
            const int hexDigits = 2;
            byte rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (byte)digit;
                }
            }
            else
            {
                if (!byte.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to an 8-bit signed integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(sbyte), displayName: "8-bit signed integer")]
        public static bool TryParseInt8(string s, out object result)
        {
            const int hexDigits = 2;
            sbyte rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (sbyte)digit;
                }
            }
            else
            {
                if (!sbyte.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 16-bit unsigned integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(ushort), displayName: "16-bit unsigned integer")]
        public static bool TryParseUInt16(string s, out object result)
        {
            const int hexDigits = 4;
            ushort rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (ushort)digit;
                }
            }
            else
            {
                if (!ushort.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 16-bit signed integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(short), displayName: "16-bit signed integer")]
        public static bool TryParseInt16(string s, out object result)
        {
            const int hexDigits = 4;
            short rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (short)digit;
                }
            }
            else
            {
                if (!short.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 32-bit unsigned integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(uint), displayName: "32-bit unsigned integer")]
        public static bool TryParseUInt32(string s, out object result)
        {
            const int hexDigits = 8;
            uint rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (uint)digit;
                }
            }
            else
            {
                if (!uint.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 32-bit signed integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(int), displayName: "32-bit signed integer")]
        public static bool TryParseInt32(string s, out object result)
        {
            const int hexDigits = 8;
            int rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (int)digit;
                }
            }
            else
            {
                if (!int.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 64-bit unsigned integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(ulong), displayName: "64-bit unsigned integer")]
        public static bool TryParseUInt64(string s, out object result)
        {
            const int hexDigits = 16;
            ulong rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (ulong)digit;
                }
            }
            else
            {
                if (!ulong.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        /// <summary>Attempts to parse the specified string to a 64-bit signed integer value</summary>
        /// <param name="s">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(long), displayName: "64-bit signed integer")]
        public static bool TryParseInt64(string s, out object result)
        {
            const int hexDigits = 16;
            long rawResult;
            if (s == null) goto invalid;
            if (s.StartsWith("0x") || s.StartsWith("0X"))
            {
                if (s.Length > (2 + hexDigits))
                    goto invalid;
                rawResult = 0;
                for (int i = 2; i < s.Length; i++)
                {
                    char c = s[i];
                    int digit;
                    //If number
                    if (c >= '0' && c <= '9') digit = c - '0';
                    //If uppercase
                    else if (c >= 'A' && c <= 'F') digit = (c - 'A') + 10;
                    //If lowercase
                    else if (c >= 'a' && c <= 'f') digit = (c - 'a') + 10;
                    //If anything else
                    else goto invalid;
                    //Update value
                    rawResult <<= 4;
                    rawResult |= (long)digit;
                }
            }
            else
            {
                if (!long.TryParse(s, out rawResult))
                    goto invalid;
            }
            result = rawResult;
            return true;
        invalid:
            result = default;
            return false;
        }

        #pragma warning restore CS0675
    }
}