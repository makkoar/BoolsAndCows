﻿namespace BoolsAndCows
{
    public static class Data
    {
        public static Random random = new();

        public static Menu MainMenu = new("Выберите противника:", "Компьютер", "Игрок");
        public static Menu ReplayMenu = new("Выберите действие:", "Главное меню", "Начать заново");
        public static Menu PCPlayMenu = new("Вы выбрали режим боя с Компьютером.\n\nКраткие правила:\nКомпьютер загадывает 4-ех значное число, цифры в нем не повторяются. Игроку даётся ∞/15/10/7 попыток в зависимости от выбраной сложности, чотбы угадать число Компьютера, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число компьютера за 10 ходов.", "Начать игру");
        public static Menu PlPlayMenu = new("Вы выбрали режим боя с Игроком.\n\nКраткие правила:\nИгрок загадывает 4-ех значное число в закрытую от соперника, цифры в нем не должны повторяться, затем соперник делает тоже самое. Игроки пытаются отгадать число друг друга до тех пор, пока один из игроков не отгадает число соперника, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число соперника раньше него.", "Начать игру");
        public static Menu ChangePlayerMenu = new("Игрок {0}, передайте управление игроку {1}.", "Готово");
        public static Menu DifficultyMenu = new("Выберите уровень сложности:", "Без ограничений", "Лёгкий", "Нормальный", "Сложный");
        public static Theme BlackGrayTheme = new(ConsoleColor.Black, ConsoleColor.DarkGray);

        public enum Difficulty : sbyte
        {
            NoRestrictions = 0,
            Easy = 1,
            Normal = 2,
            Hard = 3
        }
    }
}
