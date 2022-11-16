﻿namespace BoolsAndCows
{
    public static class Data
    {
        public const byte FontSize = 20;
        public static Random random = new();

        public static Menu MainMenu = new("Выберите противника:", "Компьютер", "Игрок");
        public static Menu ReplayMenu = new("Выберите действие:", "Главное меню", "Начать заново");


        public static List<string> Text = new()
        {
            "* Главное меню",
            "* Готово",
            null,
            null,
            "* Начать заново",
            "* Начать игру",
            "\nБыки: {0} | Коровы: {1}",
            "\nВы победили!",
            "\nВы проиграли!",
            "\nИгрок {0} победил!",
            "Выберите действие:",
            null,
            "Компьютер загадал число, попытайтесь угадать число. Попытка {0}/10",
            "Вы выбрали режим боя с Игроком.\n\nКраткие правила:\nИгрок загадывает 4-ех значное число в закрытую от соперника, цифры в нем не должны повторяться, затем соперник делает тоже самое. Игроки пытаются отгадать число друг друга до тех пор, пока один из игроков не отгадает число соперника, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число соперника раньше него.\n",
            "Игрок {0}, введите своё число втайне от игрока {1}:",
            "Игрок {0}, передайте управление игроку {1}.\n",
            "Вы выбрали режим боя с Ботом.\n\nКраткие правила:\nБот загадывает 4-ех значное число, цифры в нем не повторяются. Игроку даётся 10 попыток, чотбы угадать число бота, где попытка — это 4-значное число с неповторяющимися цифрами, сообщаемое компьютеру.\nКомпьютер сообщает в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров) и сколько угадано вплоть до позиции в тайном числе (то есть количество быков), например:\n* Задумано тайное число «3219».\n* Попытка: «2310».\n* Результат: две «коровы» (две цифры: «2» и «3» — угаданы на неверных позициях) и один «бык» (одна цифра «1» угадана вплоть до позиции).\nИгрок побеждает, если угадает число компьютера за 10 ходов.\n",
            "Игрок {0}, попытайтесь угадать число соперника.",
            "Ваше число: "
        };
    }
}
