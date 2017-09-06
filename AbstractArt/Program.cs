using System;
using System.Windows.Forms;
using System.Drawing;

namespace AbstractArt
{
    static class Program
    {
        public static Random random = new Random();
        [STAThread]
        static void Main()
        {
            while (true)
            {
                new FormMain()
                {
                    ForeColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)),
                    BackColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)),
                    Size = new Size(random.Next(Screen.PrimaryScreen.Bounds.Size.Width), random.Next(Screen.PrimaryScreen.Bounds.Size.Height)),
                    Location = new Point(random.Next(Screen.PrimaryScreen.Bounds.Size.Width), random.Next(Screen.PrimaryScreen.Bounds.Size.Height)),
            }.Show();
                
            }
        }
    }
}
