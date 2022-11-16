﻿namespace BoolsAndCows
{
    public class Program
    {
        public unsafe static void Main()
        {
            Init();
        MainMenu:
            switch (MainMenu.GetIndex())
            {
                case 0: PlayToPC(); break;
                case 1: PlayToPl(); break;
            }
            goto MainMenu;
        }

        public unsafe static void PlayToPC()
        {
            _ = PCPlayMenu.GetIndex();
        StartGamePC:
            string PCNumber = "----";
            List<int> rndNum = Enumerable.Range(1, 9).OrderBy(x => random.Next()).Take(4).ToList();
            for (int i = 0; i < 4; i++)
                fixed (char* ptr = PCNumber)
                    ptr[i] = Convert.ToChar(rndNum[i].ToString());
            for (int i = 1; i <= 10; i++)
            {
            EnteringANumber:
                Console.Clear();
                Console.WriteLine(Text[12].Replace("{0}", $"{i}"));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write(Text[18]);
                int.TryParse(Console.ReadLine(), out int Attempt);
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                if (Attempt.ToString().Length != 4) goto EnteringANumber;
                for (int _i = 0; _i < Attempt.ToString().Length; _i++)
                    for (int __i = 0; __i < Attempt.ToString().Length; __i++)
                        if (_i != __i && Attempt.ToString()[_i] == Attempt.ToString()[__i]) goto EnteringANumber;
                int Cows = 0;
                int Bulls = 0;
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if ($"{Attempt}"[j] == PCNumber[k]) Cows++;
                        if ($"{Attempt}"[j] == PCNumber[k] && j == k) Bulls++;
                    }
                Cows -= Bulls;
                Console.WriteLine((i == 10) ? Text[8] : ((Bulls == 4) ? Text[7] : Text[6].Replace("{0}", $"{Bulls}").Replace("{1}", $"{Cows}")));
                Console.ReadKey();
                if (Bulls == 4) break;
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
                Console.WriteLine(Text[14].Replace("{0}", CPlayer.ToString()).Replace("{1}", (CPlayer + ((CPlayer == 1) ? 1 : -1)).ToString()));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write(Text[18]);
                int.TryParse(Console.ReadLine(), out int _CPlayer);
                SetColor(ConsoleColor.Black, ConsoleColor.White);
                if (_CPlayer.ToString().Length != 4) goto EnteringANumber;
                for (int i = 0; i < _CPlayer.ToString().Length; i++)
                    for (int _i = 0; _i < _CPlayer.ToString().Length; _i++)
                        if (i != _i && _CPlayer.ToString()[i] == _CPlayer.ToString()[_i]) goto EnteringANumber;
                PlNumber[CPlayer - 1] = _CPlayer.ToString();
                _ = ChangePlayerMenu.GetIndex(CPlayer == 1 ? "1" : "2", CPlayer == 1 ? "2" : "1");
            }
            bool isFirstPlayer = true;
            while (true)
            {
            EnteringANumber:
                Console.Clear();
                Console.WriteLine(Text[17].Replace("{0}", isFirstPlayer ? "1" : "2"));
                SetColor(ConsoleColor.Black, ConsoleColor.DarkGray);
                Console.Write(Text[18]);
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
                Console.WriteLine((Bulls == 4) ? Text[9].Replace("{0}", isFirstPlayer ? "1" : "2") : Text[6].Replace("{0}", $"{Bulls}").Replace("{1}", $"{Cows}"));
                Console.ReadKey();
                if (Bulls == 4) break;

                _ = ChangePlayerMenu.GetIndex(isFirstPlayer ? "1" : "2", isFirstPlayer ? "2" : "1");
                isFirstPlayer = !isFirstPlayer;
            }
            if (ReplayMenu.GetIndex() == 1) goto StartGamePl;
        }
    }
}
