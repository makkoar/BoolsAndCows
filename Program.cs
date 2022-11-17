namespace BoolsAndCows
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            SetColor(ConsoleColor.Black, ConsoleColor.White);
        MainMenu:
            switch (MainMenu.GetIndex())
            {
                case 0: PlayToPC(); break;
                case 1: PlayToPl(); break;
            }
            goto MainMenu;
        }

        public static void PlayToPC()
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
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                Console.WriteLine(Replace("Компьютер загадал число, попытайтесь угадать число. Попытка {0}/{1}", i, NumberOfAttempts != -1 ? NumberOfAttempts : "∞"));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write("Ваше число: ");
                if (!int.TryParse(Console.ReadLine(), out int Attempt) || $"{Attempt}".Length != 4 || $"{Attempt}".Distinct().Count() != 4) goto EnteringANumber;
                SetColor(ConsoleColor.Black, ConsoleColor.White);
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
        }

        public static void PlayToPl()
        {
            _ = PlPlayMenu.GetIndex();
        StartGamePl:

            bool isFirstPlayer = true;
            List<string> PlNumber = new();
            while (PlNumber.Count < 2)
            {
            EnteringANumber:
                Console.Clear();
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                Console.WriteLine(Replace("Игрок {0}, введите своё число втайне от игрока {1}:", isFirstPlayer ? 1 : 2, isFirstPlayer ? 2 : 1));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write("Ваше число: ");
                if (!int.TryParse(Console.ReadLine(), out int CPlayer) || $"{CPlayer}".Length != 4 || $"{CPlayer}".Distinct().Count() != 4) goto EnteringANumber;
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                PlNumber.Add($"{CPlayer}");
                _ = ChangePlayerMenu.GetIndex(isFirstPlayer ? 1 : 2, isFirstPlayer ? 2 : 1);
                isFirstPlayer = !isFirstPlayer;
            }
            while (true)
            {
            EnteringANumber:
                Console.Clear();
                Console.WriteLine(Replace("Игрок {0}, попытайтесь угадать число соперника.", isFirstPlayer ? 1 : 2));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write("Ваше число: ");
                if (!int.TryParse(Console.ReadLine(), out int Attempt) || $"{Attempt}".Length != 4 || $"{Attempt}".Distinct().Count() != 4) goto EnteringANumber;
                SetColor(ConsoleColor.Black, ConsoleColor.White);
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
                _ = ChangePlayerMenu.GetIndex(isFirstPlayer ? 1 : 2, isFirstPlayer ? 2 : 1);
                isFirstPlayer = !isFirstPlayer;
            }
            if (ReplayMenu.GetIndex() == 1) goto StartGamePl;
        }

        public static string Replace(string Text, params object[] Texts)
        {
            string Temp = Text;
            for (int i = 0; i < Texts.Length; i++) Temp = Temp.Replace($"{{{i}}}", $"{Texts[i]}");
            return Temp;
        }
    }
}
