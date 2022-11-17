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
                if ((!int.TryParse(Console.ReadLine(), out int Attempt) || $"{Attempt}".Length != 4) || $"{Attempt}".Distinct().Count() != 4) goto EnteringANumber;
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

            List<string> PlNumber = new() { "----", "----" };
            for (byte CPlayer = 1; CPlayer <= 2; CPlayer++)
            {
            EnteringANumber:
                Console.Clear();
                Console.WriteLine(Replace("Игрок {0}, введите своё число втайне от игрока {1}:", CPlayer, CPlayer + (CPlayer == 1 ? 1 : -1)));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write("Ваше число: ");
                int.TryParse(Console.ReadLine(), out int _CPlayer);
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                if (_CPlayer.ToString().Length != 4) goto EnteringANumber;
                for (int i = 0; i < _CPlayer.ToString().Length; i++)
                    for (int _i = 0; _i < _CPlayer.ToString().Length; _i++)
                        if (i != _i && _CPlayer.ToString()[i] == _CPlayer.ToString()[_i]) goto EnteringANumber;
                PlNumber[CPlayer - 1] = _CPlayer.ToString();
                _ = ChangePlayerMenu.GetIndex(CPlayer == 1 ? 1 : 2, CPlayer == 1 ? 2 : 1);
            }
            bool isFirstPlayer = true;
            while (true)
            {
            EnteringANumber:
                Console.Clear();
                Console.WriteLine(Replace("Игрок {0}, попытайтесь угадать число соперника.", isFirstPlayer ? 1 : 2));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write("Ваше число: ");
                _ = int.TryParse(Console.ReadLine(), out int Attempt);
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                if (Attempt.ToString().Length != 4) goto EnteringANumber;
                for (int i = 0; i < Attempt.ToString().Length; i++)
                    for (int _i = 0; _i < Attempt.ToString().Length; _i++)
                        if (i != _i && Attempt.ToString()[i] == Attempt.ToString()[_i]) goto EnteringANumber;

                int Cows = 0;
                int Bulls = 0;
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if ($"{Attempt}"[j] == PlNumber[isFirstPlayer ? 1 : 0][k]) Cows++;
                        if ($"{Attempt}"[j] == PlNumber[isFirstPlayer ? 1 : 0][k] && j == k) Bulls++;
                    }
                Cows -= Bulls;
                Console.WriteLine((Bulls == 4) ? Replace("\nИгрок {0} победил!", isFirstPlayer ? 1 : 2) : Replace("\nБыки: {0} | Коровы: {1}", Bulls, Cows));
                Console.ReadKey();
                if (Bulls == 4) break;

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
