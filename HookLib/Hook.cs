using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HookLib
{
    public static class Hook
    {
        public delegate bool HookEvent(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HModule { get; } = Process.GetCurrentProcess().MainModule.BaseAddress;
        private static IntPtr MouseHookHandle { get; set; } = IntPtr.Zero;
        private static IntPtr KeyHookHandle { get; set; } = IntPtr.Zero;
        private static IntPtr HInstance { get; } = WinAPI.GetModuleHandle(null);
        private static uint ThreadId { get; } = WinAPI.GetCurrentThreadId();

        public static event HookEvent Mouse;
        public static event HookEvent Key;
        
        private static HookProc KeyHookProc { get; } = new HookProc((nCode, wParam, lParam) =>
        {
            try
            {
                if (Key?.Invoke(nCode, wParam, lParam) ?? true)
                {
                    return WinAPI.CallNextHookEx(KeyHookHandle, nCode, wParam, lParam);
                }
                else
                {
                    // キー操作無効化
                    return (IntPtr) 1;
                }
            }
            catch (Exception e)
            {
                return WinAPI.CallNextHookEx(KeyHookHandle, nCode, wParam, lParam);
            }
        });

        private static HookProc MouseHookProc { get; } = new HookProc((nCode, wParam, lParam) =>
        {
            try
            {
                if (Mouse?.Invoke(nCode, wParam, lParam) ?? true)
                {
                    return WinAPI.CallNextHookEx(KeyHookHandle, nCode, wParam, lParam);
                }
                else
                {
                    // マウス操作無効化
                    return (IntPtr) 1;
                }
            }
            catch (Exception e)
            {
                return WinAPI.CallNextHookEx(KeyHookHandle, nCode, wParam, lParam);
            }
        });

        static Hook()
        {
            GCHandle.Alloc(KeyHookProc, GCHandleType.Normal);
            GCHandle.Alloc(MouseHookProc, GCHandleType.Normal);
        }

        public static void SetMouseHook()
        {
            if (IntPtr.Zero == MouseHookHandle)
            {
                MouseHookHandle = WinAPI.SetWindowsHookEx(HookType.WH_MOUSE_LL, MouseHookProc, HModule, 0u);
            }
        }

        public static void UnSetMouseHook()
        {
            if (IntPtr.Zero != MouseHookHandle)
            {
                if (WinAPI.UnhookWindowsHookEx(MouseHookHandle))
                {
                    MouseHookHandle = IntPtr.Zero;
                }
                else
                {
                    // 失敗 (未定義)
                }
            }
        }

        public static void SetKeyHook()
        {
            if (IntPtr.Zero == KeyHookHandle)
            {
                KeyHookHandle = WinAPI.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, KeyHookProc, HModule, 0u);
            }
        }

        public static void UnSetKeyHook()
        {
            if (IntPtr.Zero != KeyHookHandle)
            {
                if (WinAPI.UnhookWindowsHookEx(KeyHookHandle))
                {
                    KeyHookHandle = IntPtr.Zero;
                }
                else
                {
                    // 失敗 (未定義)
                }
            }
        }
    }
}
