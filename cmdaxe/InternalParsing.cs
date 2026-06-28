using System;

namespace cmdaxe
{
    /// <summary>Utility for parsing input data</summary>
    internal static partial class InternalParsing
    {
        #region string

        [ParseFunc(typeof(string))]
        internal static bool MM_TryParseString(string input, out object result)
        {
            result = input;
            return true;
        }

        #endregion

        #region bool

        /// <summary>Attempts to parse the specified string to a boolean value</summary>
        /// <param name="input">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(bool), displayName: "boolean")]
        public static bool TryParseBool(string input, out object result)
        {
            if (input != null)
            {
                //Look for text
                switch (input.ToLower())
                {
                    case "true": result = true; return true;
                    case "false": result = false; return true;
                    case "t": result = true; return true;
                    case "f": result = false; return true;
                    case "yes": result = true; return true;
                    case "no": result = false; return true;
                    case "y": result = true; return true;
                    case "n": result = false; return true;
                    case "on": result = true; return true;
                    case "off": result = false; return true;
                }
                //Look at numerical value
                if (int.TryParse(input, out int intValue))
                {
                    if (intValue == 1) { result = true; return true; }
                    if (intValue == 0) { result = false; return true; }
                }
            }
            result = default;
            return false;
        }

        #endregion

        #region single

        /// <summary>Attempts to parse the specified string to a boolean value</summary>
        /// <param name="input">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(float), displayName: "single-precision floating-point")]
        public static bool TryParseSingle(string input, out object result)
        {
            result = default;
            if (!float.TryParse(input, out var raw)) return false;
            result = raw; return true;
        }

        #endregion

        #region double

        /// <summary>Attempts to parse the specified string to a double-precision floating-point value</summary>
        /// <param name="input">String input</param>
        /// <param name="result">Parse result</param>
        /// <returns>Whether or not successful</returns>
        [ParseFunc(typeof(double), displayName: "double-precision floating-point")]
        public static bool TryParseDouble(string input, out object result)
        {
            result = default;
            if (!double.TryParse(input, out var raw)) return false;
            result = raw; return true;
        }

        #endregion
    }
}