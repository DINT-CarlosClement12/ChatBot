﻿using ChatBot.Messagging;
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

namespace ChatBot
{
    public partial class MainWindow : Window
    {

        private ObservableCollection<Message> messages;
        Bot bot;
        bool isCheckingConnection = false;

        public MainWindow()
        {
            bot = new Bot();
            messages = new ObservableCollection<Message>();

            InitializeComponent();
            SendMessageButton.DataContext = bot.IsNotProcessing;

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
            e.CanExecute = bot.IsNotProcessing && NewMessageTexBox.Text != string.Empty;
        }

        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.ShowDialog();
            //Owner = settingsWindow;
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
                    {
                        file.WriteLine(message);
                    }
            
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
    }
}
