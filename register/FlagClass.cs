using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Model
{
    public partial class FlagClass : ObservableObject
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
        public bool value = false;
        partial void OnValueChanged(bool value)
        {
            if (ValueChanged != null)
                ValueChanged(this, value);
            if (DebugPrint != null)
                DebugPrint($"{Name} = {value}\n");
        }
        [JsonIgnore]
        [ObservableProperty]
        public bool isEnabled = true;
    }
}








