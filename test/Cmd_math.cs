using System;

using cmdaxe;

namespace test
{
    [Command(name: "math", desc: "Math commands")]
    class Cmd_math : SuperCommand
    {
        protected override string PP_SubGroupName => "math";
    }
}