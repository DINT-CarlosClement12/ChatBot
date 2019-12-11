using ChatBot.Messagging;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
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
        Bot bot;

        public MainWindow()
        {
            bot = new Bot();
            InitializeComponent();
            SendMessageButton.DataContext = bot.IsNotProcessing;

            messages = new ObservableCollection<Message>();

            MessageContainerItemsControl.DataContext = messages;
        }


        private async void Send_ExecutedAsync(object sender, ExecutedRoutedEventArgs e)
        {
            string question = NewMessageTexBox.Text;
            NewMessageTexBox.Text = string.Empty;
            messages.Add(new Message(Message.SenderType.User, question));

            try
            {
                await bot.MakeQuestion(question, messages);
            }
            catch (ErrorResponseException)
            {
                messages.Add(new Message(Message.SenderType.Bot, "Estoy muy cansado"));
                bot.IsNotProcessing = true;
            }
        }

        private void Send_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = bot.IsNotProcessing;
        }

        private async void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

    }
}
