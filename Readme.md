# General MCU register data structure Test

![Demo](assets/demo.png)

## Usage

- Add Project Reference to Converters
- Add Register Database

```
RegList.Add(new()
{
    //DebugPrint = DebugPrint,
    Name = "reg0",
    Items = new()
    {
        new FlagClass(){Name= "a", Pos = 0} ,
        new VarClass() {Name= "var0", Mask = 0x03, Pos = 1, Value = 0} ,
        new FlagClass(){Name= "b",Pos = 3},
        new VarClass() {Name= "var1", Mask = 0x07, Pos = 4 , Value = 0},
        new FlagClass() {Name= "c", Pos = 7},
    },
    Value = 0xFF
});
```

- Assign register list to ItemsSource in ViewModel
```
<TreeView ItemsSource="{Binding RegList}" x:Name="TreeviewContainer">
```

- Add the following to Xaml
```
<uc:McuRegUserControl></uc:McuRegUserControl>
``` 