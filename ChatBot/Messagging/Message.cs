using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Messagging
{
    public class Message
    {
        public enum SenderType { Bot, User }

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
            }
        }

        public Message(SenderType sender, string content)
        {
            Sender = sender;
            Content = content;
        }

        public override string ToString()
        {
            return $"{Metadata}:\n" +
                $"\t«{Content}»";
        }

    }
}
