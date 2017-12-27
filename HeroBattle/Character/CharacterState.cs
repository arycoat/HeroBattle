using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public class CharacterState
    {
        public enum State
        {
            None,
            Move,
            Attack,
        }

        private long timestamp;
        private State state;

        public CharacterState()
        {
            this.timestamp = 0;
            this.state = State.None;
        }

        internal State GetState()
        {
            return this.state;
        }

        internal void SetState(State state)
        {
            this.state = state;
        }

        internal long GetTimeStamp()
        {
            return this.timestamp;
        }

        internal void SetTimeStamp(long timestamp)
        {
            this.timestamp = timestamp;
        }
    }
}
