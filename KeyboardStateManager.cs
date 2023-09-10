using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using System.Net.Http.Headers;

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

        public void SetCurrentState(BotState.State state)
        {
            currentState = state;
        }

        public BotState.State GetCurrentState()
        {
            return currentState;
        }

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
            new KeyboardButton("Наші послуги🐣") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Залишити заявку☎️") { RequestContact = false },

        },
    });
        }
        private ReplyKeyboardMarkup GetContactKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
            {
 new[]
        {
            new KeyboardButton("Аналітика🏅") { RequestContact = true },
            new KeyboardButton("Юридичні🔮") { RequestContact = true },
        },
        new[]
        {
            new KeyboardButton("Бухгалтерські🌸") { RequestContact = true },
            new KeyboardButton("Управлінські🐍") { RequestContact = true },
        },
        new[]
        {
            new KeyboardButton("Назад⏎") { RequestContact = false },
        },
            });;
        }
        private ReplyKeyboardMarkup GetSubmenuKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
     {
        new[]
        {
            new KeyboardButton("Бізнес Аналітика🏅") { RequestContact = false },
            new KeyboardButton("Юридичні.послуги🔮") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Бух.Послуги🌸") { RequestContact = false },
            new KeyboardButton("Управлінський облік🐍") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Назад⏎") { RequestContact = false },
        },
    });

        }
    }
}
