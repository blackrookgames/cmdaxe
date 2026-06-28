using System;

namespace cmdaxe
{
    /// <summary>Represents a command-line parameter that must be given an argument by the user</summary>
    public interface IRequired : IParameter, IParameterWArg { }
}