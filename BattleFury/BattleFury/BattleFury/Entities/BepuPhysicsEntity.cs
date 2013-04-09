using System;
using System.Collections.Generic;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using BattleFury.Components;
using BattleFury.Components.Animated;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities
{
    public class BepuPhysicsEntity : Entity
    {

        private BepuPhysicsComponent bepuPhysicsComponent;

        public BepuPhysicsEntity(Box b, Model m) : base("BepuPhysicsEntity")
        {
            bepuPhysicsComponent = new BepuPhysicsComponent(this, b);

            // Since the cube model is 1x1x1, it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(b.Width, b.Height, b.Length); 

            BasicModelComponent drawComponent = new CubeRenderComponent(this, m, scaling);
            this.AttachComponent(bepuPhysicsComponent);
            this.AttachComponent(drawComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }

    }
}
