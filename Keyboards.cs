using Telegram.Bot.Types.ReplyMarkups;

namespace MANIFESTA
{
    public static class Keyboards
    {
        public static ReplyKeyboardMarkup GetMainKeyboard
        {
            get
            {
                return new ReplyKeyboardMarkup(new[]
                {
                new[]
                {
                    new KeyboardButton("Наші послуги🐣"),
                },
                new[]
                {
                    new KeyboardButton("Залишити заявку☎️"),
                },
            })
                {
                    ResizeKeyboard = true
                };
            }
        }

        public static ReplyKeyboardMarkup GetProductsKeyboard
        {
            get
            {
                return new ReplyKeyboardMarkup(new[]
                {
                new[]
                {
                    new KeyboardButton("Аналітика🏅"),
                    new KeyboardButton("Юридичні🔮"),
                },
                new[]
                {
                    new KeyboardButton("Бухгалтерські🌸"),
                    new KeyboardButton("Управлінські🐍"),
                },
                new[]
                {
                    new KeyboardButton("В меню⏎"),
                },
            })
                {
                    ResizeKeyboard = true
                };
            }
        }

        public static ReplyKeyboardMarkup GetSubmenuKeyboard
        {
            get
            {
                return new ReplyKeyboardMarkup(new[]
                {
                new[]
                {
                    new KeyboardButton("Бізнес Аналітика🏅"),
                    new KeyboardButton("Юридичні.послуги🔮"),
                },
                new[]
                {
                    new KeyboardButton("Бух.Послуги🌸"),
                    new KeyboardButton("Управлінський облік🐍"),
                },
                new[]
                {
                    new KeyboardButton("В меню⏎"),
                },
            })
                {
                    ResizeKeyboard = true
                };
            }
        }

        public static ReplyKeyboardMarkup GetContactKeyboard
        {
            get
            {
                return new ReplyKeyboardMarkup(new[]
               {
                new[]
                {
                    new KeyboardButton("Поділитися номером телефону") { RequestContact = true },                
                },
                new[]
                {
                    new KeyboardButton("В меню⏎"),
                },
            })
                {
                    ResizeKeyboard = true
                };
            }
        }


    }
}




