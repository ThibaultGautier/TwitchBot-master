using System;

namespace TwitchBot
{
    public class ChatMessage : Message
    {
        public ChatMessage(string userName, ChatMessageType type, string channel, string text)
        {
            UserName = userName;
            Type = type;
            Channel = channel;
            Text = text;
        }

        public string UserName { get; private set; }
        
        public ChatMessageType Type { get; private set; }

        public string Channel { get; private set; }
        
        public string Text { get; private set; }

        public bool Contains(string value)
        {
            return Text.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        public new string ToString()
        {
            return string.Concat(":", UserName, "!", UserName, "@", UserName, ".tmi.twitch.tv ", Type, " #", Channel, " :", Text);
        }

        public static bool TryParse(string value, out Message message)
        {
            ChatMessageType? type = ExtractChatMessageType(value);
            if (!type.HasValue)
            {
                message = null;
                return false;
            }

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

            message = new ChatMessage(values[0].Substring(1, values[0].IndexOf("!") - 1), type.Value, values[2].TrimStart('#'), text);
            return true;
        }

        private static ChatMessageType? ExtractChatMessageType(string value)
        {
            foreach (ChatMessageType enumValue in Enum.GetValues(typeof(ChatMessageType)))
            {
                if (value.IndexOf(Convert.ToString(enumValue), StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return enumValue;
                }
            }

            return null;
        }



        public enum ChatMessageType
        {
            PRIVMSG,
            JOIN,
            PART
        }
    }
}