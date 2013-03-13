using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    public abstract class Entity
    {
        public string ID { get; protected set; }

        public List<IEntityComponent> Components { get; protected set; }

        public Entity(String id){
            this.ID = id;
        }

        public void AttachComponent(IEntityComponent component){
            // TODO
        }

        public void DetachComponent(IEntityComponent component){
            // TODO
        }

        public void Initialize()
        {
            // TODO
        }

        public void Update(GameTime gameTime)
        {
            // TODO
        }

        public void Draw(GameTime gameTime)
        {
            // TODO
        }


    }
}
