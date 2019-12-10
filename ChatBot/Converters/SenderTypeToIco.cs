using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChatBot.Converters
{
    class SenderTypeToIco : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Messagging.Message.SenderType)value)
            {
                case Messagging.Message.SenderType.Bot:
                    return "img/bot.ico";
                case Messagging.Message.SenderType.User:
                    return "img/user.ico";
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