using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// A drawable component in the Entity-Component model. A component 
    /// represents a small piece of functionality of an entity which is able to
    /// modify the data of a parent entity.
    /// 
    /// Specific drawable components which wish to provide functionality should 
    /// subclass this class.
    /// </summary>
    public abstract class DrawableComponent : Component, IEntityDrawable
    {
        /// <summary>
        /// Whether or not the component should be drawn.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Constructs the drawable component.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="id">The name of the component.</param>
        public DrawableComponent(Entity parent, String id)
            : base(parent, id)
        {
            this.IsVisible = true;
        }

        /// <summary>
        /// Loads any content necessary for drawing this component.
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Unloads any content necessary for drawing this component
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param nam="gameTime">The current GameTime.</param>
        /// <param name="view">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        public abstract void Draw(Microsoft.Xna.Framework.GameTime gameTime, Matrix view, Matrix projection);

    }
}
