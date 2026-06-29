# IOptionFlag

Namespace: cmdaxe

Represents a command-line flag

```csharp
public interface IOptionFlag : IOption, IParameter
```

Implements [IOption](./cmdaxe.ioption.md), [IParameter](./cmdaxe.iparameter.md)

## Properties

### **IsHelp**

Whether or not this is the help flag

```csharp
bool IsHelp { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
