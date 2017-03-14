using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class AntiInsultsFunctionnality
    {
        public static void ProcessForbiddenMessages(string message, List<InputInfos> listInfos, TwitchBot bot)
        {
            WordParser messageParser = new WordParser();
            messageParser.FillInsultsFromFile();
            messageParser.FillLinksFromFile();
            InputInfos userToTimeOutInfos = listInfos[listInfos.Count - 1];
            if ((messageParser.CheckForInsult(message)) || (messageParser.CheckForLink(message)))
            {
                bot.TimeOutUser(userToTimeOutInfos.UserName);
            }
        }

    }
}
