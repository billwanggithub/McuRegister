using CommunityToolkit.Mvvm.ComponentModel;

namespace Model
{
    public partial class FlagClass : ObservableObject
    {
        public ValueChangedDelegate? ValueChanged = null;
        public DebugPrintDelegate? DebugPrint = null;

        [ObservableProperty]
        public string name = "";
        [ObservableProperty]
        public int pos = 0;
        [ObservableProperty]
        public bool value = false;
        partial void OnValueChanged(bool value)
        {
            if (ValueChanged != null)
                ValueChanged(this, value);
            if (DebugPrint != null)
                DebugPrint($"{Name} = {value}\n");
        }
    }
}








