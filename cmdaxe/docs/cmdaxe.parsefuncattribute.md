# ParseFuncAttribute

Namespace: cmdaxe

Specifies a method a parse function

In order for the method to be considered a parse function, it must

- 
- 
-

```csharp
public class ParseFuncAttribute : System.Attribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [ParseFuncAttribute](./cmdaxe.parsefuncattribute.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [AttributeUsageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **Type**

Target type

```csharp
public Type Type { get; }
```

#### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>

### **DisplayName**

Display name for target type (ex: "8-bit unsigned integer")

```csharp
public string? DisplayName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TypeId**

```csharp
public virtual object TypeId { get; }
```

#### Property Value

[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **ParseFuncAttribute(Type, String)**

Specifies a method a parse function

In order for the method to be considered a parse function, it must

- 
- 
-

```csharp
public ParseFuncAttribute(Type type, string? displayName = null)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>
Target type

`displayName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
Display name for target type (ex: "8-bit unsigned integer")
