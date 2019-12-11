using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatBot.Commands
{
    public static class File
    {
        public static readonly RoutedUICommand Settings = new RoutedUICommand("Settings", "Settings", typeof(File), new InputGestureCollection()
        {
            new  KeyGesture(Key.S, ModifierKeys.Alt)
        });
    }
}
