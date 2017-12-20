using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroBattle
{
    public class Map
    {
        private int[][] map;
        private readonly int width;
        private readonly int height;

        public int GetWidth() { return this.width; }
        public int GetHeight() { return this.height; }

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Initialize()
        {
            map = new int[width][];
            for (int x = 0; x < width; x++)
            {
                map[x] = new int[height];
                for (int y = 0; y < height; y++)
                    map[x][y] = 0;
            }
        }

        public bool IsWalkable(int x, int y)
        {
            return (map[x][y] == 0);
        }

        internal void SetBlock(int x, int y)
        {
            map[x][y] = 1; // blocked
        }

        public void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    e.Graphics.DrawRectangle(Pens.Black,
                        new Rectangle(new Point(i * 50, j * 50), new Size(50, 50)));

                    if (IsWalkable(i, j) == false)
                    {
                        e.Graphics.FillRectangle(Brushes.Blue,
                            new Rectangle(new Point(i * 50, j * 50), new Size(50, 50)));
                    }
                }
            }
        }
    }
}
