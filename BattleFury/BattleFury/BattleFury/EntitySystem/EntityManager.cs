using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace BattleFury.EntitySystem
{
    public class EntityManager
    {

        private ContentManager content;

        private List<Entity> entities;

        public EntityManager(ContentManager content)
        {
            this.content = content;
            this.entities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void RemoveEntity(Entity entity)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Update()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Draw()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
