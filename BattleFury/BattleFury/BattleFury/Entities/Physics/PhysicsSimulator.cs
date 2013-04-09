using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.Physics;
using BEPUphysics;

namespace BattleFury.Entities.Physics
{
    public class PhysicsSimulator : Entity
    {

        private PhysicsSimulationComponent physics;

        public PhysicsSimulator()
            : base("PhysicsSimulator")
        {
            physics = new PhysicsSimulationComponent(this);
            this.AttachComponent(physics);
        }

        public void AddPhysicsEntity(BepuPhysicsBox physicsEntity)
        {
            physics.AddPhysicsObject(physicsEntity.GetBox());
        }

        public void RemovePhysicsEntity(BepuPhysicsBox physicsEntity)
        {
            physics.RemovePhysicsObject(physicsEntity.GetBox());
        }

    }
}
