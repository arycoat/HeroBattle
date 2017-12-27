using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    class MoveNone : MoveBase
    {
        public MoveNone(Character hero)
            : base(MoveType.None)
        {

        }
    }
}
