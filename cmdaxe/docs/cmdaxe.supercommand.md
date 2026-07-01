# SuperCommand

Namespace: cmdaxe

Represents a super command

```csharp
public abstract class SuperCommand : BaseCommand
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [BaseCommand](./cmdaxe.basecommand.md) → [SuperCommand](./cmdaxe.supercommand.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **PP_SubGroupName**

Name of group containing subcommands

```csharp
protected abstract string? PP_SubGroupName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PP_HelpRequest**

```csharp
protected internal sealed override bool PP_HelpRequest { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **SuperCommand()**

```csharp
protected SuperCommand()
```

## Methods

### **MM__GetSyntax(IContext, Type)**

Gets the input syntax of the command

```csharp
protected static new string MM__GetSyntax(IContext context, Type type)
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
protected internal sealed override void MM_Prepare(ICmdInfo info, IContext context)
```

#### Parameters

`info` [ICmdInfo](./cmdaxe.icmdinfo.md)<br>

`context` [IContext](./cmdaxe.icontext.md)<br>

### **Main()**

Main method

```csharp
public sealed override void Main()
```

#### Exceptions

[CommandException](./cmdaxe.commandexception.md)<br>
An error occurred while executing the command
