using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.Physics;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs;

namespace BattleFury.Entities.Physics
{
    public class PhysicsSimulator : Entity
    {

        private PhysicsSimulationComponent physics;

        public PhysicsSimulator()
        {
            physics = new PhysicsSimulationComponent(this);
            this.AttachComponent(physics);
        }

        public void AddPhysicsEntity(Box physicsEntity)
        {
            physics.AddPhysicsObject(physicsEntity);
        }

        public void RemovePhysicsEntity(Box physicsEntity)
        {
            physics.RemovePhysicsObject(physicsEntity);
        }

    }
}
