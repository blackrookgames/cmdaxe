# IOptionWArg

Namespace: cmdaxe

Specifies a command-line option that takes an argument

```csharp
public interface IOptionWArg : IOption, IParameter, IParameterWArg
```

Implements [IOption](./cmdaxe.ioption.md), [IParameter](./cmdaxe.iparameter.md), [IParameterWArg](./cmdaxe.iparameterwarg.md)

## Properties

### **ArgType**

Argument display type; this tells the user what kind of argument should be inputted<br>
 Examples: "number", "name", "path", "directory"

```csharp
string ArgType { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
