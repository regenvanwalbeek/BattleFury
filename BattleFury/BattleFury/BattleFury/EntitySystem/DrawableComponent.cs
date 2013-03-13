using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace BattleFury.EntitySystem
{
    public abstract class DrawableComponent : Component, IEntityDrawable
    {

        public bool IsVisible { get; set; }

        public DrawableComponent(Entity parent, String id)
            : base(parent, id)
        {
            this.IsVisible = true;
        }

        public abstract override void Start();

        public abstract override void Initialize();

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract override void Update(Microsoft.Xna.Framework.GameTime gameTime);

        public abstract void Draw(Microsoft.Xna.Framework.GameTime gameTime);

    }
}
