using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// An entity in the Entity-Component model. Entities represent objects 
    /// within the game world.
    /// 
    /// Specific entities within the gameworld should subclass this class.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The Entity's id.
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// List of components for the entity.
        /// </summary>
        private List<IEntityComponent> components = new List<IEntityComponent>();

        /// <summary>
        /// List of updateable components for the entity.
        /// </summary>
        private List<IEntityUpdateable> updateableComponents = new List<IEntityUpdateable>();

        /// <summary>
        /// This of drawable components for the entity.
        /// </summary>
        private List<IEntityDrawable> drawableComponents = new List<IEntityDrawable>();

        /// <summary>
        /// Whether or not this entity has been initialized.
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        public Entity(String id){
            this.ID = id;
        }
        
        /// <summary>
        /// Attaches a component to this entity.
        /// </summary>
        /// <param name="component">The component to attach to the entity.</param>
        public void AttachComponent(IEntityComponent component){
            // Don't allow null parameters.
            if (component == null)
            {
                throw new ArgumentNullException("Component is null.");
            }

            // Don't allow multiples of the same type of component
            String id = component.ID;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].ID.Equals(id))
                {
                    throw new ArgumentException("Component already exists in entity.");
                }
            }

            // Add the component
            components.Add(component);

            if (component is IEntityUpdateable)
            {
                updateableComponents.Add((IEntityUpdateable)component);
            }

            if (component is IEntityDrawable)
            {
                drawableComponents.Add((IEntityDrawable)component);
            }

            // Initialize the component if this entity has already been initialized.
            if (isInitialized)
            {
                component.Initialize();
            }
        }

        /// <summary>
        /// Removes a component from the entity
        /// </summary>
        /// <param name="component">The component to remove</param>
        /// <returns>Returns true if the component was removed. False otherwise.</returns>
        public bool DetachComponent(IEntityComponent component){
            // Don't allow null parameters.
            if (component == null)
            {
                throw new ArgumentNullException("Component is null");
            }

            bool removed = components.Remove(component);
            if (removed)
            {
                if (component is IEntityUpdateable)
                {
                    updateableComponents.Remove((IEntityUpdateable) component);
                }

                if (component is IEntityDrawable)
                {
                    drawableComponents.Remove((IEntityDrawable) component);
                }

            }
            return removed;
        }

        /// <summary>
        /// Initializes the entity by initializing and starting the components in the entity.
        /// </summary>
        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Initialize();
            }

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }

            isInitialized = true;
        }

        /// <summary>
        /// Updates the entity by updating each component attached to this entity.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < updateableComponents.Count; i++)
            {
                if (updateableComponents[i].Enabled)
                {
                    updateableComponents[i].Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Draws the entity by drawing each component attached to this entity.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < drawableComponents.Count; i++)
            {
                if (drawableComponents[i].IsVisible)
                {
                    drawableComponents[i].Draw(gameTime);
                }
            }
        }

        /// <summary>
        /// Retrieves a component by a given id.
        /// </summary>
        /// <param name="id">The id to retrieve</param>
        /// <returns>The component with the specified id.</returns>
        public IEntityComponent GetComponent(String id)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].ID.Equals(id))
                {
                    return components[i];
                }
            }
            throw new ArgumentException("Component id: " + id + " does not exist.");
        }


    }
}
