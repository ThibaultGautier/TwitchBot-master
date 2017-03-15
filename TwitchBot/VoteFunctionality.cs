using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    /**
     * Classe gérant le démarrage, l'évolution et la clôture d'un vote.
     * */
    class VoteFunctionality
    {
        private string propositionAll = "";
        private Dictionary<string, int> propositions = new Dictionary<string, int>();
        private bool? isStartVoting = null;
        
        public Dictionary<string, int> getPropositions()
        {
            return this.propositions;
        }


        public void StartVote(string propositionsVote, TwitchBot bot)
        {
            if (propositionsVote != "")
            {
                //Récupère les choix du vote déclarés après la commande "!vote"
                string[] msgPropos = propositionsVote.Split(' ');
                string sendPropo = "Vote : ";
                foreach (string propo in msgPropos)
                {
                    sendPropo += "!" + propo + " ";
                    this.propositions.Add("!" + propo, 0);
                }

                bot.SendAdminMessage(sendPropo);
                this.isStartVoting = true;
            }
        }

        //Fonction ajoutant chaque nouveau vote à un dictionnaire
        public void setValProposition(string message, TwitchBot bot)
        {
            foreach (KeyValuePair<string, int> prop in this.propositions)
            {
                if (message.Contains(prop.Key))
                {
                    propositions[prop.Key] += 1;
                    break;
                }
            }
        }

        public void endVote(TwitchBot bot)
        {
            this.isStartVoting = false;
            bot.SendAdminMessage("Le vote est terminé !");
            string recapMsg = "/me RECAP : ";
            string gagnant = "";
            int maxVal = 0;
            foreach (KeyValuePair<string, int> prop in propositions)
            {
                if (prop.Value > maxVal)
                {
                    gagnant = "/me LE GAGNANT EST : " + prop.Key;
                }
                recapMsg += prop.Key + " : " + prop.Value.ToString() + ", ";
                maxVal = prop.Value;
            }
            this.propositions.Clear();
            bot.SendAdminMessage(recapMsg);
            bot.SendAdminMessage(gagnant);
        }



    }
}
