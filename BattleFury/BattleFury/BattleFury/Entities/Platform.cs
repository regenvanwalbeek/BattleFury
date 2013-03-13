using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components;

namespace BattleFury.Entities
{
    public class Platform : Entity
    {
        /// <summary>
        /// Flag representing whether or not the platform is solid
        /// </summary>
        public bool IsSolid;

        private TransformComponent transformComponent;

        private RectangularCollisionComponent collisionComponent;

        public Platform()
            : base("Platform")
        {
            throw new NotImplementedException();
        }

    }
}
