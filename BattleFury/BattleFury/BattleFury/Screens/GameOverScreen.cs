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

            // Sort the characters by placement
            for (int i = 0; i < characters.Count; i++)
            {
                for (int j = 1; j < characters.Count - i; j++)
                {
                    if (characters[j - 1].GetPlacement() > characters[j].GetPlacement())
                    {
                        Character temp = characters[j - 1];
                        characters[j - 1] = characters[j];
                        characters[j] = temp;
                    }
                }
            }
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

            // Stuff I get sick of typing
            SpriteBatch spritebatch = ScreenManager.SpriteBatch;
            int height = GameSettings.WindowHeight; 
            int width = GameSettings.WindowWidth;

            spritebatch.Begin();
            String gameString = "Game!";
            Vector2 gameStringDim = font.MeasureString(gameString);
            Vector2 gameStringLoc = new Vector2(width / 2, height / 4) - gameStringDim / 2;
            spritebatch.DrawString(font, gameString, gameStringLoc, Color.White);

            // Draw the placements
            String placements = "";
            int yPos = height / (characters.Count + 1);
            for (int i = 0; i < characters.Count; i++)
            {
                placements += getPlacementString(characters[i].GetPlacement()) +" Player " + characters[i].GetPlayerIndex() + "\n";
            }
            Vector2 strDim = font.MeasureString(placements);
            //spritebatch.DrawString(font, placements, new Vector2((width / 2) - (strDim.X / 2), gameStringLoc.Y + gameStringDim.Y + 25), Color.White);

            for (int i = 0; i < characters.Count; i++)
            {
                String str = characters[i].GetPlacement() + ". Player " + characters[i].GetPlayerIndex() + "\n";
                spritebatch.DrawString(font, str, new Vector2((width / 2) - (strDim.X / 2), gameStringLoc.Y + i *gameStringDim.Y + 50), characters[i].getColor());
            }


            spritebatch.End();
        }

        private string getPlacementString(int placement)
        {
            if (placement == 1)
            {
                return "1st";
            }
            else if (placement == 2)
            {
                return "2nd";
            }
            else if (placement == 3)
            {
                return "3rd";
            }
            else if (placement == 4)
            {
                return "4th";
            }
            else
            {
                return "FIX ME";
            }
        }

       

    }
}
