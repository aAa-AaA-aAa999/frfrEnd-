namespace appFrench
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonProfile = new System.Windows.Forms.Button();
            this.buttonLearnSlowed = new System.Windows.Forms.Button();
            this.buttonLearnSpeed = new System.Windows.Forms.Button();
            this.buttonTopArray = new System.Windows.Forms.Button();
            this.buttonListWord = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.labelForPhrase = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonProfile
            // 
            this.buttonProfile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonProfile.BackgroundImage")));
            this.buttonProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonProfile.FlatAppearance.BorderSize = 0;
            this.buttonProfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(133)))), ((int)(((byte)(255)))));
            this.buttonProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(176)))), ((int)(((byte)(255)))));
            this.buttonProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProfile.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.buttonProfile.Location = new System.Drawing.Point(358, 14);
            this.buttonProfile.Margin = new System.Windows.Forms.Padding(5);
            this.buttonProfile.Name = "buttonProfile";
            this.buttonProfile.Size = new System.Drawing.Size(99, 95);
            this.buttonProfile.TabIndex = 0;
            this.buttonProfile.Text = "Профиль";
            this.buttonProfile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonProfile.UseVisualStyleBackColor = true;
            this.buttonProfile.Click += new System.EventHandler(this.buttonProfile_Click);
            // 
            // buttonLearnSlowed
            // 
            this.buttonLearnSlowed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLearnSlowed.BackgroundImage")));
            this.buttonLearnSlowed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLearnSlowed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLearnSlowed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonLearnSlowed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.buttonLearnSlowed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLearnSlowed.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonLearnSlowed.Location = new System.Drawing.Point(254, 266);
            this.buttonLearnSlowed.Margin = new System.Windows.Forms.Padding(5);
            this.buttonLearnSlowed.Name = "buttonLearnSlowed";
            this.buttonLearnSlowed.Size = new System.Drawing.Size(203, 78);
            this.buttonLearnSlowed.TabIndex = 1;
            this.buttonLearnSlowed.Text = "Постепенное изучение";
            this.buttonLearnSlowed.UseVisualStyleBackColor = true;
            this.buttonLearnSlowed.Click += new System.EventHandler(this.buttonLearnSlowed_Click);
            // 
            // buttonLearnSpeed
            // 
            this.buttonLearnSpeed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLearnSpeed.BackgroundImage")));
            this.buttonLearnSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLearnSpeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLearnSpeed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonLearnSpeed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.buttonLearnSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLearnSpeed.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonLearnSpeed.Location = new System.Drawing.Point(254, 178);
            this.buttonLearnSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.buttonLearnSpeed.Name = "buttonLearnSpeed";
            this.buttonLearnSpeed.Size = new System.Drawing.Size(203, 78);
            this.buttonLearnSpeed.TabIndex = 2;
            this.buttonLearnSpeed.Text = "Быстрая игра";
            this.buttonLearnSpeed.UseVisualStyleBackColor = true;
            this.buttonLearnSpeed.Click += new System.EventHandler(this.buttonLearnSpeed_Click);
            // 
            // buttonTopArray
            // 
            this.buttonTopArray.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTopArray.FlatAppearance.BorderSize = 0;
            this.buttonTopArray.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonTopArray.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonTopArray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTopArray.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonTopArray.Location = new System.Drawing.Point(286, 358);
            this.buttonTopArray.Margin = new System.Windows.Forms.Padding(5);
            this.buttonTopArray.Name = "buttonTopArray";
            this.buttonTopArray.Size = new System.Drawing.Size(171, 38);
            this.buttonTopArray.TabIndex = 3;
            this.buttonTopArray.Text = "Топ игроков";
            this.buttonTopArray.UseVisualStyleBackColor = true;
            this.buttonTopArray.Click += new System.EventHandler(this.buttonTopArray_Click);
            // 
            // buttonListWord
            // 
            this.buttonListWord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonListWord.FlatAppearance.BorderSize = 0;
            this.buttonListWord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonListWord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.buttonListWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonListWord.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.buttonListWord.Location = new System.Drawing.Point(288, 396);
            this.buttonListWord.Margin = new System.Windows.Forms.Padding(5);
            this.buttonListWord.Name = "buttonListWord";
            this.buttonListWord.Size = new System.Drawing.Size(168, 38);
            this.buttonListWord.TabIndex = 4;
            this.buttonListWord.Text = "Список слов";
            this.buttonListWord.UseVisualStyleBackColor = true;
            this.buttonListWord.Click += new System.EventHandler(this.buttonListWord_Click);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTranslate.Location = new System.Drawing.Point(16, 406);
            this.buttonTranslate.Margin = new System.Windows.Forms.Padding(5);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(27, 27);
            this.buttonTranslate.TabIndex = 5;
            this.buttonTranslate.Text = "Переводчик";
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Visible = false;
            this.buttonTranslate.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // labelForPhrase
            // 
            this.labelForPhrase.BackColor = System.Drawing.Color.Transparent;
            this.labelForPhrase.Font = new System.Drawing.Font("FrenchFont", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.labelForPhrase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(203)))), ((int)(((byte)(255)))));
            this.labelForPhrase.Location = new System.Drawing.Point(2, 2);
            this.labelForPhrase.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelForPhrase.Name = "labelForPhrase";
            this.labelForPhrase.Size = new System.Drawing.Size(347, 195);
            this.labelForPhrase.TabIndex = 7;
            this.labelForPhrase.Text = "L\'amour fait les plus grandes douceurs et les plus sensibles infortunes de la vie" +
    ".";
            this.labelForPhrase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.ErrorImage = global::appFrench.Properties.Resources.menLookingUpContrast;
            this.pictureBox.Image = global::appFrench.Properties.Resources.menLookingUpContrast;
            this.pictureBox.Location = new System.Drawing.Point(-8, 200);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(306, 261);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(471, 448);
            this.Controls.Add(this.buttonLearnSpeed);
            this.Controls.Add(this.labelForPhrase);
            this.Controls.Add(this.buttonListWord);
            this.Controls.Add(this.buttonTopArray);
            this.Controls.Add(this.buttonLearnSlowed);
            this.Controls.Add(this.buttonProfile);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonTranslate);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormMain";
            this.Text = "Главное окно";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Button buttonLearnSlowed;
        private System.Windows.Forms.Button buttonLearnSpeed;
        private System.Windows.Forms.Button buttonTopArray;
        private System.Windows.Forms.Button buttonListWord;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.Label labelForPhrase;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}