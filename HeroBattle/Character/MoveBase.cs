using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public class MoveBase
    {
        public enum MoveType
        {
            None,
            Normal,
        }

        public MoveType moveType { get; private set; }

        public MoveBase(MoveType moveType)
        {
            this.moveType = moveType;
        }

        public virtual void Update()
        {

        }

        public virtual bool IsEnd()
        {
            return true;
        }

        public virtual void FindPath(Character target_)
        {

        }
    }
}
