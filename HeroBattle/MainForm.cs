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
        float time3 = 0;

        Timer timer = new Timer();

        Map map;
        Hero hero;

        public Form1()
        {
            this.Size = new Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Start();

            datetime = DateTime.Now;

            //
            map = new Map(10, 10);
            map.Initialize();
            map.SetBlock(3, 3);
            map.SetBlock(3, 2);
            map.SetBlock(3, 1);

            hero = new Hero();
            hero.startPos = new Point(0, 1);
            hero.endPos = new Point(5, 2);
            hero.position = hero.startPos;
        }

        private void Update(object sender, EventArgs e)
        {
            SearchParameters searchParameters = new SearchParameters(hero.startPos, hero.endPos, map);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();

            time3++;
            if (time3 >= 100)
                time3 = 0;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            map.OnPaint(e);
            hero.OnPaint(e);
        }
    }
}
