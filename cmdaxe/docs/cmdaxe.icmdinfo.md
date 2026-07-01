# ICmdInfo

Namespace: cmdaxe

Represents information about a command

```csharp
public interface ICmdInfo
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Group**

Group command belongs to

```csharp
ICmdGroup Group { get; }
```

#### Property Value

[ICmdGroup](./cmdaxe.icmdgroup.md)<br>

### **Type**

Type

```csharp
Type Type { get; }
```

#### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Name**

Command name

```csharp
string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Desc**

Command description

```csharp
string? Desc { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HelpKeyword**

Keyword for displaying help

```csharp
string? HelpKeyword { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HelpShort**

Shortcut for displaying help

```csharp
char HelpShort { get; }
```

#### Property Value

[Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)<br>

## Methods

### **Run(IContext)**

Executes based on context input

```csharp
virtual int Run(IContext? context)
```

#### Parameters

`context` [IContext](./cmdaxe.icontext.md)?<br>
Current context

#### Returns

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Exit code

### **Run()**

Executes based on context input

```csharp
virtual int Run()
```

#### Returns

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Exit code

### **GetSyntax(IContext)**

Creates a string of the full syntax of the command

```csharp
virtual string GetSyntax(IContext? context)
```

#### Parameters

`context` [IContext](./cmdaxe.icontext.md)?<br>
Current context

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Full syntax of the command

### **GetSyntax()**

Creates a string of the full syntax of the command

```csharp
virtual string GetSyntax()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Full syntax of the command

### **IsHelp(String)**

Checks whether or not the specified string indicates a help keyword

```csharp
virtual bool IsHelp(string? s)
```

#### Parameters

`s` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
String to check

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not `s` indicates a help keyword
