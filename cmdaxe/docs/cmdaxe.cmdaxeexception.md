# CmdAxeException

Namespace: cmdaxe

Thrown when a `cmdaxe`-related error occurs

```csharp
public class CmdAxeException : System.Exception, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception) → [CmdAxeException](./cmdaxe.cmdaxeexception.md)<br>
Implements [ISerializable](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **TargetSite**

```csharp
public MethodBase? TargetSite { get; }
```

#### Property Value

[MethodBase](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **Message**

```csharp
public virtual string Message { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

```csharp
public virtual IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**

```csharp
public Exception? InnerException { get; }
```

#### Property Value

[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**

```csharp
public virtual string? HelpLink { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**

```csharp
public virtual string? Source { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StackTrace**

```csharp
public virtual string? StackTrace { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **CmdAxeException(String)**

Initializer for [CmdAxeException](./cmdaxe.cmdaxeexception.md)

```csharp
public CmdAxeException(string? message)
```

#### Parameters

`message` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
Error message

### **CmdAxeException(String, Exception)**

Initializer for [CmdAxeException](./cmdaxe.cmdaxeexception.md)

```csharp
public CmdAxeException(string? message, Exception? inner)
```

#### Parameters

`message` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
Error message

`inner` [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception)?<br>
Inner excpetion

## Events

### **SerializeObjectState**

#### Caution

BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more information.

---

```csharp
protected event EventHandler<SafeSerializationEventArgs>? SerializeObjectState;
```
