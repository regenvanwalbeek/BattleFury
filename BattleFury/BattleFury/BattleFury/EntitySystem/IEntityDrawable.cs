using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        /// Draws 2D elements.
        /// </summary>
        /// <param name="gameTime">The current GameTime</param>
        /// <param name="spriteBatch">The spritebatch to draw with.</param>
        void Draw2D(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
