# RequiredAttribute

Namespace: cmdaxe

Specifies a command-line parameter that must be given an argument by the user

Fields take order priority over properties.

Arrays are also supported as an underlying field/property, 
 but only one is supported per command and it will always be parsed last.

```csharp
public class RequiredAttribute : ParameterAttribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [ParameterAttribute](./cmdaxe.parameterattribute.md) → [RequiredAttribute](./cmdaxe.requiredattribute.md)<br>
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

### **RequiredAttribute(String, String)**

Specifies a command-line parameter that must be given an argument by the user

Fields take order priority over properties.

Arrays are also supported as an underlying field/property, 
 but only one is supported per command and it will always be parsed last.

```csharp
public RequiredAttribute(string name = null, string desc = null)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter name

`desc` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Parameter description
