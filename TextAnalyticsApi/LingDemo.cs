using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyticsApi
{
    class LingDemo
    {
        public string Text { get; set; }
        public dynamic Response { get; set; }
        public Linguistics Ling { get; set; }

        public LingDemo()
        {
            Ling = new Linguistics();
        }

        public void Start()
        {
            //Console.WriteLine("LAnalyzer 1.0");
            //Console.WriteLine("----------------------------");
            //Char quit;
            //do
            //{
            //    EnterSentence();
            //    GetResponse();
            //    ShowResponse();
            //    ShowQuitMessage();
            //    quit = Console.ReadKey().KeyChar;
            //}
            //while (quit != 'q');

            TreeItem item = new TreeItem("I am a man.");
            Console.Write(item.Tag);
            //TreeItem root = ParseTreeItem("EX (EX (EX a))");
            //Console.Write(root.Tag + "-" + root.Value);

            //List<TreeItem> children = GetChildren("(TOP (S (NP (PRP I)) (VP (VBP am) (NP (DT a) (NN man))) (. .)))");
            //TreeItem root = new TreeItem();
            //root.Tag = "ROOT";
            //root.Children = children;

            //Console.Write(root.Children[0].Children[0].Children[1].Children[1].Children[0].ToString());
        }

        private void ShowQuitMessage()
        {
            Console.WriteLine("press 'q' to exit");
        }

        private void ShowResponse()
        {
            Console.Write(Response);
        }

        private void GetResponse()
        {
            Task.Run(async () =>
            {
                Response = await Ling.GetTree(Text);
            }).GetAwaiter().GetResult();
        }

        private void EnterSentence()
        {
            Console.WriteLine("Enter a sentence to analyze:");
            Text = Console.ReadLine();
        }


        private List<TreeItem> GetChildren(string tree)
        {
            // (EX a) (EX b)
            List<TreeItem> items = new List<TreeItem> ();            
            List<string> children = new List<string>();
            int openBrackets = 0;
            int openBracketIndex = -1;
            int closedBrackets = 0;
            for (int i = 0; i < tree.Length; i++)
            {
                Char currentChar = tree[i];
                if (currentChar == '(')
                {
                    if (openBracketIndex == -1)
                    {
                        openBracketIndex = i; //3
                    }
                    openBrackets++;
                }
                else if (currentChar == ')')
                {
                    closedBrackets++;
                }

                if (openBrackets == closedBrackets && openBrackets + closedBrackets > 0)
                {
                    children.Add(tree.Substring(openBracketIndex + 1, i - openBracketIndex - 1));
                    openBracketIndex = -1;
                    openBrackets = 0;
                    closedBrackets = 0;
                }
            }   

            foreach (string s in children)
            {
                TreeItem i = new TreeItem();
                i.Tag = GetTag(s); 

                List<TreeItem> gchildren = GetChildren(s);
                if (gchildren.Count == 0)
                {
                    i.Value = GetValue(s);                
                }
                else
                {
                    i.Children = gchildren;
                }
                items.Add(i);
            }
            return items;
        }
        private string GetTag(string item)
        {
            int firstSpace = item.IndexOf(" ");
            return item.Substring(0, firstSpace);
        }

        private string GetValue(string item)
        {
            int firstSpace = item.IndexOf(" ");
            return item.Substring(firstSpace + 1);
        }
    }
}
