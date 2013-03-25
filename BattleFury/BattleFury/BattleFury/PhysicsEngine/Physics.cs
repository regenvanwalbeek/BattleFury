using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics;

namespace BattleFury.PhysicsEngine
{
    // TODO should this extend Entity? Probably could...

    public class Physics
    {
        private Space space;

        public Physics()
        {
            space = new Space();   
        }

        public void Update()
        {
            space.Update();
        }
    }
}
