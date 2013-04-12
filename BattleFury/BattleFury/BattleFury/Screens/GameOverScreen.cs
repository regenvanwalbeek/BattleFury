using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.ScreenManagement;
using BattleFury.Entities;
using BattleFury.Input;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Characters;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleFury.Screens
{
    /// <summary>
    /// Screen to display after the completion of a battle.
    /// </summary>
    public class GameOverScreen : GameScreen
    {

        /// <summary>
        /// Minimum milliseconds the user will see this screen.
        /// Don't let the user accidentally exit this screen. 
        /// </summary>
        private const int MIN_SCREEN_TIME = 2000;
        private int timeOnScreen = 0;

        private List<Character> characters;

        private ContentManager content;

        private SpriteFont font;

        public GameOverScreen(List<Character> characters)
        {
            this.characters = characters;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            // Create the Content Manager.
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            font = content.Load<SpriteFont>("fonts/menufont");
            // TODO
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            timeOnScreen += gameTime.ElapsedGameTime.Milliseconds;

            if (timeOnScreen > MIN_SCREEN_TIME)
            {
                PlayerIndex playerIndex;
                if (MenuBindings.IsMenuSelect(null, out playerIndex))
                {
                    // Go to the main menu screen
                    LoadingScreen.Load(ScreenManager, null, new BackgroundScreen(), new MainMenuScreen());
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(font, "Game Over", new Vector2(GameSettings.WindowWidth / 2, 
                GameSettings.WindowHeight / 2) - font.MeasureString("Game Over") / 2, Color.White);
            ScreenManager.SpriteBatch.End();
            // TODO Draw placements
        }

       

    }
}
