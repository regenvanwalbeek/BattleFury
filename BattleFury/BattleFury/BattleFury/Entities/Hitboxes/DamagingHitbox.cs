using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFury.Entities.Hitboxes
{
    public class DamagingHitbox : Hitbox
    {
        public int RawDamageValue;

        public int RawFlinchValue;

        public DamagingHitbox()
            : base("DamagingHitbox")
        {
        
        }
    }
}
