using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class InputInfos
    {
        /**
         * Classe servant à obtenir un log des différents messages pour faciliter certaines opérations
         * De nombreuses options pourront être ajoutées
         * */
        public string UserName { get; private set; }
        public DateTime TimePost { get; private set; }  

        public InputInfos(string _userName)
        {
            this.UserName = _userName;
            this.TimePost = DateTime.Now;
        }
    }
}
