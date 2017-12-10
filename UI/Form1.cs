﻿using System;
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
                LoadItem(item.Children[i], nodes[nodes.Count -1].Nodes);
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            root = new TreeItem(txtBox.Text);
            LoadTree();
        }
    }
}