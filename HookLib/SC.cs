namespace HookLib
{
    public static class SC
    {
        public const uint SC_SIZE = 0xF000;
        public const uint SC_MOVE = 0xF010;
        public const uint SC_MINIMIZE = 0xF020;
        public const uint SC_MAXIMIZE = 0xF030;
        public const uint SC_NEXTWINDOW = 0xF040;
        public const uint SC_PREVWINDOW = 0xF050;
        public const uint SC_CLOSE = 0xF060;
        public const uint SC_VSCROLL = 0xF070;
        public const uint SC_HSCROLL = 0xF080;
        public const uint SC_MOUSEMENU = 0xF090;
        public const uint SC_KEYMENU = 0xF100;
        public const uint SC_ARRANGE = 0xF110;
        public const uint SC_RESTORE = 0xF120;
        public const uint SC_TASKLIST = 0xF130;
        public const uint SC_SCREENSAVE = 0xF140;
        public const uint SC_HOTKEY = 0xF150;
        //#if(WINVER >= 0x0400) //Win95
        public const uint SC_DEFAULT = 0xF160;
        public const uint SC_MONITORPOWER = 0xF170;
        public const uint SC_CONTEXTHELP = 0xF180;
        public const uint SC_SEPARATOR = 0xF00F;
        //#endif /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0600) //Vista
        public const uint SCF_ISSECURE = 0x00000001;
        //#endif /* WINVER >= 0x0600 */

        /*
          * Obsolete names
          */
        public const uint SC_ICON = SC_MINIMIZE;
        public const uint SC_ZOOM = SC_MAXIMIZE;
    }
}
