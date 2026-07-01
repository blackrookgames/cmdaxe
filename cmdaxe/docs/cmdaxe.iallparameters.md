# IAllParameters

Namespace: cmdaxe

Represents all command-line parameters for a command

```csharp
public interface IAllParameters : IParameters`1, System.Collections.Generic.IEnumerable`1[[cmdaxe.IParameter, cmdaxe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Collections.IEnumerable
```

Implements [IParameters&lt;IParameter&gt;](./cmdaxe.iparameters-1.md), [IEnumerable&lt;IParameter&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Required**

Parameters that must be given arguments by the user

```csharp
IParameters<IRequired> Required { get; }
```

#### Property Value

[IParameters&lt;IRequired&gt;](./cmdaxe.iparameters-1.md)<br>

### **Optional**

Optional command-line parameters

```csharp
IParameters<IOption> Optional { get; }
```

#### Property Value

[IParameters&lt;IOption&gt;](./cmdaxe.iparameters-1.md)<br>

### **Shortcuts**

Shortcuts

```csharp
IOptionsWithShortcut Shortcuts { get; }
```

#### Property Value

[IOptionsWithShortcut](./cmdaxe.ioptionswithshortcut.md)<br>
