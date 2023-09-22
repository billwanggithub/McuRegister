using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    string consoleText = "";

    [ObservableProperty]
    int progressBarValue = 50;
    [ObservableProperty]
    int progressBarMax = 100;

    [ObservableProperty]
    public List<RegClass> regList = new();

    public ViewModel()
    {
        //Init_Registers();
        Init_G2198();
    }

    void Init_Registers()
    {
        RegList.Add(new()
        {
            //DebugPrint = DebugPrint,
            Name = "reg0",
            SubFields = new()
            {
                new FlagClass(){Name= "a", Pos = 0} ,
                new VarClass() {Name= "var0", Mask = 0x03, Pos = 1, Value = 0} ,
                new FlagClass(){Name= "b",Pos = 3},
                new VarClass() {Name= "var1", Mask = 0x07, Pos = 4 , Value = 0},
                new FlagClass() {Name= "c", Pos = 7},
            },
            Value = 0xFF
            //SubFields = new()
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
            SubFields = new()
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

    void Init_G2198()
    {
        RegList.Add(new()
        {
            Address = 0,
            Name = "TMST_VALUE",
            Mask = 0xFF,
            Value = 0x00,
        });
        RegList.Add(new()
        {
            Address = 1,
            Name = "VPOS1 Control",
            SubFields = new()
            {
                new VarClass() {Name= "VPOS1[9:8]", Mask = 0x03, Pos = 0} ,
                new FlagClass(){Name= "FREQ_VP1", Pos = 2} ,
                new FlagClass(){Name= "MODE_VP1", Pos = 3} ,
                new VarClass() {Name= "FIXED[1:0]", Mask = 0x03, Pos = 4} ,
                new FlagClass(){Name= "TRACK1", Pos = 6} ,
                new FlagClass(){Name= "DIS", Pos = 7} ,
            },
            Mask = 0xFF,
            Value = 0x07,
        });
        RegList.Add(new()
        {
            Address = 2,
            Name = "VPOS1[7:0]",
            Mask = 0xFF,
            Value = 0x20,
        });
        RegList.Add(new()
        {
            Address = 3,
            Name = "VNEG1 Control",
            SubFields = new()
            {
                new VarClass() {Name= "VNEG1[9:8]", Mask = 0x03, Pos = 0} ,
                new FlagClass(){Name= "FREQ_VN1", Pos = 2} ,
                new FlagClass(){Name= "MODE_VN1", Pos = 3} ,
                new VarClass() {Name= "Reserved", Mask = 0x07, Pos = 4} ,
                new FlagClass(){Name= "DIS", Pos = 7} ,
            },
            Mask = 0xFF,
            Value = 0x07,
        });
        RegList.Add(new()
        {
            Address = 4,
            Name = "VNEG1[7:0]",
            Mask = 0xFF,
            Value = 0x20,
        });
        for (int i = 0; i < 20; i++)
        {
            RegList.Add(new()
            {
                SubFields = new()
            {
                new VarClass() {Name= "VPOS1[9:8]", Mask = 0x03, Pos = 0} ,
                new FlagClass(){Name= "FREQ_VP1", Pos = 2} ,
                new FlagClass(){Name= "MODE_VP1", Pos = 3} ,
                new VarClass() {Name= "FIXED[1:0]", Mask = 0x03, Pos = 4} ,
                new FlagClass(){Name= "TRACK1", Pos = 6} ,
                new FlagClass(){Name= "DIS", Pos = 7} ,
            },
                Address = (uint)i + 6,
                Name = $"Reg{i}",
                Mask = 0xFF,
                Value = 0x20,
            });
        }
    }
    [RelayCommand]
    async Task DumpRegs(object? parameter)
    {
        await Task.Run(async () =>
        {
            ProgressBarValue = 0;
            ProgressBarMax = RegList.Count - 1;
            foreach (RegClass reg in RegList)
            {
                string? regString = reg.Dump();
                ConsoleText += regString ?? "";
                ProgressBarValue++;
                await Task.Delay(1);
            }
        });
    }
    [RelayCommand]
    void ReadRegister(object? parameter)
    {
        if (parameter == null)
            return;
        RegClass? reg = GetReg((string)parameter);
        string? regString = reg!.Dump();
        ConsoleText += regString;
    }
    [RelayCommand]
    void WriteJson(object? parameter)
    {
        SaveToJson(RegList);
    }
    [RelayCommand]
    void ReadJson(object? parameter)
    {
        ReadFromJson();
    }
    [RelayCommand]
    void AddReg(object? parameter)
    {
        //TODO: popup a window to edit
        RegList.Add(new());
    }

    public RegClass? GetReg(string name)
    {
        return RegList.Where(x => x.Name == name).Select(x => x).FirstOrDefault();
    }

    void DebugPrint(string message)
    {
        ConsoleText += message;
    }

    void SaveToJson(List<RegClass> regs)
    {
        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };
        string json = JsonConvert.SerializeObject(regs, jsonSerializerSettings);
        SaveFileDialog saveFileDialog = new()
        {
            RestoreDirectory = true,
            Title = "Save Setting",
            DefaultExt = "json",
            Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
        };
        saveFileDialog.ShowDialog();
        string fileName = saveFileDialog.FileName;
        if (fileName == "")
            return;
        File.WriteAllText(fileName, json);
    }
    void ReadFromJson()
    {
        OpenFileDialog openFileDialog = new()
        {
            RestoreDirectory = true,
            Title = "Load Setting",
            DefaultExt = "json",
            Filter = "json files (*.json)|*.json|All files (*.*)|*.*",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true
        };
        openFileDialog.ShowDialog();
        if (openFileDialog.FileName == "")
            return;
        string fn = openFileDialog.FileName;
        string json = File.ReadAllText(fn);
        RegList = new();

        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };
        RegList = JsonConvert.DeserializeObject<List<RegClass>>(json, jsonSerializerSettings)!;
    }
}
