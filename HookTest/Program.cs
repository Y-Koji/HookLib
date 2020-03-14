using HookLib;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HookTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Hook.Key += OnKey;
            Hook.Mouse += OnMouse;

            Hook.SetKeyHook();
            Hook.SetMouseHook();

            while (true)
            {
                Task.Delay(1000).Wait();
            }
        }

        /// <summary>マウス操作時</summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>true: マウス操作イベントを伝搬する, false: 伝搬しない</returns>
        private static bool OnMouse(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // マンガミーヤ表示中に、マウスカーソルの位置を表示する

            var mouse = Marshal.PtrToStructure<MOUSEHOOKSTRUCT>(lParam);

            IntPtr hWnd = WinAPI.GetForegroundWindow();
            WinAPI.GetWindowThreadProcessId(hWnd, out uint lpdwProcessId);

            Process p = Process.GetProcessById((int)lpdwProcessId);
            switch (p.ProcessName)
            {
                case "MangaMeeya":
                    Console.WriteLine("X: {0}, Y: {1}", mouse.pt.X, mouse.pt.Y);
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>キー操作時</summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>true: キー操作イベントを伝搬する, false: 伝搬しない</returns>
        private static bool OnKey(int nCode, IntPtr wParam, IntPtr lParam)
        {
            switch ((uint)wParam)
            {
                case WM.WM_KEYDOWN:
                    return OnKeyDown(nCode, wParam, lParam);

                case WM.WM_KEYUP:
                    break;
            }

            return true;
        }

        /// <summary>キー押下時の操作</summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>true: キー押下イベントを伝搬する, false: 伝搬しない</returns>
        private static bool OnKeyDown(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // マンガミーヤ表示中に、PageUp, PageDownを押下したら最小化するサンプル

            var kbd = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

            IntPtr hWnd = WinAPI.GetForegroundWindow();
            WinAPI.GetWindowThreadProcessId(hWnd, out uint lpdwProcessId);

            Process p = Process.GetProcessById((int)lpdwProcessId);
            switch (p.ProcessName)
            {
                case "MangaMeeya":
                    // ウインドウサイズ最小化処理
                    Action setMinisize = () =>
                        WinAPI.PostMessage(hWnd, WM.WM_SYSCOMMAND, (IntPtr)SC.SC_MINIMIZE, IntPtr.Zero);

                    switch (kbd.vkCode)
                    {
                        /* VK_NEXT: PageUp, VK_PRIOR: PageDown */
                        case VK.VK_NEXT: setMinisize(); return false;
                        case VK.VK_PRIOR: setMinisize(); return false;
                        default: return true;
                    }

                default: return true;
            }
        }
    }
}
