using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Messagging
{
    class Bot : INotifyPropertyChanged
    {

        const string DEFAULT_NOT_FOUND_ANSWER = "No good match found in KB.";

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly QnAMakerRuntimeClient cliente;

        private bool isNotProcessing;
        public bool IsNotProcessing
        {
            get { return isNotProcessing; }
            set
            {
                isNotProcessing = value;
                OnPropertyChanged("IsNotProcessing");
            }
        }

        public Bot()
        {
            cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Properties.Settings.Default.AzureBotKey))
            {
                RuntimeEndpoint = Properties.Settings.Default.AzureBotEndPoint
            };
            IsNotProcessing = true;
        }

        public async Task MakeQuestion(string question, ObservableCollection<Message> messages)
        {
            IsNotProcessing = false;
            QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(Properties.Settings.Default.AzureBotId, new QueryDTO { Question = question });

            string responseString = response.Answers[0].Answer;

            messages.Add(new Message(Message.SenderType.Bot, responseString == DEFAULT_NOT_FOUND_ANSWER ? "¿Que si quiero o que si tengo?" : responseString));
            IsNotProcessing = true;
        }

        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                await cliente.Runtime.GenerateAnswerAsync(Properties.Settings.Default.AzureBotId, new QueryDTO { Question = "Test" });
            }
            catch(Exception)
            {
                return false;
            }
            return true;   
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
