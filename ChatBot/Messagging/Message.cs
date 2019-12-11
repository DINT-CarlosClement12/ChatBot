using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Messagging
{
    class Message
    {

        public enum SenderType { Bot, User }

        public event PropertyChangedEventHandler PropertyChanged;

        public SenderType Sender { get; set; }
        private string content;
        public string Content
        {
            get { return content;  }
            set
            {
                content = value;
                //NotifyPropertyChanged("Content");
            }
        }

        public Message(SenderType sender, string content)
        {
            Sender = sender;
            Content = content;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
