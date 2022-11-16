namespace BoolsAndCows
{
    public class Program
    {
        public unsafe static void Main()
        {
            Init();
            uint Selector = 1;
        MainMenu:
            switch (MainMenu.GetIndex())
            {
                case 0: Selector = 1; break; /*goto StartGamePC;*/
                case 1: Selector = 2; break; /*goto MainMenu;*/
                default:
                    break;
            }
        
            switch (Selector)
            {
                case 1:
                StartGamePC:
                    Console.Clear();
                    Console.WriteLine(Text[16]);
                    SetColor(ConsoleColor.Green, ConsoleColor.Black);
                    Console.WriteLine(Text[5].PadRight(64));
                    SetColor(ConsoleColor.Black, ConsoleColor.White);
                    Console.ReadKey();
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
                    switch (ReplayMenu.GetIndex())
                    {
                        case 0: goto MainMenu;
                        case 1:  goto StartGamePC;
                        default:
                            break;
                    }
                    break;
                case 2:
                StartGamePl:
                    Console.Clear();
                    Console.WriteLine(Text[13]);
                    SetColor(ConsoleColor.Green, ConsoleColor.Black);
                    Console.WriteLine(Text[5].PadRight(64));
                    SetColor(ConsoleColor.Black, ConsoleColor.White);
                    Console.ReadKey();
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
                        ChangePlayer(CPlayer == 1);
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
                        ChangePlayer(isFirstPlayer);
                        isFirstPlayer = !isFirstPlayer;
                    }
                    switch (ReplayMenu.GetIndex())
                    {
                        case 0: goto MainMenu;
                        case 1: goto StartGamePl;
                        default:
                            break;
                    }
                    break;
            }
            goto MainMenu;
        }

        public static void PlayToPC()
        {

        }

        public static void ChangePlayer(bool isFirstPlayer)
        {
            Console.Clear();
            Console.WriteLine(Text[15].Replace("{0}", isFirstPlayer ? "1" : "2").Replace("{1}", isFirstPlayer ? "2" : "1"));
            SetColor(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine(Text[1].PadRight(64));
            SetColor(ConsoleColor.Black, ConsoleColor.White);
            Console.ReadKey();
        }
    }
}
