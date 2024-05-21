namespace appFrench
{
    partial class FormTranslate
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
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.labelTranslete = new System.Windows.Forms.Label();
            this.buttonTraslateEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxWord
            // 
            this.textBoxWord.Location = new System.Drawing.Point(134, 195);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(100, 20);
            this.textBoxWord.TabIndex = 0;
            // 
            // labelTranslete
            // 
            this.labelTranslete.AutoSize = true;
            this.labelTranslete.Location = new System.Drawing.Point(164, 102);
            this.labelTranslete.Name = "labelTranslete";
            this.labelTranslete.Size = new System.Drawing.Size(35, 13);
            this.labelTranslete.TabIndex = 1;
            this.labelTranslete.Text = "label1";
            // 
            // buttonTraslateEnter
            // 
            this.buttonTraslateEnter.Location = new System.Drawing.Point(147, 368);
            this.buttonTraslateEnter.Name = "buttonTraslateEnter";
            this.buttonTraslateEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonTraslateEnter.TabIndex = 2;
            this.buttonTraslateEnter.Text = "button1";
            this.buttonTraslateEnter.UseVisualStyleBackColor = true;
            this.buttonTraslateEnter.Click += new System.EventHandler(this.buttonTraslateEnter_ClickAsync);
            // 
            // FormTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 449);
            this.Controls.Add(this.buttonTraslateEnter);
            this.Controls.Add(this.labelTranslete);
            this.Controls.Add(this.textBoxWord);
            this.Name = "FormTranslate";
            this.Text = "FormTranslate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.Label labelTranslete;
        private System.Windows.Forms.Button buttonTraslateEnter;
    }
}