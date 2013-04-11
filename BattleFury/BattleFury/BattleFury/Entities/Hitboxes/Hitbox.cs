using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;

namespace BattleFury.Entities.Hitboxes
{
    public abstract class Hitbox : Entity
    {
  

        protected Entity owner;

        public Hitbox()
        {
        
        }

    }
}
