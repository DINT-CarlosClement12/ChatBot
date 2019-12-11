using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatBot.Commands
{
    public static class Chat
    {
        public static readonly RoutedUICommand Send = new RoutedUICommand("Send", "Send", typeof(Chat), new InputGestureCollection()
        {
            new  KeyGesture(Key.Enter)
        });
    }
}
