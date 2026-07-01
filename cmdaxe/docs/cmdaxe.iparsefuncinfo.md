# IParseFuncInfo

Namespace: cmdaxe

Represents information about a parse function

```csharp
public interface IParseFuncInfo
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Type**

Target type

```csharp
Type Type { get; }
```

#### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>

### **DisplayName**

Display name for target type (ex: "8-bit unsigned integer")

```csharp
string? DisplayName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Func**

Parse function

```csharp
ParseFunc Func { get; }
```

#### Property Value

[ParseFunc](./cmdaxe.parsefunc.md)<br>
