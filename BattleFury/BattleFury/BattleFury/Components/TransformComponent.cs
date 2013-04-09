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

        // public float scale;

        // public Quaternion Rotation;

        public TransformComponent(Entity parent, Vector3 position)
            : base(parent, "TransformComponent")
        {
            this.Position = position;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
