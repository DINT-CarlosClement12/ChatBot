using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChatBot.Converters
{
    class SenderTypeToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Messagging.Message.SenderType)value)
            {
                case Messagging.Message.SenderType.Bot:
                    return Properties.Settings.Default.BotMessagesColor;
                case Messagging.Message.SenderType.User:
                    return Properties.Settings.Default.UserMessagesColor;
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}