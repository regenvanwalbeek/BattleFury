using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;

namespace BattleFury.Entities.Items
{
    public abstract class Item : Entity
    {
        protected BepuPhysicsComponent bepuPhysicsComponent;

        public Item(Box box){
            this.bepuPhysicsComponent = new BepuPhysicsComponent(this, box);
            this.AttachComponent(bepuPhysicsComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }
    }
}
