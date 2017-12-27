﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroBattle
{
    public class Room
    {
        private Map map;
        private Hero hero;
        private Hero enemy;

        public Room()
        {
            map = new Map(10, 10);
        }

        public void Initialize()
        {
            map.Initialize();
            map.load();

            hero = new Hero();
            hero.SetUp(this);
            hero.SetBrush(Brushes.Blue);
            hero.SetMoveType(new MoveNormal(hero));
            hero.SetPosition(new Point(-1, -1));

            enemy = new Hero();
            enemy.SetUp(this);
            enemy.SetBrush(Brushes.Red);
            enemy.SetMoveType(new MoveNone(enemy));
            enemy.SetPosition(new Point(-1, -1));

            //
            hero.SetPosition(new Point(5, 1));
            enemy.SetPosition(new Point(7, 5));
        }

        public void Update()
        {
            //
            if (enemy.IsAlive() == false)
            {
                Random random = new Random();
                Point endPoint = new Point(random.Next(0, 9), random.Next(0, 9));
                if (map.IsWalkable(endPoint.X, endPoint.Y) == true)
                {
                    enemy.SetPosition(endPoint);

                    hero.SetTarget(enemy);
                }
            }

            hero.Update(0);
            enemy.Update(0);
        }

        internal Map GetMap()
        {
            return this.map;
        }

        public Character SearchTarget()
        {
            return (Character)enemy;
        }

        public void OnPaint(PaintEventArgs e)
        {
            map.OnPaint(e);
            hero.OnPaint(e);
            enemy.OnPaint(e);

            //
            //Font myFont = new System.Drawing.Font("Helvetica", 11, FontStyle.Italic);
            //Brush myBrush = new SolidBrush(System.Drawing.Color.Red);
            //e.Graphics.DrawString("E", myFont, myBrush, hero.endPos.X   * 50 + 10, hero.endPos.Y   * 50 + 30);
        }
    }
}
