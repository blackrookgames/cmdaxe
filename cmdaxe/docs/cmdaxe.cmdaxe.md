# CmdAxe

Namespace: cmdaxe

Main class for `cmdaxe`

```csharp
public static class CmdAxe
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [CmdAxe](./cmdaxe.cmdaxe.md)

## Methods

### **CreateContext(IEnumerable&lt;String&gt;, Assembly, String)**

Creates a `cmdaxe` context

```csharp
public static IContext CreateContext(IEnumerable<string> input, Assembly assembly = null, string entryName = null)
```

#### Parameters

`input` [IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
User input

`assembly` [Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
Entry assembly

`entryName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Entry name

#### Returns

[IContext](./cmdaxe.icontext.md)<br>
Created context

#### Exceptions

[CmdAxeException](./cmdaxe.cmdaxeexception.md)<br>
`assembly` is null and entry assembly could not be retrieved
 <br>or<br>
 An error occurred when retrieving types defined in `assembly`<br>or<br>
 Two or more commands in the same group have the same name
 <br>or<br>
 Two or more parse functions have the same target type

### **Run(IEnumerable&lt;String&gt;, Assembly, String, String)**

Main method

```csharp
public static int Run(IEnumerable<string> input, Assembly assembly = null, string entryName = null, string group = null)
```

#### Parameters

`input` [IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
User input

`assembly` [Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
Entry assembly

`entryName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Entry name

`group` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Group of valid commands

#### Returns

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Created context

#### Exceptions

[CmdAxeException](./cmdaxe.cmdaxeexception.md)<br>
`assembly` is null and entry assembly could not be retrieved
 <br>or<br>
 An error occurred when retrieving types defined in `assembly`<br>or<br>
 Two or more commands in the same group have the same name

[KeyNotFoundException](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keynotfoundexception)<br>
Could not find a group of the specified name
