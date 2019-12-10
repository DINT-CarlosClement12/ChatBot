using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Messagging
{
    static class Bot
    {

        private static QnAMakerRuntimeClient cliente;

        public static async Task InitializeAsync()
        {
            string EndPoint = "https://botisma.cognitiveservices.azure.com/qnamaker/v4.0";
            string Key = "369cda7a9a29495eb647a8c8faf5df61";
            cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(Key)) { RuntimeEndpoint = EndPoint };

        }

        public static async Task<string> MakeQuestion(string question)
        {
            string id = "e3c48047-80d3-4450-9231-ed4d0985840d";
            QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(id, new QueryDTO { Question = question });

            return response.Answers[0].Answer;
        }

    }
}
