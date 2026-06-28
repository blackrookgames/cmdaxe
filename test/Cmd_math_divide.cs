using System;

using cmdaxe;

namespace test
{
    [Command(name: "divide", group: "math", desc: "Performs a division operation")]
    class Cmd_math_divide : Command
    {
        #region fields

        private double f_Number0 = 1;
        private double f_Number1 = 1;

        #endregion

        #region required
        
#pragma warning disable IDE1006 // Naming Styles

        [Required(desc: "Dividend")]
        public double number0
        {
            get
            {
                return f_Number0;
            }
            set
            {
                f_Number0 = value;
            }
        }

        [Required(desc: "Divisor")]
        public double number1
        {
            get
            {
                return f_Number1;
            }
            set
            {
                if (value != 0)
                    throw new CommandException("Cannot divide by zero");
                f_Number1 = value;
            }
        }
        
#pragma warning restore IDE1006 // Naming Styles

        #endregion

        #region methods

        public override void Main()
        {
            Console.WriteLine($"{f_Number0} / {f_Number1} = {f_Number0 / f_Number1}");
        }

        #endregion

    }
}