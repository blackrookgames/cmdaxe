using System;
using System.IO;
using cmdaxe;

namespace test
{
    [Command(name: "test", desc: "This is a command description.")]
    class Cmd_test : Command
    {
        #region required

        [Required(desc: "A string value")]
        string str;

        #endregion

        #region optional

        [OptionFlag(desc: "Optional flag")]
        bool flag;

        [OptionWArg(desc: "Day of the week")]
        DayOfWeek day;

        [OptionWArg(shortcut: 'B', desc: "8-bit unsigned integer")]
        byte u8 = 0;

        [OptionWArg(shortcut: 'b', desc: "8-bit signed integer")]
        sbyte i8 = 0;

        [OptionWArg(shortcut: 'S', desc: "16-bit unsigned integer")]
        ushort u16 = 0;

        [OptionWArg(shortcut: 's', desc: "16-bit signed integer")]
        short i16 = 0;

        [OptionWArg(shortcut: 'I', desc: "32-bit unsigned integer")]
        uint u32 = 0;

        [OptionWArg(shortcut: 'i', desc: "32-bit signed integer")]
        int i32 = 0;

        [OptionWArg(shortcut: 'L', desc: "64-bit unsigned integer")]
        ulong u64 = 0;

        [OptionWArg(shortcut: 'l', desc: "64-bit signed integer")]
        long i64 = 0;

        [OptionWArg(shortcut: 'f', desc: "Single-precision floating-point decimal")]
        float single = 0.0f;

        [OptionWArg(name: "double", shortcut: 'F', desc: "Double-precision floating-point decimal")]
        double _double = 0.0f;

        [OptionWArg(name: "name", desc: "Name; this option can be specified multiple times")]
        string[] names;

        #endregion

        #region helper methods

        private static string MM_ArrayString<T>(T[] items)
        {
            if (items is null || items.Length == 0)
                return "";
            using StringWriter w = new ();
            w.Write(items[0]);
            for (int i = 1; i < items.Length; ++i)
                w.Write($", {items[i]}");
            return w.ToString();
        }
        
        #endregion

        #region methods

        public override void Main()
        {
            Console.WriteLine($"str        {str}");
            Console.WriteLine($"flag       {flag}");
            Console.WriteLine($"day        {day}");
            Console.WriteLine($"u8         {u8}");
            Console.WriteLine($"i8         {i8}");
            Console.WriteLine($"u16        {u16}");
            Console.WriteLine($"i16        {i16}");
            Console.WriteLine($"u32        {u32}");
            Console.WriteLine($"i32        {i32}");
            Console.WriteLine($"u64        {u64}");
            Console.WriteLine($"i64        {i64}");
            Console.WriteLine($"single     {single}");
            Console.WriteLine($"double     {_double}");
            Console.WriteLine($"names      {MM_ArrayString(names)}");
        }

        #endregion

    }
}