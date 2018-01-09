using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public class NoneBase : CharacterState
    {
        public static NoneBase GetEmpty = new NoneBase();

        public NoneBase()
            : base(State.None)
        {

        }

        public override void Update()
        {

        }
    }
}
