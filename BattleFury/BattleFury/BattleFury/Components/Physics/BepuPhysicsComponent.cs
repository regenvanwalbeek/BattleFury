using System;
using System.Collections.Generic;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using BattleFury.Entities;
using BEPUphysics;

namespace BattleFury.Components
{
    public class BepuPhysicsComponent : Component
    {

        public Box Box;

        public BepuPhysicsComponent(Entity parent, Box box) : base(parent, "BepuPhysicsComponent"){
            this.Box = box;
            //PointOnPlaneJoint join;
        }

        public override void Initialize()
        {
            // Initialize the component. Do nothing.
        }

        public override void Start()
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }
    }
}
