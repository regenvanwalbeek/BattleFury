using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;


namespace BattleFury.Components
{
    public class SphericalCollisionComponent : Component
    {
        public BoundingSphere boundingSphere;

        public SphericalCollisionComponent(Entity parent)
            : base(parent, "SphericalCollisionComponent")
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

    public class CopyOfSphericalCollisionComponent : Component
    {
        public BoundingSphere boundingSphere;

        public CopyOfSphericalCollisionComponent(Entity parent)
            : base(parent, "CopyOfSphericalCollisionComponent")
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
        public bool CollidesWith(CopyOfSphericalCollisionComponent collisionComponent)
        {
            throw new NotImplementedException();
        }
    }
}
