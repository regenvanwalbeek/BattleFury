using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// Manages the entities within the game world.
    /// </summary>
    public class EntityManager
    {
        /// <summary>
        /// The content manager to be used for loading content.
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// List of entities within the game world.
        /// </summary>
        private List<Entity> entities = new List<Entity>();

        /// <summary>
        /// Constructs the entity manager
        /// </summary>
        /// <param name="content">The content manager to be used for loading content.</param>
        public EntityManager(ContentManager content)
        {
            this.content = content;
            this.entities = new List<Entity>();
        }

        /// <summary>
        /// Adds an entity to the entity manager.
        /// </summary>
        /// <param name="entity">The entity to add to the manager.</param>
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        /// <summary>
        /// Remvoes an entity from the entity manager.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>True if the entity was successfully removed. False otherwise.</returns>
        public bool RemoveEntity(Entity entity)
        {
            return entities.Remove(entity);
        }

        /// <summary>
        /// Initializes all the entities in the manager.
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Initialize();
            }
        }

        /// <summary>
        /// Updates all the entities in the manager.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all the entities in the manager.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(gameTime);
            }
        }


    }
}
