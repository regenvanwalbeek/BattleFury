using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using BattleFury.EntitySystem;

namespace BattleFury.Screens
{
    /// <summary>
    /// The screen to be displayed for a BattleFury match. This is the gameplay screen.
    /// </summary>
    public class BattleScreen : GameScreen
    {
        /// <summary>
        /// The manager for loading content.
        /// </summary>
        private ContentManager content;

        private EntityManager entityManager;

        public BattleScreen()
        {

        }

        /// <summary>
        /// Load assets used by the game
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            entityManager.Initialize();
        }

        /// <summary>
        /// Unload assets used by the game.
        /// </summary>
        public override void UnloadContent()
        {
           
            content.Unload();
        }

        /// <summary>
        /// Update the state of the game.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // TODO pause screens are cool.

            entityManager.Update(gameTime);
        }

        /// <summary>
        /// Draw the gameplay screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Draw(GameTime gameTime)
        {
            entityManager.Draw(gameTime);
        }

        /// <summary>
        /// Respond to player input. This will only be called when the gameplay screen is active.
        /// </summary>
        /// <param name="input">The current input state.</param>
        public override void HandleInput(InputState input)
        {
            // TODO handle input.
        }

    }
}
