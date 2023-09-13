using Telegram.Bot.Types.ReplyMarkups;

namespace MANIFESTA
{
    public class KeyboardStateManager
    {
        private ReplyKeyboardMarkup _mainKeyboard;
        private ReplyKeyboardMarkup _submenuKeyboard;
        private ReplyKeyboardMarkup _ContactKeyboard;
        private bool _showSubmenu;
        private bool _showContactmenu;
        private BotState.State currentState;

        public KeyboardStateManager()
        {
            InitializeKeyboards();
            Reset();
        }

        private void InitializeKeyboards()
        {
            _mainKeyboard = GetMainKeyboard();
            _submenuKeyboard = GetSubmenuKeyboard();
            _ContactKeyboard = GetContactKeyboard();
        }

        public void Reset()
        {
            _showSubmenu = false;
        }

        public void ShowSubmenu()
        {
            _showSubmenu = true;
        }
        public void ShowContactmenu()
        {
            _showContactmenu = true;
        }
        public void HideContactMenu()
        {
            _showContactmenu = false;
        }

        public void HideSubmenu()
        {
            _showSubmenu = false;
        }

        public ReplyKeyboardMarkup GetCurrentKeyboard()
        {
            return _showSubmenu ? _submenuKeyboard : _mainKeyboard;
        }
        public ReplyKeyboardMarkup GetContactKeyboard2()
        {
            return _showContactmenu ? _ContactKeyboard : _mainKeyboard;
        }

        private ReplyKeyboardMarkup GetMainKeyboard()
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
    });
        }
        private ReplyKeyboardMarkup GetContactKeyboard()
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
            new KeyboardButton("Назад⏎"),
        },
            }); ;
        }
        private ReplyKeyboardMarkup GetSubmenuKeyboard()
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
            new KeyboardButton("Назад⏎"),
        },
    });

        }

    }
}
