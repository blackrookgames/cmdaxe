# BaseCommand

Namespace: cmdaxe

Represents a base command

```csharp
public abstract class BaseCommand
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [BaseCommand](./cmdaxe.basecommand.md)

## Properties

### **PP_HelpRequest**

```csharp
protected internal abstract bool PP_HelpRequest { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **BaseCommand()**

```csharp
protected BaseCommand()
```

## Methods

### **MM__GetSyntax(IContext, Type)**

Gets the input syntax of the command

```csharp
protected static string MM__GetSyntax(IContext context, Type type)
```

#### Parameters

`context` [IContext](./cmdaxe.icontext.md)<br>
Current context

`type` [Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)<br>
Command type

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Command input syntax

#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
`context` is null
 <br>or<br>`type` is null

### **MM_Prepare(ICmdInfo, IContext)**

```csharp
protected internal abstract void MM_Prepare(ICmdInfo info, IContext context)
```

#### Parameters

`info` [ICmdInfo](./cmdaxe.icmdinfo.md)<br>

`context` [IContext](./cmdaxe.icontext.md)<br>

### **Main()**

Main method

```csharp
public abstract void Main()
```

#### Exceptions

[CommandException](./cmdaxe.commandexception.md)<br>
An error occurred while executing the command
