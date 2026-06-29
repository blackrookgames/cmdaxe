# OptionAttribute

Namespace: cmdaxe

Specifies an optional command-line parameter

```csharp
public abstract class OptionAttribute : ParameterAttribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [ParameterAttribute](./cmdaxe.parameterattribute.md) → [OptionAttribute](./cmdaxe.optionattribute.md)<br>
Attributes [AttributeUsageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **Shortcut**

Shortcut character

```csharp
public char Shortcut { get; }
```

#### Property Value

[Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>

### **Name**

Parameter name

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Desc**

Parameter description

```csharp
public string Desc { get; }
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

### **OptionAttribute(String, Char, String)**

Specifies an optional command-line parameter

```csharp
protected OptionAttribute(string name, char shortcut, string desc)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter name; this is also its keyword

`shortcut` [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>
Shortcut character

`desc` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter description
