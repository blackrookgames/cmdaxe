# Command

Namespace: cmdaxe

Represents an executable command

```csharp
public abstract class Command : BaseCommand
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [BaseCommand](./cmdaxe.basecommand.md) → [Command](./cmdaxe.command.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **PP_HelpRequest**

```csharp
protected internal sealed override bool PP_HelpRequest { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **Command()**

```csharp
protected Command()
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
