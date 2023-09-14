using CommunityToolkit.Mvvm.ComponentModel;

namespace Model
{
    public partial class VarClass : ObservableObject
    {
        public ValueChangedDelegate? ValueChanged = null;
        public DebugPrintDelegate? DebugPrint = null;

        [ObservableProperty]
        public string name = "";
        [ObservableProperty]
        public int pos = 0;
        [ObservableProperty]
        public uint mask = 1;
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
    }
}








