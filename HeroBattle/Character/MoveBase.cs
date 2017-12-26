using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    class MoveBase
    {
        public MoveBase()
        {

        }

        public virtual void Update()
        {

        }

        public virtual bool IsEnd()
        {
            return true;
        }

        public virtual void FindPath(Hero target_)
        {

        }
    }
}
