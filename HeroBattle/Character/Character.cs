using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public abstract class Character
    {
        public CharacterState state { get; private set; }
        protected MoveBase move { get; set; }
        protected long hp, maxHp;
        protected Point position { get; set; }
        protected Room room { get; set; } 

        public Character()
        {
            this.state = new CharacterState();
        }

        public virtual void SetUp(Room room)
        {
            this.room = room;
        }

        public abstract void Update(long delta);

        public long GetHp()
        {
            return this.hp;
        }

        public Point GetPosition()
        {
            return this.position;
        }

        internal void SetPosition(Point pos)
        {
            this.position = pos;
        }

        public bool IsAlive()
        {
            return (hp > 0);
        }

        public void SetDamage(long damage)
        {
            this.hp = Math.Max(this.hp - damage, 0);
        }

        internal Map GetMap()
        {
            return this.room.GetMap();
        }
    }
}
