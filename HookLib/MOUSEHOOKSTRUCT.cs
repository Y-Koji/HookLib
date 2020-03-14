using System;
using System.Runtime.InteropServices;

namespace HookLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCT
    {
        public Point pt;
        public IntPtr hwnd;
        public uint wHitTestCode;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseHookStructEx
    {
        public MOUSEHOOKSTRUCT mouseHookStruct;
        public int MouseData;
    }
}
