using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Classe principale représentative du chat
 **/
 
namespace TwitchBot
{
    class TwitchBot
    {
        /**
         * /!\ Membres importants /!\
         * listInputInfos : contient des informations utilisateurs récupérées lors d'un post de message sur le chat
         * irc : Notre objet principale permettant la connexion au chat et diverses opérations dessus
         * */

        private List<InputInfos> listInputInfos = new List<InputInfos>();
        private IrcClient irc = new IrcClient();
        private string login = ConfigurationManager.AppSettings["Login"];
        private string server = "irc.chat.twitch.tv";
        private int port = 6667;
        private string channel = "#narxhen";

        static void Main(string[] args)
        {
            TwitchBot bot = new TwitchBot();
        }

        public TwitchBot()
        {
            // Ajout des différents handlers au client irc
            irc.OnConnected += new EventHandler(OnConnected);
            irc.OnPing += new PingEventHandler(OnPing);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
            irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
            irc.OnJoin += new JoinEventHandler(OnJoin);
            irc.OnChannelActiveSynced += new IrcEventHandler(OnChannelActiveSynced);

            try
            {
                irc.Connect(server, port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect: n" + e.Message);
                Console.ReadKey();
            }
        }

        public string getChannel()
        {
            return this.channel;
        }

        /**
         *  Handlers ajoutés au client IRC qui seront appelés à différentes étapes du programme
         * */

        void OnConnected(object sender, EventArgs e)
        {
            irc.Login(login, "Thibault Gautier", 0, login, "oauth:0c2wgm1gubio0ijhfykp0tn5h3rzji");
            irc.RfcJoin(channel);
            //Boucle recevant incessamment les actions du chat
            irc.Listen();
        }

        //Handlers chargé d'effectuer une action à chaque fois que twitch vérifie que nous sommes bien connectés
        void OnPing(object sender, PingEventArgs e)
        {
            SendAdminMessage("Responded to ping at {0}." + DateTime.Now.ToShortTimeString());
        }

        //Actions à effectuer par le programme lors de la reception d'un message
        void OnChannelMessage(object sender, IrcEventArgs e)
        {   
            //Permet de stocker des infos à chaque nouveau message
            InputInfos infosMessage = new InputInfos(e.Data.Nick);
            listInputInfos.Add(infosMessage);
            //traitement du message
            MessageProcessing.ProcessReceivedMessage(e.Data.Message, listInputInfos, this);
        }

        // Gestion de messages privés
        void OnQueryMessage(object sender, IrcEventArgs e)
        {
            SendAdminMessage("Nouveau(x) message(s) non lu(s)");
        }

        //Actions à effectuer lors de la connexion au chat
        void OnJoin(object sender, IrcEventArgs e)
        {
            irc.SendMessage(SendType.Message, "#narxhen", "Bienvenue sur le chat");
        }

        //Permet d'attendre la synchronisation entre l'utilisateur et le chat
        void OnChannelActiveSynced(object sender, IrcEventArgs e)
        {
            //irc.SendMessage(SendType.Message, "#narxhen", "Linked");
        }

        public void TimeOutUser(string nick)
        {
            //irc.SendMessage(SendType.Action, channel, "tententive de timeout de "+nick);
            irc.SendMessage(SendType.Action, channel, "/timeout " + nick + " 200");
        }

        public void SendAdminMessage(string messageVote)
        {
            irc.SendMessage(SendType.Message, channel, messageVote);
        }

        public void SendChanMessage(string messageVote)
        {
            irc.SendMessage(SendType.Action, channel, messageVote);
        }
    }
}
