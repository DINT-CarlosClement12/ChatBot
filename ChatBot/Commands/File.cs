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

        public static readonly RoutedUICommand CheckConnection = new RoutedUICommand("CheckConnection", "CheckConnection", typeof(File), new InputGestureCollection()
        {
            new  KeyGesture(Key.Q, ModifierKeys.Control)
        });

        public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(File), new InputGestureCollection()
        {
            new  KeyGesture(Key.F4, ModifierKeys.Alt)
        });
    }
}
