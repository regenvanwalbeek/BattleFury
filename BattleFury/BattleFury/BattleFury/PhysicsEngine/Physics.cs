using System;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.PhysicsEngine
{
    // TODO should this extend Entity? Probably could...

    public class Physics
    {
        private Space space;

        public Physics()
        {
            space = new Space();
            Box ground = new Box(Vector3.Zero, 30, 1, 30);
            space.Add(ground);
            space.Add(new Box(new Vector3(0, 4, 0), 1, 1, 1, 1));
            space.Add(new Box(new Vector3(0, 8, 0), 1, 1, 1, 1));
            space.Add(new Box(new Vector3(0, 12, 0), 1, 1, 1, 1));
            space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);
        }

        public void LoadContent()
        {
        
        }

        public void Update()
        {
            space.Update();
        }

        public void Draw()
        {
              
        }

        public void Add(ISpaceObject spaceObject)
        {
            space.Add(spaceObject);
        }
    }
}
