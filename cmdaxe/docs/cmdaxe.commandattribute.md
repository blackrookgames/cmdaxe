# CommandAttribute

Namespace: cmdaxe

Specifies a class as a command

NOTE: In order for the class to be considered a valid command, it must contain a parameterless constructor

```csharp
public class CommandAttribute : System.Attribute
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.attribute) → [CommandAttribute](./cmdaxe.commandattribute.md)<br>
Attributes [AttributeUsageAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **Name**

Command name

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Group**

Group command belongs to

```csharp
public string Group { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Desc**

Command description

```csharp
public string Desc { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HelpKeyword**

Keyword for displaying help

```csharp
public string HelpKeyword { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HelpShort**

Shortcut for displaying help

```csharp
public char HelpShort { get; }
```

#### Property Value

[Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>

### **TypeId**

```csharp
public virtual object TypeId { get; }
```

#### Property Value

[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **CommandAttribute(String, String, String, String, Char)**

Specifies a class as a command

NOTE: In order for the class to be considered a valid command, it must contain a parameterless constructor

```csharp
public CommandAttribute(string name = null, string group = null, string desc = null, string helpKeyword = "help", char helpShort = 'h')
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Command name

`group` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Group command belongs to

`desc` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Command description

`helpKeyword` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Keyword for displaying help

`helpShort` [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>
Shortcut for displaying help
