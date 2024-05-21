using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;


namespace appFrench
{
    internal class ToolTipChanger : ToolTip
    {
        private Color backColor = Color.FromArgb(169, 179, 255);
        private Font font = new Font("Century Gothic", 8); // Приватное поле для шрифта
        private Color textColor = Color.Black;

        public ToolTipChanger()
        {
            this.OwnerDraw = true;
            this.Draw += CustomToolTip_Draw;
        }

        private void CustomToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Задаем цвет фона подсказки
            e.DrawBackground();
            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);

            // Задаем шрифт текста подсказки и его цвет
            using (Brush brush = new SolidBrush(textColor))
            {
                // Рисуем текст по центру подсказки
                e.Graphics.DrawString(e.ToolTipText, font, brush, new PointF((e.Bounds.Width - e.Graphics.MeasureString(e.ToolTipText, font).Width) / 2, (e.Bounds.Height - e.Graphics.MeasureString(e.ToolTipText, font).Height) / 2));
            }
        }
    }





}
