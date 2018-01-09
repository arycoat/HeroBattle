using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HeroBattle;
using System.Drawing;
using System.Windows;

namespace HeroBattleClient
{
    class Form1 : Form
    {
        DateTime datetime;

        Timer timer = new Timer();
        Timer timer2 = new Timer();

        private Room room;

        private Board board;
        private List<Vehicle> movers;

        public Form1()
        {
            this.Size = new System.Drawing.Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 150;
            timer.Enabled = true;

            timer2.Tick += new EventHandler(Draw);
            timer2.Interval = 20;
            timer2.Enabled = true;

            datetime = DateTime.Now;

            //
            room = new Room();
            room.Initialize();

            board = new Board(room.GetMap());
            movers = new List<Vehicle>();
            foreach (Character parent in room.characters)
            {
                Vehicle mover = new Vehicle();
                mover.id = parent.GetID();
                mover.position = new Vector(parent.GetPosition().X * 50 + 25, parent.GetPosition().Y * 50 + 25);
                mover.mass = 1;
                mover.range = 0;
                mover.brush = parent.GetCharacterType() == Character.CharacterType.Player ? Brushes.Blue : Brushes.Red;
                movers.Add(mover);
            }

            timer.Start();
            timer2.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            //
            room.Update();
        }

        private void Draw(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            board.OnPaint(e);
            foreach (Vehicle mover in movers)
            {
                Character parent = room.FindCharacter(mover.id);
                if (parent.IsAlive() == false)
                    continue;

                Vector target = new Vector(parent.GetPosition().X * 50 + 25, parent.GetPosition().Y * 50 + 25);
                mover.seek(target);
                mover.Update();
                mover.OnPaint(e);
            }

            //!~ debug paint
            //room.OnPaint(e);
            //~!
        }
    }
}
