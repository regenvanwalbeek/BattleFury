using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;

namespace BattleFury.Components
{
    public class TransformComponent : Component
    {
        public Vector3 Position;

        public float scale;

        public Quaternion Rotation;

        public TransformComponent(Entity parent)
            : base(parent, "TransformComponent")
        {

        }

        public override void Initialize()
        {
            // TODO
            throw new NotImplementedException();
        }

        public override void Start()
        {
            // TODO
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
