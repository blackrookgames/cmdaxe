using System;

using cmdaxe;

namespace test
{
    [Command(name: "multiply", group: "math", desc: "Performs a multiplication operation")]
    class Cmd_math_multiply : Command
    {
        #region required

        [Required(desc: "First number")]
        double number0;

        [Required(desc: "Second number")]
        double number1;

        #endregion

        #region methods

        public override void Main()
        {
            Console.WriteLine($"{number0} * {number1} = {number0 * number1}");
        }

        #endregion

    }
}