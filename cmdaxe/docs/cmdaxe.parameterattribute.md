# ParameterAttribute

Namespace: cmdaxe

Specifies a command-line parameter

```csharp
public abstract class ParameterAttribute : System.Attribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [ParameterAttribute](./cmdaxe.parameterattribute.md)<br>
Attributes [AttributeUsageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

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

### **ParameterAttribute(String, String)**

Specifies a command-line parameter

```csharp
protected ParameterAttribute(string name, string desc)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter name

`desc` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter description
