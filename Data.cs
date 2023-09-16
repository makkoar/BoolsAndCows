namespace BoolsAndCows;

/// <summary>Класс хранилище глобальных ресурсов.</summary>
public static class Data
{
    public static Random random = new();

    public static Theme BlackGrayTheme = new(ConsoleColor.Black, ConsoleColor.DarkGray);
}
