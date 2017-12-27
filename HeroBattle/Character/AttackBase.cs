using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public class AttackBase
    {
        public enum AttackType
        {
            None,
            Nearest,
        }

        private readonly AttackType attackType;

        public AttackBase(AttackType attackType)
        {
            this.attackType = attackType;
        }

        public virtual void Update()
        {

        }
    }
}
