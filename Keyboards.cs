using Telegram.Bot.Types.ReplyMarkups;

namespace MANIFESTA
{
    public static class Keyboards
    {
        // method for Get Main keyboard
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
        // Method to click on button for share the contact + button back in menu
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
        // Method with all info buttons + button back in menu
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
        // The method for get new button and share contact and we can handle it
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




