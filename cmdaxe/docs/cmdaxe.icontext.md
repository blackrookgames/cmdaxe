# IContext

Namespace: cmdaxe

Represents a `cmdaxe` context

```csharp
public interface IContext
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Original**

Original context

```csharp
IContext? Original { get; }
```

#### Property Value

[IContext](./cmdaxe.icontext.md)<br>

### **EntryName**

Name of the entry assembly

```csharp
string EntryName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CommandGroups**

Command groups

```csharp
ICmdGroups CommandGroups { get; }
```

#### Property Value

[ICmdGroups](./cmdaxe.icmdgroups.md)<br>

### **ParseFuncs**

Parse functions

```csharp
IParseFuncs ParseFuncs { get; }
```

#### Property Value

[IParseFuncs](./cmdaxe.iparsefuncs.md)<br>

### **Prefixes**

Syntax prefixes

```csharp
ImmutableArray<string> Prefixes { get; }
```

#### Property Value

[ImmutableArray&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablearray-1)<br>

### **RawInput**

User raw input

```csharp
ImmutableArray<string> RawInput { get; }
```

#### Property Value

[ImmutableArray&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablearray-1)<br>
