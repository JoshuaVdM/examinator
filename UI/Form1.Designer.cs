namespace UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ItemTree = new System.Windows.Forms.TreeView();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemTree
            // 
            this.ItemTree.Location = new System.Drawing.Point(583, 12);
            this.ItemTree.Name = "ItemTree";
            this.ItemTree.Size = new System.Drawing.Size(549, 608);
            this.ItemTree.TabIndex = 0;
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(13, 45);
            this.txtBox.Multiline = true;
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(564, 575);
            this.txtBox.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 17);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(156, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Write or paste a text to analyze!";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(502, 12);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 632);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.ItemTree);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ItemTree;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnAnalyze;
    }
}

