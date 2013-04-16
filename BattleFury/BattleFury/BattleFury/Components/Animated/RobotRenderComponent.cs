using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Animated
{
    public class RobotRenderComponent : BasicModelComponent
    {

        public Matrix Transform;

        private BepuPhysicsComponent physicsComponent;

        public RobotRenderComponent(Entity parent, Matrix transform)
            : base(parent, ContentLoader.Robot)
        {
            this.Transform = transform;
        }

        protected override Matrix GetWorld()
        {
            return Transform * physicsComponent.Box.WorldTransform;
        }

        public override void Start()
        {
            base.Start();
 
            // Get a reference to the transform component
            this.physicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }
    }
}
