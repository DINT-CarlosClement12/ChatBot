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
            string EndPoint = "https://botisma.cognitiveservices.azure.com/qnamaker/v4.0";
            string Key = "369cda7a9a29495eb647a8c8faf5df61";
            cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Key)) { RuntimeEndpoint = EndPoint };
            IsNotProcessing = true;
        }

        public async Task MakeQuestion(string question, ObservableCollection<Message> messages)
        {
            IsNotProcessing = false;
            string id = "e3c48047-80d3-4450-9231-ed4d0985840d";
            QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(id, new QueryDTO { Question = question });
            
            messages.Add(new Message(Message.SenderType.User, response.Answers[0].Answer));
            IsNotProcessing = true;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
