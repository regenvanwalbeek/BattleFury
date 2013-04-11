using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BEPUphysics;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;

namespace BattleFury.Entities.Physics
{
    public class BepuPhysicsBox : Entity
    {
        private BepuPhysicsComponent bepuPhysicsComponent;

        public BepuPhysicsBox(Box b, Model m)
        {
            // Create the physics Component.
            bepuPhysicsComponent = new BepuPhysicsComponent(this, b);

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(b.Width, b.Height, b.Length);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, m, scaling);

            this.AttachComponent(bepuPhysicsComponent);
            this.AttachComponent(drawComponent);
        }

        public Box GetBox()
        {
            return this.bepuPhysicsComponent.Box;
        }
        
    }
}
