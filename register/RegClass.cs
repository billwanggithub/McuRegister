using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace Model
{
    public partial class RegClass : ObservableObject
    {
        public DebugPrintDelegate? DebugPrint = null; // defined in ViewModel

        [ObservableProperty]
        public ObservableCollection<dynamic>? items;
        partial void OnItemsChanged(ObservableCollection<dynamic>? value)
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

        [ObservableProperty]
        public string name = "";

        [ObservableProperty]
        public uint value = 0;
        partial void OnValueChanged(uint value)
        {
            if (Items is null)
                return;

            foreach (var v in Items)
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

        public void PropertyValueChanged(object obj, dynamic value)
        {
            GetValue();
            //if (DebugPrint is not null)
            //    DebugPrint($"{Name} = {Value}\n");
        }
        public uint GetValue()
        {
            if (Items is null)
                return 0;


            uint varTemp = 0;
            uint flagTemp = 0;
            foreach (var v in Items)
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
        //        foreach (var v in Items)
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
        //        foreach (var v in Items)
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








