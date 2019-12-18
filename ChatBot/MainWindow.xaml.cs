using ChatBot.Messagging;
using ChatBot.Windows;
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
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Net.Http;

namespace ChatBot
{
    public partial class MainWindow : Window
    {

        private const string ERROR_RESPONSE_MESSAGE = "Estoy muy cansado";
        private const string ERROR_HTTP_REQUEST_MESSAGE = "No me encuentro bien señor Stark";

        private ObservableCollection<Message> messages;
        Bot bot;
        bool isCheckingConnection = false;
        bool canScrollToEnd = false;    

        public MainWindow()
        {
            bot = new Bot();
            messages = new ObservableCollection<Message>();

            InitializeComponent();

            SendMessageButton.DataContext = bot.IsNotProcessing;
            MessageContainerItemsControl.DataContext = messages;
        }

        // To scroll to the bottom when new item is added to the list
        private void MessagesScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (canScrollToEnd)     // If new item is being added to the list. If it false means that user is scrolling manually
            {
                MessagesScrollViewer.ScrollToEnd();
                canScrollToEnd = false;     // The message is no longer being added
            }
        }

        #region COMMANDS

        private async void Send_ExecutedAsync(object sender, ExecutedRoutedEventArgs e)
        {
            string question = NewMessageTexBox.Text;
            NewMessageTexBox.Text = string.Empty;
            messages.Add(new Message(Message.SenderType.User, question));

            try
            {
                canScrollToEnd = true;      // The message is being added
                await bot.MakeQuestion(question, messages);
            }
            catch (ErrorResponseException)
            {
                messages.Add(new Message(Message.SenderType.Bot, ERROR_RESPONSE_MESSAGE));
            }
            catch (HttpRequestException)
            {
                messages.Add(new Message(Message.SenderType.Bot, ERROR_HTTP_REQUEST_MESSAGE));
            }
            finally
            {
                bot.IsNotProcessing = true;     // Whatever happens, we have to say that the bot is not processing anything
            }
        }

        private void Send_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = bot.IsNotProcessing && NewMessageTexBox.Text != string.Empty;
        }

        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.ShowDialog();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommonSaveFileDialog folderSelectorDialog = new CommonSaveFileDialog
            {
                EnsureReadOnly = true,
                DefaultExtension = ".txt",
                DefaultFileName = $"ChatBot - {DateTime.Now.ToString().Replace('/', '-').Replace(':', '_')}",
                AlwaysAppendDefaultExtension = true,
                Title = "Guardar como..."
            };
            folderSelectorDialog.Filters.Add(new CommonFileDialogFilter("Documento de texto", "*.txt"));

            if(folderSelectorDialog.ShowDialog() == CommonFileDialogResult.Ok)
                foreach(Message message in messages)
                    using (StreamWriter file = new StreamWriter(folderSelectorDialog.FileName, true))
                        file.WriteLine(message);
            
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = messages.Count > 0;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            messages.Clear();
        }

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = messages.Count > 0;
        }

        private async void CheckConnection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isCheckingConnection = true;
            bool checkResult = await bot.CheckConnectionAsync();

            MessageBox.Show($"{(checkResult ? "Ok" : "Sin conexión")}", "Prueba de conexión", MessageBoxButton.OK, 
                checkResult ? MessageBoxImage.Information : MessageBoxImage.Error, MessageBoxResult.OK);

            isCheckingConnection = false;
        }

        private void CheckConnection_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isCheckingConnection;
        }

        private void FocusTextBox_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewMessageTexBox.Focus();
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        #endregion COMMANDS

    }
}
