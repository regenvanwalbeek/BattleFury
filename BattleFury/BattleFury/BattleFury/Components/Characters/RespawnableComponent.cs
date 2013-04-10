using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;

namespace BattleFury.Components.Characters
{
    public class RespawnableComponent : Component
    {
        VitalityComponent vitalityComponent;

        BepuPhysicsComponent bepuPhysicsComponent;

        public RespawnableComponent(Entity parent)
            : base(parent, "RespawnableComponent")
        {
            
        }


        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            vitalityComponent = (VitalityComponent) Parent.GetComponent("VitalityComponent");
            bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Check if the object is outside of the arena. 
            // If so, kill it, decrement the health, respawn
            
            
            throw new NotImplementedException();
        }
    }
}
