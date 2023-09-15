using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Model
{
    public partial class VarClass : ObservableObject
    {
        [JsonIgnore]
        public ValueChangedDelegate? ValueChanged = null;
        [JsonIgnore]
        public DebugPrintDelegate? DebugPrint = null;
        [JsonIgnore]
        [ObservableProperty]
        public string name = "";
        [JsonIgnore]
        [ObservableProperty]
        public int pos = 0;
        [JsonIgnore]
        [ObservableProperty]
        public uint mask = 1;
        [JsonIgnore]
        [ObservableProperty]
        public uint value = 0;
        partial void OnValueChanged(uint value)
        {
            Value = value & Mask;
            if (ValueChanged != null)
                ValueChanged(this, Value);
            if (DebugPrint != null)
                DebugPrint($"{Name} = 0X{Value:X2}\n");
        }
        [JsonIgnore]
        [ObservableProperty]
        public bool isEnabled = true;
    }
}








