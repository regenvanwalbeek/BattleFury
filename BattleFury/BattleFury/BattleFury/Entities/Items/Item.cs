using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BattleFury.Components.Animated;

namespace BattleFury.Entities.Items
{
    public abstract class Item : Entity
    {
        protected TransformComponent transformComponent;

        protected AnimationComponent animationComponent;

        public Item(string id) : base(id){
        
        }
    }
}
