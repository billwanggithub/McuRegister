using System;
using System.Collections.Generic;

[Flags]
public enum Flags
{
    B0 = 1,
    B1 = 1 << 1,
    B2 = 1 << 2,
    B3 = 1 << 3,
    B4 = 1 << 4,
    B5 = 1 << 5,
    B6 = 1 << 6,
    B7 = 1 << 7,
}

public class VarClass
{
    public int Pos { get; set; } = 0;
    public uint Mask { get; set; } = 0;
    public uint Value { get; set; } = 0;
    //public VarClass(uint mask, uint pos)
    //{
    //    this.Mask = mask;
    //    this.Pos = pos;
    //}
}

public class FlagClass
{
    public int Pos { get; set; } = 0;
    public bool Value { get; set; } = false;
}

public class RegClass
{
    byte var0 = 0;
    //public Reg0Flags FlagsTemp { get; set; } = new Reg0Flags();
    public Dictionary<string, VarClass> Vars { get; set; } = new();
    public Dictionary<string, FlagClass> Flags { get; set; } = new();

    public uint FlagMask
    {
        get
        {
            uint tempMask = 0;
            foreach (var v in Flags)
            {
                tempMask |= (uint)1 << v.Value.Pos;
            }
            return tempMask;
        }
    }

    public uint Value
    {
        get
        {
            uint varTemp = 0;
            foreach (var v in Vars)
            {
                varTemp |= (v.Value.Value & v.Value.Mask) << v.Value.Pos;
            }
            uint flagTemp = 0;
            foreach (var v in Flags)
            {
                flagTemp |= (v.Value.Value) ? (uint)1 << v.Value.Pos : 0;
            }
            return varTemp | flagTemp;
        }
        set
        {
            foreach (var v in Vars)
            {
                v.Value.Value = (value >> v.Value.Pos) & v.Value.Mask;
            }
            foreach (var v in Flags)
            {
                v.Value.Value = (value & ((uint)1 << v.Value.Pos)) == ((uint)1 << v.Value.Pos);
            }
        }
    }

    //public byte Var0
    //{
    //    get
    //    {
    //        return (byte)(var0 & 0x0F);
    //    }
    //    set
    //    {
    //        var0 = (byte)(value & 0x0F);
    //    }
    //}

    //public byte Value
    //{
    //    get
    //    {
    //        return (byte)((byte)(var0 << 1) | (byte)(Flags));
    //    }
    //    set
    //    {
    //        var0 = (byte)((value & ~(byte)Reg0Flags.Mask) >> 1);
    //        Flags = 0;
    //        foreach (Reg0Flags val in Enum.GetValues(typeof(Reg0Flags)))
    //        {
    //            // You can now work with 'value' which will be one of the enum elements (Bit0, Bit1,..)
    //            Reg0Flags b = (Reg0Flags)(value & (byte)val);
    //            Flags |= b;
    //        }
    //    }
    //}

    //public Reg0()
    //{
    //    this.Value = 0;
    //}
    //public Reg0(byte value)
    //{
    //    this.Value = value;
    //}

    //public void SetFlag(Reg0Flags? val = null)
    //{
    //    FlagsTemp |= val ?? Reg0Flags.Mask;
    //}
    //public void ReSetFlag(Reg0Flags? val = null)
    //{
    //    FlagsTemp &= ~val ?? ~Reg0Flags.Mask;
    //}
}






