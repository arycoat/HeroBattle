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

        internal void load()
        {
            SetBlock(3, 3);
            SetBlock(3, 2);
            SetBlock(3, 1);
        }

        public bool IsWalkable(int x, int y)
        {
            return (map[x][y] == 0);
        }

        internal void SetBlock(int x, int y)
        {
            map[x][y] = 1; // blocked
        }
    }
}
