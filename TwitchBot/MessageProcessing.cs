using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class MessageProcessing
    {
        /**
        * Classe chargée de la bonne gestion d'un message suivant sa nature :
        *  - Insulte ?
        *  - Flood utilisateur ?
        *  - Commande ?
        *  - Autre
        * */
        public static void ProcessReceivedMessage(string message, List<InputInfos> chatMessages, TwitchBot bot)
        {
            AntiInsultsFunctionnality.ProcessForbiddenMessages(message, chatMessages, bot);
            AntiFloodFunctionality.ProcessIsFloodUser(chatMessages, bot);
            TreatCommands.TreatCommand(message, bot);
        }
    }
}
