using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HeroBattle;

namespace HeroBattleClient
{
    public class Board
    {
        private readonly Map map;

        public Board(Map map)
        {
            this.map = map;
        }

        public void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    e.Graphics.DrawRectangle(Pens.Black,
                        new Rectangle(new Point(i * 50, j * 50), new Size(50, 50)));

                    if (map.IsWalkable(i, j) == false)
                    {
                        e.Graphics.FillRectangle(Brushes.Blue,
                            new Rectangle(new Point(i * 50, j * 50), new Size(50, 50)));
                    }
                }
            }
        }
    }
}
