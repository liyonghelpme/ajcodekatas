namespace AjPepsi
{
    using System;

    public enum ByteCode : byte
    {
        Nop = 0,
        GetVariable = 1,
        SetVariable = 2,
        GetArgument = 3,
        SetArgument = 4,
        GetConstant = 5,
        GetLocal = 6,
        SetLocal = 7,
        GetGlobalVariable = 8,
        SetGlobalVariable = 9,
        GetBlock = 10,

        GetSelf = 20,
        GetClass = 21,
        NewObject = 22,
        Pop = 23,
        ReturnPop = 24,

        Add = 40,
        Substract = 41,
        Multiply = 42,
        Divide = 43,

        Send = 50,

        InstAt = 60,
        InstAtPut = 61,
        InstSize = 62,

        GetDotNetType = 80,
        NewDotNetObject = 81,
        InvokeDotNetMethod = 82
    }
}
