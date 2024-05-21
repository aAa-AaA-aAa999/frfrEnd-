using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace appFrench
{
    internal class LabelChanger : Label
    {
        public Color forGradLabelStartColor { get; set; }
        public Color forGradLabelEndColor { get; set; }

        public LabelChanger() { }
        public LabelChanger(string type)
        {
            switch (type)
            {
                case "Регистрация":
                   SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                   Text = "Регистрация";
                   Font = new Font("Century Gothic", 34, FontStyle.Bold);
                   Location = new Point(12, 9);
                   Size = new Size(322, 55);
                   forGradLabelStartColor = Color.FromArgb(241, 243, 255);
                   forGradLabelEndColor = Color.FromArgb(200, 190, 255);
                   break;
                case "Название список":
                    SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    Text = "Список используемых \r\nв приложении слов\r\n";
                    Font = new Font("Century Gothic", 20, FontStyle.Bold);
                    Location = new Point(12, 13);
                    Size = new Size(329, 64);
                    forGradLabelStartColor = Color.FromArgb(241, 243, 255);
                    forGradLabelEndColor = Color.FromArgb(200, 190, 255);
                    break;
            }
        }
        internal static void deleteBackgroundLabel(Label[] lab) {
            foreach (Label l in lab) { l.BackColor = Color.Transparent; }

        }

        internal static void lab_MouseEnter(object sender, EventArgs e, int color)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.FromArgb(color); // Изменение цвета текста при наведении
            }
        }

        internal static void lab_MouseLeave(object sender, EventArgs e, int color)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.FromArgb(color); // Возврат исходного цвета текста при уходе мыши
            }
        }

        internal static void lab_MouseDown(object sender, MouseEventArgs e, int color)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.FromArgb(color); // Изменение цвета текста при нажатии
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, forGradLabelStartColor, forGradLabelEndColor, LinearGradientMode.Horizontal))
            {
                e.Graphics.DrawString(Text, Font, brush, ClientRectangle);
            }
        }

    }
}
