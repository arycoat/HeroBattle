using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using HeroBattle.PathFind;

namespace HeroBattle
{
    class Form1 : Form
    {
        DateTime datetime;

        Timer timer = new Timer();

        Map map;
        Hero hero;
        Hero enemy;

        public Form1()
        {
            this.Size = new Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 150;
            timer.Enabled = true;
            timer.Start();

            datetime = DateTime.Now;

            //
            map = new Map(10, 10);
            map.Initialize();
            map.load();

            hero = new Hero();
            hero.SetUp(map);
            hero.SetPosition(new Point(0, 0));

            enemy = new Hero();
            enemy.SetUp(map);
            enemy.SetPosition(new Point(5, 2));

            hero.endPos = enemy.GetPosition();
        }

        private void Update(object sender, EventArgs e)
        {
            //
            if (hero.IsEndMove())
            {
                Random random = new Random();
                Point endPoint = new Point(random.Next(0, 9), random.Next(0, 9));
                if (map.IsWalkable(endPoint.X, endPoint.Y) == true)
                {
                    enemy.SetPosition(endPoint);

                    hero.endPos = enemy.GetPosition();
                }
            }

            hero.Update(0);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            map.OnPaint(e);
            hero.OnPaint(e);

            //
            Font myFont = new System.Drawing.Font("Helvetica", 11, FontStyle.Italic);
            Brush myBrush = new SolidBrush(System.Drawing.Color.Red);
            e.Graphics.DrawString("E", myFont, myBrush, hero.endPos.X   * 50 + 10, hero.endPos.Y   * 50 + 30);
        }
    }
}
