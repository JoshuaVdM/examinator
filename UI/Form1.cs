using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;

namespace UI
{
    public partial class Form1 : Form
    {
        public TreeItem root { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBox.Select();
        }

        private void LoadTree()
        {
            ItemTree.Nodes.Clear();
            ItemTree.BeginUpdate();
            LoadItem(root, ItemTree.Nodes);
            ItemTree.EndUpdate();
        }

        private void LoadItem(TreeItem item, TreeNodeCollection nodes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.Tag);
            if (!String.IsNullOrWhiteSpace(item.Value))
            {
                sb.Append($": {item.Value}");
            }

            nodes.Add(sb.ToString());

            for (int i = 0; i < item.Children.Count; i++)
            {
                LoadItem(item.Children[i], nodes[nodes.Count - 1].Nodes);
            }
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            root = new TreeItem();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                await root.LoadFromSentence(txtBox.Text);
                txtSubject.Text = root.Children[0].Children[0].NounPhrase.toReadableString();
                txtVerbPhrase.Text = root.Children[0].Children[0].VerbPhrase.toReadableString();
                QuestionAndAnswer qa = root.Children[0].Children[0].GetQuestionAndAnswer();
                txtQuestion.Text = qa.Question;
                txtAnswer.Text = qa.Answer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error analyzing text", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadTree();
            Cursor.Current = Cursors.Default;
            ItemTree.ExpandAll();
        }
    }
}
