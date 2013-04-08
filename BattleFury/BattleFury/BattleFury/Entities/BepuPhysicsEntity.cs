using System;
using System.Collections.Generic;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using BattleFury.Components;
using BattleFury.Components.Animated;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Entities
{
    public class BepuPhysicsEntity : Entity
    {

        private BepuPhysicsComponent bepuPhysicsComponent;

        public BepuPhysicsEntity(Box b, Model m, Camera c) : base("BepuPhysicsEntity")
        {
            bepuPhysicsComponent = new BepuPhysicsComponent(this, b);
            BasicModelComponent drawComponent = new BasicModelComponent(this, m, c);
            this.AttachComponent(bepuPhysicsComponent);
            this.AttachComponent(drawComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }

    }
}
