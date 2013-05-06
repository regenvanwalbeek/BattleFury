using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    public class HoldOffsetComponent : Component
    {

        private BepuPhysicsComponent thisPhysicsComponent;

        private BepuPhysicsComponent otherPhysicsComponent;

        private Vector3 offset;

        public HoldOffsetComponent(Entity parent, BepuPhysicsComponent otherPhysicsComponent)
            : base(parent, "HoldOffsetComponent")
        {
            this.otherPhysicsComponent = otherPhysicsComponent;
            this.thisPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
            this.offset = thisPhysicsComponent.Box.Position - otherPhysicsComponent.Box.Position;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            thisPhysicsComponent.Box.Position = otherPhysicsComponent.Box.Position + offset;
        }
    }
}
