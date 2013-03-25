using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    /// <summary>
    /// Interface which updateable components should implement.
    /// </summary>
    public interface IEntityUpdateable
    {
        /// <summary>
        /// Whether or not the component should be updated.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        void Update(GameTime gameTime);
    }
}
