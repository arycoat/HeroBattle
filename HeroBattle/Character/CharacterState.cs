using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public abstract class CharacterState
    {
        public enum State
        {
            None,
            Move,
            Attack,
        }

        private long timestamp;
        protected State state { private set; get; }

        public CharacterState(State state)
        {
            this.timestamp = 0;
            this.state = state;
        }

        internal State GetState()
        {
            return this.state;
        }

        internal long GetTimeStamp()
        {
            return this.timestamp;
        }

        internal void SetTimeStamp(long timestamp)
        {
            this.timestamp = timestamp;
        }

        public abstract void Update();
    }
}
