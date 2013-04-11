using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        /// Removes an entity from the entity manager.
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
        /// <param name="view">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        /// <param name="spriteBatch">The spriteBatch to draw with.</param>
        public void Draw(GameTime gameTime, Matrix view, Matrix projection, SpriteBatch spriteBatch)
        {

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(gameTime, view, projection);
            }
            spriteBatch.Begin();
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw2D(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }


    }
}
