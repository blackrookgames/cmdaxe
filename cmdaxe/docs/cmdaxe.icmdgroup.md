# ICmdGroup

Namespace: cmdaxe

Represents a command group

```csharp
public interface ICmdGroup : System.Collections.Generic.IEnumerable`1[[cmdaxe.ICmdInfo, cmdaxe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Implements [IEnumerable&lt;ICmdInfo&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Groups**

All command groups

```csharp
ICmdGroups Groups { get; }
```

#### Property Value

[ICmdGroups](./cmdaxe.icmdgroups.md)<br>

### **Name**

Group name

```csharp
string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Count**

Number of commands in group

```csharp
int Count { get; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **TryGet(String, out ICmdInfo)**

Attempts to find the command with the specified name

```csharp
bool TryGet(string name, out ICmdInfo command)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
Command name

`out` `command` [ICmdInfo](./cmdaxe.icmdinfo.md)<br>
Found command

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not successful

### **Run(IContext)**

Executes a command based on context input

```csharp
virtual int Run(IContext context)
```

#### Parameters

`context` [IContext](./cmdaxe.icontext.md)<br>
Current context

#### Returns

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Exit code

### **Run()**

Executes a command based on context input

```csharp
virtual int Run()
```

#### Returns

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>
Exit code
