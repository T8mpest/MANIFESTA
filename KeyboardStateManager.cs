using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;

namespace MANIFESTA
{
    public class KeyboardStateManager
    {
        private ReplyKeyboardMarkup _mainKeyboard;
        private ReplyKeyboardMarkup _submenuKeyboard;
        private bool _showSubmenu;

        public KeyboardStateManager()
        {
            InitializeKeyboards();
            Reset();
        }

        private void InitializeKeyboards()
        {
            _mainKeyboard = GetMainKeyboard();
            _submenuKeyboard = GetSubmenuKeyboard();
        }

        public void Reset()
        {
            _showSubmenu = false;
        }

        public void ShowSubmenu()
        {
            _showSubmenu = true;
        }

        public void HideSubmenu()
        {
            _showSubmenu = false;
        }

        public ReplyKeyboardMarkup GetCurrentKeyboard()
        {
            return _showSubmenu ? _submenuKeyboard : _mainKeyboard;
        }

        private ReplyKeyboardMarkup GetMainKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
    {
        new[]
        {
            new KeyboardButton("Наші послуги") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Залишити заявку☎️") { RequestContact = true },
            
        },
    });
        }

        private ReplyKeyboardMarkup GetSubmenuKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
     {
        new[]
        {
            new KeyboardButton("Бізнес Аналітика") { RequestContact = false },
            new KeyboardButton("Юр. послуги") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Бух. послуги") { RequestContact = false },
            new KeyboardButton("Управлінський облік") { RequestContact = false },
        },
        new[]
        {
            new KeyboardButton("Назад") { RequestContact = false },           
        },
    });
            
        }
    }
}
