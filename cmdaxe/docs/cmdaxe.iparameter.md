# IParameter

Namespace: cmdaxe

Represents a command-line parameter

```csharp
public interface IParameter
```

## Properties

### **Name**

Parameter name

```csharp
string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Desc**

Parameter description

```csharp
string Desc { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **SetValue(Object, Object)**

Sets the value of the underlying field or property

```csharp
void SetValue(object instance, object value)
```

#### Parameters

`instance` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
Instance that contains the underlying field or property

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
Value

#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
`instance` is null
