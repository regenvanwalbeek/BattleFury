using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// Interface which drawable components should implement.
    /// </summary>
    public interface IEntityDrawable
    {
        /// <summary>
        /// Whether or not the component should be drawn.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        void Draw(GameTime gameTime, Matrix view, Matrix projection);
    }
}
