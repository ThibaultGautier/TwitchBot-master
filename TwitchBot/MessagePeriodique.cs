using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchBot
{
    public static class MessagePeriodique
    {
        private static IrcClient irc;
        private static Timer timer;
        private const double ONEMIN = 60000;
         
        public static void initTimer(IrcClient _irc)
        {
            irc = _irc;
            timer = new Timer(ONEMIN);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           // irc.sendChatMessage("/me BIENVENUE SUR MA CHAINE !");
        }

        public static void stopTimer() {
            timer.Stop();
        }
    }
}
