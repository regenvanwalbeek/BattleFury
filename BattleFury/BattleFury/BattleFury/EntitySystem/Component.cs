using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// A component in the Entity-Component model. A component represents a 
    /// small piece of functionality of an entity which is able to modify the 
    /// data of a parent entity.
    /// 
    /// Specific components which wish to provide functionality should subclass
    /// this class.
    /// </summary>
    public abstract class Component : IEntityComponent, IEntityUpdateable
    {
        /// <summary>
        /// The component's ID. Used when getting this component from inside 
        /// the parent entity.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Whether or not the component should be updated.
        /// </summary>
        public bool Enabled { get; set; }
     
        /// <summary>
        /// The entity which this component is attached to.
        /// </summary>
        public Entity Parent { get; protected set; }

        /// <summary>
        /// Constructs the component.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="id">The name of the component.</param>
        public Component(Entity parent, String id)
        {
            this.Parent = parent;
            this.Enabled = true;
            this.ID = id;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called after initialize. Gathers references to other components.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public abstract void Update(GameTime gameTime);
    }
}
