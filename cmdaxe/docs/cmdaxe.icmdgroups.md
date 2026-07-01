# ICmdGroups

Namespace: cmdaxe

Represents a collection of command groups

```csharp
public interface ICmdGroups : System.Collections.Generic.IEnumerable`1[[cmdaxe.ICmdGroup, cmdaxe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Implements [IEnumerable&lt;ICmdGroup&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Context**

`cmdaxe` context

```csharp
IContext Context { get; }
```

#### Property Value

[IContext](./cmdaxe.icontext.md)<br>

### **Count**

Number of groups

```csharp
int Count { get; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **TryGet(String, out ICmdGroup)**

Attempts to find the group with the specified name

```csharp
bool TryGet(string? name, out ICmdGroup group)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
Group name

`out` `group` [ICmdGroup](./cmdaxe.icmdgroup.md)<br>
Found group

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not successful
