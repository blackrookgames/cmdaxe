# IParameterWArg

Namespace: cmdaxe

Represents a command-line parameter that takes an argument

```csharp
public interface IParameterWArg
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **IsArray**

Whether or not the underlying field/property is an array

```csharp
bool IsArray { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ParseFunc**

Parse functionIf the underlying field/property is an array, this function is used to parse each item in the array

```csharp
IParseFuncInfo ParseFunc { get; }
```

#### Property Value

[IParseFuncInfo](./cmdaxe.iparsefuncinfo.md)<br>
