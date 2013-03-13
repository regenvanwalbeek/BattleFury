using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    public interface IEntityUpdateable
    {
        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        void Update(GameTime gameTime);
    }
}
