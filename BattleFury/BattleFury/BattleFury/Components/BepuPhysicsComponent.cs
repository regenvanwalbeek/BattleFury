using System;
using System.Collections.Generic;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using BattleFury.Entities;

namespace BattleFury.Components
{
    public class BepuPhysicsComponent : Component
    {

        public Box Box;

        public BepuPhysicsComponent(Entity parent, Box b) : base(parent, "BepuPhysicsComponent"){
            this.Box = b;
        }

        public override void Initialize()
        {
            // Initialize the component. Do nothing.
        }

        public override void Start()
        {
            // Gather references to other components. Do nothing.
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Do nothing
        }
    }
}
