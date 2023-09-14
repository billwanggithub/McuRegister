using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using System.Collections.ObjectModel;

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    string consoleText = "";

    [ObservableProperty]
    public ObservableCollection<RegClass> regList = new();

    public ViewModel()
    {
        Init_Registers();
    }

    void Init_Registers()
    {
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
            //Items = new()
            //{
            //    new FlagClass(){Name= "a", Pos = 0, Value = true} ,
            //    new VarClass() {Name= "var0", Mask = 0x03, Pos = 1, Value = 0xFF} ,
            //    new FlagClass(){Name= "b",Pos = 3, Value = true},
            //    new VarClass() {Name= "var1", Mask = 0x07, Pos = 4, Value = 0xFF},
            //    new FlagClass() {Name= "c", Pos = 7, Value = true},
            //},
        });

        RegList.Add(new()
        {
            DebugPrint = DebugPrint,
            Name = "reg1",
            Items = new()
            {
                new VarClass() {Name= "var2", Mask = 0x03, Pos = 1, Value = 0x00 } ,
                new VarClass() {Name= "var3", Mask = 0x07, Pos = 4, Value = 0x00 },
                new FlagClass(){Name= "d", Pos = 0, Value = false} ,
                new FlagClass(){Name= "e",Pos = 3, Value = false },
                new FlagClass() {Name= "f", Pos = 7, Value = false},
            },
        });

        RegList.Add(new()
        {
            Value = 0xAA,
            Name = "Reg2"
        });
    }

    [RelayCommand]
    void DumpRegs()
    {
        foreach (var reg in RegList)
        {
            string varString = "";
            string flagString = "";
            foreach (var item in reg.Items)
            {
                if (item.GetType() == typeof(VarClass))
                {
                    varString += $"{item.Name}:0X{item.Value:X2}\n";
                }
                if (item.GetType() == typeof(FlagClass))
                {
                    flagString += $"{item.Name}:{item.Value}\n";
                }
            }
            ConsoleText += $"{reg.Name}:0X{reg.Value:X2}\n{varString}{flagString}\n";
        }
    }

    void DebugPrint(string message)
    {
        ConsoleText += message;
    }
}
