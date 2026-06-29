# IParameters&lt;T&gt;

Namespace: cmdaxe

Represents a collection of command-line parameters

```csharp
public interface IParameters<T> : IEnumerable`1, System.Collections.IEnumerable where T : IParameter
```

#### Type Parameters

`T`<br>

Implements IEnumerable&lt;T&gt;, [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Count**

Number of command-line parameters in collection

```csharp
int Count { get; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **TryGet(String, out T)**

Attempts to find the parameter with the specified name

```csharp
bool TryGet(string name, out T func)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter name

`out` `func` T<br>
Found parameter

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not successful
