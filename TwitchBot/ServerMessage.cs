using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class ServerMessage : Message
    {
        public ServerMessage(int code, string userName, string text)
            : base()
        {
            Code = code;
            UserName = userName;
            Text = text;
        }

        public int Code { get; private set; }

        public string UserName { get; private set; }

        public string Text { get; private set; }

        public static bool TryParse(string value, out Message message)
        {
            string[] values = value.Split(' ');
            string text = null;
            for (int i = 3; i < values.Length; i++)
            {
                if (text == null)
                {
                    text = values[i];
                }
                else
                {
                    text = string.Concat(text, " ", values[i]);
                }
            }

            message = new ServerMessage(int.Parse(values[1]), values[2], text);
            return true;
        }
    }
}