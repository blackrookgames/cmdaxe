# IParseFuncs

Namespace: cmdaxe

Represents a collection of parse functions

```csharp
public interface IParseFuncs : System.Collections.Generic.IEnumerable`1[[cmdaxe.IParseFuncInfo, cmdaxe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Implements [IEnumerable&lt;IParseFuncInfo&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Count**

Number of parse functions in collection

```csharp
int Count { get; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **TryGet(Type, out IParseFuncInfo)**

Attempts to find the parse function with the specified target type

```csharp
bool TryGet(Type? type, out IParseFuncInfo func)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)?<br>
Target type

`out` `func` [IParseFuncInfo](./cmdaxe.iparsefuncinfo.md)<br>
Found function

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not successful
