# IOptionsWithShortcut

Namespace: cmdaxe

Represents a collection of optional command-line parameters with a shortcut

```csharp
public interface IOptionsWithShortcut : System.Collections.Generic.IEnumerable`1[[cmdaxe.IOption, cmdaxe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Implements [IEnumerable&lt;IOption&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Count**

Number of command-line parameters in collection

```csharp
int Count { get; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **TryGet(Char, out IOption)**

Attempts to find the parameter with the specified shortcut

```csharp
bool TryGet(char shortcut, out IOption func)
```

#### Parameters

`shortcut` [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>
Parameter shortcut

`out` `func` [IOption](./cmdaxe.ioption.md)<br>
Found parameter

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not successful
