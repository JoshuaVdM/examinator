using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyticsApi
{
    public class TreeItem
    {
        public string Tag { get; set; }
        public List<TreeItem> Children { get; set; }
        public string Value { get; set; }

        public TreeItem()
        {
            Children = new List<TreeItem>();
        }

        public TreeItem(string sentence)
        {
            Children = new List<TreeItem>();
            Linguistics linguistics = new Linguistics();
            Task.Run(async () =>
            {
                List<string> d = await linguistics.analyze(sentence);

                foreach (string tree in d)
                {                    
                    Children.AddRange(GetChildren(tree));
                }

                Tag = "ROOT";

            }).GetAwaiter().GetResult();
        }

        public TreeItem(string tag, string value)
        {
            Tag = tag;
            Value = value;
            Children = new List<TreeItem>();
        }

        private List<TreeItem> GetChildren(string tree)
        {
            // (EX a) (EX b)
            List<TreeItem> items = new List<TreeItem>();
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
            return item.Substring(0, firstSpace).TrimStart('(');
        }

        private string GetValue(string item)
        {
            int firstSpace = item.IndexOf(" ");
            return item.Substring(firstSpace + 1);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Tag: {Tag}, value: {Value}\n");
            sb.Append($"Children: {Children.Count}");
            return sb.ToString();

        }
    }
}
