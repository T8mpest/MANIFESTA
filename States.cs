using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANIFESTA
{
    public class BotState
    {
        public enum State
        {
            Idle,
            WaitingForContact,
            // Добавьте другие состояния, если необходимо
        }

        public State CurrentState { get; set; }
        public string PhoneNumber { get; set; } // Свойство для хранения номера телефона

        public BotState()
        {
            CurrentState = State.Idle;
        }
    }
}
