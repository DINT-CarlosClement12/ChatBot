using ChatBot.Messagging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBot
{
    public partial class MainWindow : Window
    {

        private ObservableCollection<Message> messages;
        private bool isBotWritting = false;

        public MainWindow()
        {
            InitializeComponent();
            Task initializing = Bot.InitializeAsync();

            messages = new ObservableCollection<Message>();

            MessageContainerItemsControl.DataContext = messages;
            initializing.Wait();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            isBotWritting = true;
            string question = NewMessageTexBox.Text;
            messages.Add(new Message(Message.SenderType.User, question));

            Task<string> questionTask = Bot.MakeQuestion(question);
            questionTask.Wait();

            messages.Add(new Message(Message.SenderType.User, questionTask.Result));
        }

    }
}
