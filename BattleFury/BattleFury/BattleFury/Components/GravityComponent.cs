using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    public class GravityComponent : Component
    {
        /// <summary>
        /// The direction gravity is pulling objects
        /// </summary>
        public Vector3 gravity;

        public GravityComponent(Entity parent)
            : base(parent, "GravityComponent")
        {
            throw new NotImplementedException();
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
    }
}
