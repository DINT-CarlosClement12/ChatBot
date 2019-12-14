using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Messagging
{
    class Message : INotifyPropertyChanged
    {
        public enum SenderType { Bot, User }

        public event PropertyChangedEventHandler PropertyChanged;

        public SenderType Sender { get; set; }
        private string Metadata { get; set; }
        private string content;
        public string Content
        {
            get { return content;  }
            set
            {
                content = value;
                Metadata = DateTime.Now.ToString();
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

        public override string ToString()
        {
            return $"{Metadata}:\n" +
                $"\t«{Content}»";
        }

    }
}
