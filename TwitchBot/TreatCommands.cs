using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class TreatCommands
    {
        private static VoteFunctionality vote;
        private static Dictionary<string, int> listePropositions;
        private static bool startVote = false;

        private static bool IsCommand(string message)
        {
            if (message.StartsWith("!"))
            {
                Console.WriteLine("C'est une commande");
                return true;
            }
            else { return false; }
        }

        private static string AssignMessage(string message)
        {
            if (message.StartsWith("!vote"))
            {
                return "vote";
            }

            if (message.StartsWith("!endvote"))
            {
                return "endvote";
            }
            if (message.StartsWith("!modo"))
            {
                return "modo";
            }
            if (message.StartsWith("!unmodo"))
            {
                return "unmodo";
            }
            else return "Unknown";
        }

        private static bool IsVote(string message, TwitchBot bot)
        {
            listePropositions = vote.getPropositions();
            if (listePropositions.Keys.Contains(message))
            {
                bot.SendAdminMessage("added proposition");
                vote.setValProposition(message, bot);
                return true;
            }
            return false;
        }

        public static void TreatCommand(string message, TwitchBot bot)
        {
            if (IsCommand(message))
            {
                if (startVote)
                {
                    IsVote(message, bot);
                }

                string commande = AssignMessage(message);

                switch (commande)
                {
                    case "modo":
                        string user = message.Remove(0, 6);
                        IrcCommands commandeModo = new IrcCommands();
                        commandeModo.Op(bot.getChannel(), user);
                        bot.SendChanMessage(user + " est devenu modérateur !");
                        break;

                    case "unmodo":
                        String modo = message.Remove(0, 8);
                        IrcCommands supprimeModo = new IrcCommands();
                        supprimeModo.Deop(bot.getChannel(), modo);
                        bot.SendChanMessage(modo + " n'est plus modérateur !");
                        break;

                    case "vote":
                        startVote = true;
                        vote = new VoteFunctionality();
                        vote.StartVote(message.Remove(0, 6), bot);
                        break;

                    case "endvote":
                        vote.endVote(bot);
                        break;

                    case "Unknown":
                        break;
                }
            }
        }
    }
}
