using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace Model
{
    public partial class RegClass : ObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public uint address = 0;
        [JsonIgnore]
        [ObservableProperty]
        public string name = "";
        [JsonIgnore]
        [ObservableProperty]
        public uint value = 0;
        partial void OnValueChanged(uint value)
        {
            if (SubFields is null)
                return;

            foreach (var v in SubFields)
            {
                Type type = v.GetType();
                if (type == typeof(VarClass))
                {
                    v.Value = (value >> v.Pos) & v.Mask; // Set Variables
                }
                else if (type == typeof(FlagClass)) // Set Flags
                {
                    v.Value = (value & ((uint)1 << v.Pos)) == ((uint)1 << v.Pos);
                }
            }

            if (DebugPrint != null)
                DebugPrint($"{Name} = 0X{value:X2}\n");
        }
        [JsonIgnore]
        [ObservableProperty]
        public uint mask = 0xFFFFFFFF;
        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<dynamic>? subFields;
        partial void OnSubFieldsChanged(ObservableCollection<dynamic>? value)
        {
            if (value is null)
                return;

            foreach (dynamic item in value)
            {
                Type type = item.GetType();
                if (type == typeof(VarClass))
                {
                    ((VarClass)item).ValueChanged = PropertyValueChanged;
                    ((VarClass)item).DebugPrint = DebugPrint;
                }
                else if (type == typeof(FlagClass))
                {
                    ((FlagClass)item).ValueChanged = PropertyValueChanged;
                    ((FlagClass)item).DebugPrint = DebugPrint;
                }
            }

            GetValue();
        }
        [JsonIgnore]
        [ObservableProperty]
        public bool isEnabled = true;
        partial void OnIsEnabledChanged(bool value)
        {
            if (SubFields is null)
                return;

            foreach (var v in SubFields)
            {
                v.IsEnabled = IsEnabled;
            }
        }
        public void PropertyValueChanged(object obj, dynamic value)
        {
            GetValue();
            //if (DebugPrint is not null)
            //    DebugPrint($"{Name} = {Value}\n");
        }
        [JsonIgnore]
        public DebugPrintDelegate? DebugPrint = null; // defined in ViewModel
        public uint GetValue()
        {
            if (SubFields is null)
                return 0;


            uint varTemp = 0;
            uint flagTemp = 0;
            foreach (var v in SubFields)
            {
                if (v.GetType() == typeof(VarClass))
                {
                    varTemp |= (v.Value & v.Mask) << v.Pos; // OR all Variable
                }
                else if (v.GetType() == typeof(FlagClass)) // OR all Flags
                {
                    flagTemp |= (v.Value) ? (uint)1 << v.Pos : 0;
                }
            }
            return Value = varTemp | flagTemp;
        }
        //public uint Value1
        //{
        //    get
        //    {
        //        uint varTemp = 0;
        //        uint flagTemp = 0;
        //        foreach (var v in Fields)
        //        {
        //            if (v.GetType() == typeof(VarClass))
        //            {
        //                varTemp |= (v.Value & v.Mask) << v.Pos;
        //            }
        //            else if (v.GetType() == typeof(FlagClass))
        //            {
        //                flagTemp |= (v.Value) ? (uint)1 << v.Pos : 0;
        //            }
        //        }
        //        _value = varTemp | flagTemp;
        //        return _value;
        //    }
        //    set
        //    {
        //        _value = value;
        //        foreach (var v in Fields)
        //        {
        //            Type type = v.GetType();
        //            if (type == typeof(VarClass))
        //            {
        //                v.Value = (value >> v.Pos) & v.Mask;
        //            }
        //            else if (type == typeof(FlagClass))
        //            {
        //                v.Value = (value & ((uint)1 << v.Pos)) == ((uint)1 << v.Pos);
        //            }
        //        }
        //    }
        //}
    }
}








