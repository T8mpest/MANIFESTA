using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MANIFESTA
{
    public class KeyboardStateManager
    {
        private ReplyKeyboardMarkup _currentKeyboard;

        public KeyboardStateManager()
        {
            Reset();
        }


        public void Reset()
        {
            _currentKeyboard = GetMainKeyboard();
        }

        public void ShowSubmenu()
        {
            _currentKeyboard = GetSubmenuKeyboard();
        }

        public ReplyKeyboardMarkup GetCurrentKeyboard()
        {
            return _currentKeyboard;
        }

        private ReplyKeyboardMarkup GetMainKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
            {
            new KeyboardButton("Наші послуги"),
            KeyboardButton.WithRequestContact("Залишити заявку☎️"),
        });
        }

        private ReplyKeyboardMarkup GetSubmenuKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
            {
            new KeyboardButton("Юр. послуги"),
            new KeyboardButton("Бух. послуги"),
        });
        }
    }
}
