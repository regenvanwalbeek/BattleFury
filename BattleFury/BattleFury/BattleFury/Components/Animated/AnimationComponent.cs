using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.AnimationEngine;

namespace BattleFury.Components.Animated
{
    public abstract class AnimationComponent : DrawableComponent
    {

        protected AnimationManager animationManager;

        public AnimationComponent(Entity parent, String id)
            : base(parent, id)
        {
              
        }
    }
}
