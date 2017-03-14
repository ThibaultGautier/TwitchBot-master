using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    /**
     * Classe chargée de récupéré tous les mots ou items textuels interdits et de traiter les messages en conséquence
     * */

    class WordParser
    {
        private List<string> insultList;
        private List<string> linkTypeList;
        private StreamReader fileReader;

        public List<string> getInsultList()
        {
            return this.insultList;
        }

        public List<string> getLinkList()
        {
            return this.linkTypeList;
        }
        
        //Récupération de toutes les insultes du fichier contenant les mots interdits
        public void FillInsultsFromFile()
        {
            this.fileReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "annexes", "forbidden_words.txt"));
            this.insultList = new List<string>();
            int i = 0;
            string s;

            while ((s = fileReader.ReadLine()) != null)
            {
                string[] content = s.Split('=');
                if (content[0].Equals("forbid"))
                    insultList.Add(content[1]);
                i++;
            }
            fileReader.Close();
        }

        //Récupération de toutes les bouts de liens possibles du fichier contenant les mots interdits
        public void FillLinksFromFile()
        {
            this.fileReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "annexes", "forbidden_words.txt"));
            this.linkTypeList = new List<string>();
            int i = 0;
            string s;

            while ((s = fileReader.ReadLine()) != null)
            {
                string[] content = s.Split('=');
                if (content[0].Equals("link"))
                    linkTypeList.Add(content[1]);
                i++;
            }

            fileReader.Close();
        }

        public Boolean CheckForInsult(string message)
        {
            Boolean response = false;

            foreach (string s in this.insultList)
            {
                if (message.Equals(s))
                {
                    response = true;
                }
            }
            return response;
        }

        public Boolean CheckForLink(string message)
        {
            Console.WriteLine("message passé à la fonction : " + message.ToString());
            Boolean response = false;

            foreach (string s in this.linkTypeList)
            {
                if (message.Contains(s))
                {
                    response = true;
                }
            }
            return response;
        }
    }

}
