using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Movement
{
    public class PunchableComponent : Component
    {

        /// <summary>
        /// The box that is punchable.
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        public PunchableComponent(Entity parent)
            : base(parent, "PunchableComponent")
        {
        
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public void Punch(PunchComponent puncher, int strength)
        {
            // Calculate the velocity to send the entity flying at.
            Vector3 velocity = Vector3.Normalize(this.bepuPhysicsComponent.Box.Position - puncher.GetPosition()) * strength;
            this.bepuPhysicsComponent.Box.LinearVelocity = velocity;

        }

        public Box GetPunchableBox(){
            return bepuPhysicsComponent.Box;
        }
    }
}
