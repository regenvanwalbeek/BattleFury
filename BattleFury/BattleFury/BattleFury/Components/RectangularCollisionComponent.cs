using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    public class RectangularCollisionComponent : Component
    {


        public RectangularCollisionComponent(Entity parent)
            : base(parent, "CollisionComponent")
        {

        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if two collideable objects are colliding
        /// </summary>
        /// <param name="collisionComponent">The other collideable object</param>
        /// <returns>True if the two objects are colliding.</returns>
        public bool CollidesWith(SphericalCollisionComponent collisionComponent)
        {
            throw new NotImplementedException();
        }
    }
}
