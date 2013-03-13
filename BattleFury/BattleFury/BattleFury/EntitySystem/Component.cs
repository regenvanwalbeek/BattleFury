using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    public abstract class Component : IEntityComponent, IEntityUpdateable
    {
        public string ID { get; set; }

        protected Entity Parent { get; set; }

        public Component(Entity parent, String id)
        {
            this.Parent = parent;
        }

        public abstract void Initialize();

        public abstract void Start();

        public abstract void Update(GameTime gameTime);
    }
}
