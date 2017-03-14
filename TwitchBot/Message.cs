using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public abstract class Message
    {
        public Message()
        {
        }

        public static Message Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "The value must be specified.");
                //coucougr
            }

            Message message;
            if (ChatMessage.TryParse(value, out message))
            {
                return message as ChatMessage;
            }

            if (ServerMessage.TryParse(value, out message))
            {
                return message as ServerMessage;
            }

            throw new NotImplementedException(string.Concat("New Message Class To Implement For Value: ", value));
            
            ////if (!value.Contains("@"))
            ////{
            ////    throw new ArgumentException("The value is not an IRC message.", "value");
            ////}

            //string userName = "";
            //if (value.Contains("!"))
            //{
            //     userName = value.Substring(0, value.IndexOf("!"));
            //}
            //else
            //{
            //     userName = "";
            //}

            ////string substring = value.Substring(1, value.IndexOf(":"));
            ////string text = value.Substring(substring.Length);

            //return new Message(userName, value, "ducmirack");
        }
    }
}