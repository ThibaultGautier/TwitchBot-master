using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class AntiFloodFunctionality
    {
        public static void ProcessIsFloodUser(List<InputInfos> chatMessages, TwitchBot bot)
        {
            if (isFlooderUser(chatMessages))
            {
                InputInfos userToTimeOutInfos = chatMessages[chatMessages.Count - 1];
                bot.TimeOutUser(userToTimeOutInfos.UserName);
            }
        }

        public static Boolean isFlooderUser(List<InputInfos> listeInfosMessages)
        {
            int countMessages = 0;

            //Vérifie si un grand nombre de messages ont été postés dans un interval de 10sec
            for (int i = listeInfosMessages.Count - 1; i >= 0; i--)
            {
                if ((DateTime.Now - listeInfosMessages[i].TimePost).TotalSeconds > 10)
                {
                    listeInfosMessages.Remove(listeInfosMessages[i]);
                    continue;
                }

                //Vérifie si cun utilisateur est responsable du trop grand nombre de messages
                string user = listeInfosMessages[i].UserName;

                string last_user = listeInfosMessages[listeInfosMessages.Count-1].UserName;
                if (String.Equals(user, last_user, StringComparison.InvariantCultureIgnoreCase))
                {
                    countMessages++;
                    if (countMessages >= 5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
