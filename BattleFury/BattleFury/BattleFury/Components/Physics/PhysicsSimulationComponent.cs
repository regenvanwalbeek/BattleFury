using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BEPUphysics;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Physics
{
    /// <summary>
    /// Component which manages the physics simulations for the game. Acts as 
    /// an interface the the physics engine (in this case, the BepuPhysics 
    /// Engine).
    /// </summary>
    public class PhysicsSimulationComponent : Component
    {

        private Space space;

        public PhysicsSimulationComponent(Entity parent)
            : base(parent, "PhysicsSimulationComponent")
        {
            space = new Space();
            space.ForceUpdater.Gravity = new Vector3(0, -20.0f, 0);
            space.TimeStepSettings.TimeStepDuration = 1.0f / 30.0f; // 1 / 60.0f is default'
        }
        
        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            space.Update();
        }

        public void AddPhysicsObject(ISpaceObject spaceObject)
        {
            space.Add(spaceObject);
        }

        public void RemovePhysicsObject(ISpaceObject spaceObject)
        {
            space.Remove(spaceObject);
        }

    }
}
