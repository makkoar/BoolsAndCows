namespace BoolsAndCows;

public class Program
{
    public static Menu MainMenu = new("Выберите противника:", new MenuItem("Компьютер", PlayToPC()), new("Игрок", PlayToPl()));
    public static Menu ReplayMenu = new("Выберите действие:", "Главное меню", "Начать заново");
    public static Menu PCPlayMenu = new("Вы выбрали режим боя с Компьютером.\n\nКраткие правила:\nКомпьютер загадывает 4-ех значное число, цифры в нем не повторяются. Игроку даётся ∞/15/10/7 попыток в зависимости от выбраной сложности, чотбы угадать число Компьютера, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число компьютера за 10 ходов.", "Начать игру");
    public static Menu PlPlayMenu = new("Вы выбрали режим боя с Игроком.\n\nКраткие правила:\nИгрок загадывает 4-ех значное число в закрытую от соперника, цифры в нем не должны повторяться, затем соперник делает тоже самое. Игроки пытаются отгадать число друг друга до тех пор, пока один из игроков не отгадает число соперника, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число соперника раньше него.", "Начать игру");
    public static Menu ChangePlayerMenu = new("Игрок {0}, передайте управление игроку {1}.", "Готово");
    public static Menu DifficultyMenu = new("Выберите уровень сложности:", "Без ограничений", "Лёгкий", "Нормальный", "Сложный");

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        
        
    MainMenu:
        MainMenu.Apply();
        goto MainMenu;
    }

    public static Action PlayToPC() => () =>
    {

        _ = PCPlayMenu.GetIndex();
    StartGamePC:
        Difficulty RDifficulty = (Difficulty)DifficultyMenu.GetIndex();
        sbyte NumberOfAttempts = RDifficulty switch { Difficulty.Easy => 15, Difficulty.Normal => 10, Difficulty.Hard => 7, _ => -1 };
        string PCNumber = string.Join("", Enumerable.Range(1, 9).OrderBy(x => random.Next()).Take(4));
        for (int i = 1; i <= NumberOfAttempts || NumberOfAttempts == -1; i++)
        {
        EnteringANumber:
            Console.Clear();
            Themes.Templates.Black.Apply();
            Console.WriteLine(Replace("Компьютер загадал число, попытайтесь угадать число. Попытка {0}/{1}", i, NumberOfAttempts != -1 ? NumberOfAttempts : "∞"));
            BlackGrayTheme.Apply();
            Console.Write("Ваше число: ");
            if (!int.TryParse(Console.ReadLine(), out int Attempt) || $"{Attempt}".Length != 4 || $"{Attempt}".Distinct().Count() != 4) goto EnteringANumber;
            Themes.Templates.Black.Apply();
            int Cows = 0;
            int Bulls = 0;
            foreach (char A in $"{Attempt}")
                foreach (char B in PCNumber)
                    if (A == B)
                        if ($"{Attempt}".IndexOf(A) == PCNumber.IndexOf(A)) Bulls++;
                        else Cows++;
            if (Bulls == 4) { Console.Write("\nВы победили!"); Console.ReadKey(); break; }
            else if (i == NumberOfAttempts) { Console.Write(Replace("\nВы проиграли! Число компьютера было {0}", PCNumber)); Console.ReadKey(); break; };
            Console.Write(Replace("\nКоровы: {0} | Быки: {1}", Cows, Bulls));
            if (Console.ReadKey().Key == ConsoleKey.Escape) return;
        }
        if (ReplayMenu.GetIndex() == 1) goto StartGamePC;
    };

    public static Action PlayToPl() => () =>
    {
        _ = PlPlayMenu.GetIndex();
    StartGamePl:

        bool isFirstPlayer = true;
        List<string> PlNumber = new();
        while (PlNumber.Count < 2)
        {
        EnteringANumber:
            Console.Clear();
            Themes.Templates.Black.Apply();
            Console.WriteLine(Replace("Игрок {0}, введите своё число втайне от игрока {1}:", isFirstPlayer ? 1 : 2, isFirstPlayer ? 2 : 1));
            BlackGrayTheme.Apply();
            Console.Write("Ваше число: ");
            if (!int.TryParse(Console.ReadLine(), out int CPlayer) || $"{CPlayer}".Length != 4 || $"{CPlayer}".Distinct().Count() != 4) goto EnteringANumber;
            Themes.Templates.Black.Apply();
            PlNumber.Add($"{CPlayer}");
            _ = ChangePlayerMenu.SetTitle($"Игрок {(isFirstPlayer ? 1 : 2)}, передайте управление игроку {(isFirstPlayer ? 2 : 1)}.").GetIndex();
            isFirstPlayer = !isFirstPlayer;
        }
        while (true)
        {
        EnteringANumber:
            Console.Clear();
            Themes.Templates.Black.Apply();
            Console.WriteLine(Replace("Игрок {0}, попытайтесь угадать число соперника.", isFirstPlayer ? 1 : 2));
            BlackGrayTheme.Apply();
            Console.Write("Ваше число: ");
            if (!int.TryParse(Console.ReadLine(), out int Attempt) || $"{Attempt}".Length != 4 || $"{Attempt}".Distinct().Count() != 4) goto EnteringANumber;
            Themes.Templates.Black.Apply();
            int Cows = 0;
            int Bulls = 0;
            foreach (char A in $"{Attempt}")
                foreach (char B in PlNumber[isFirstPlayer ? 1 : 0])
                    if (A == B)
                        if ($"{Attempt}".IndexOf(A) == PlNumber[isFirstPlayer ? 1 : 0].IndexOf(A)) Bulls++;
                        else Cows++;
            if (Bulls == 4) { Console.Write(Replace("\nИгрок {0} победил!", isFirstPlayer ? 1 : 2)); Console.ReadKey(); break; }
            else Console.Write(Replace("\nБыки: {0} | Коровы: {1}", Bulls, Cows));
            if (Console.ReadKey().Key == ConsoleKey.Escape) return;
            _ = ChangePlayerMenu.SetTitle($"Игрок {(isFirstPlayer ? 1 : 2)}, передайте управление игроку {(isFirstPlayer ? 2 : 1)}.").GetIndex();
            isFirstPlayer = !isFirstPlayer;
        }
        if (ReplayMenu.GetIndex() == 1) goto StartGamePl;
    };

        public static string Replace(string Text, params object[] Texts)
    {
        string Temp = Text;
        for (int i = 0; i < Texts.Length; i++) Temp = Temp.Replace($"{{{i}}}", $"{Texts[i]}");
        return Temp;
    }
}
