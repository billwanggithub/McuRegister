using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace Model
{
    public partial class RegClass : ObservableObject
    {
        [ObservableProperty]
        public uint address = 0;
        [ObservableProperty]
        public string name = "";
        [ObservableProperty]
        public uint value = 0;
        [ObservableProperty]
        public ObservableCollection<dynamic>? fields;
        partial void OnFieldsChanged(ObservableCollection<dynamic>? value)
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
        partial void OnValueChanged(uint value)
        {
            if (Fields is null)
                return;

            foreach (var v in Fields)
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
        [ObservableProperty]
        public bool isEnabled = true;
        partial void OnIsEnabledChanged(bool value)
        {
            if (Fields is null)
                return;

            foreach (var v in Fields)
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

        public DebugPrintDelegate? DebugPrint = null; // defined in ViewModel
        public uint GetValue()
        {
            if (Fields is null)
                return 0;


            uint varTemp = 0;
            uint flagTemp = 0;
            foreach (var v in Fields)
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








