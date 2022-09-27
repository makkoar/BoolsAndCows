namespace BoolsAndCows
{
    public static class IConsole
    {
        //============================================================================================================
        //Код инициализации.
        //============================================================================================================

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[32];
        }
        [StructLayout(LayoutKind.Sequential)]
        struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }
        const int STD_OUTPUT_HANDLE = -11;
        const int TMPF_TRUETYPE = 4;
        static readonly IntPtr INVALID_HANDLE_VALUE = new(-1);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int dwType);
        internal unsafe static void Init()
        {
            if (OperatingSystem.IsWindows())
            {
                const string fontName = "Lucida Console";
                IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
                if (hnd != INVALID_HANDLE_VALUE)
                {
                    CONSOLE_FONT_INFO_EX info = new();
                    info.cbSize = (uint)Marshal.SizeOf(info);

                    CONSOLE_FONT_INFO_EX newInfo = new();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TMPF_TRUETYPE;
                    Marshal.Copy(fontName.ToCharArray(), 0, new IntPtr(newInfo.FaceName), fontName.Length);

                    newInfo.dwFontSize = new COORD(FontSize, FontSize);
                    newInfo.FontWeight = info.FontWeight;
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Быки и коровы";
            SetColor(ConsoleColor.Black, ConsoleColor.White);
        }
        public static void SetColor(ConsoleColor BackgroundColor, ConsoleColor TextColor)
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
        }
    }
}
