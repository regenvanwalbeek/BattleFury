using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using BattleFury.ScreenManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleFury.Screens
{
    public class MainMenuBackgroundScreen : GameScreen
    {
        private ContentManager content;
        private Texture2D backgroundTexture;
        private Texture2D redRobotTexture;
        private Texture2D blueRobotTexture;

        public MainMenuBackgroundScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            backgroundTexture = content.Load<Texture2D>("textures/background");
            redRobotTexture = content.Load<Texture2D>("textures/red");
            blueRobotTexture = content.Load<Texture2D>("textures/blue");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, 
            bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            ScreenManager.GraphicsDevice.Clear(Color.Black);
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            /*
            spriteBatch.Draw(backgroundTexture, fullscreen, 
                new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            */
            float scale = .8f *((float)viewport.Height) / 1080f;
            spriteBatch.Draw(blueRobotTexture, new Vector2(0, viewport.Height - (blueRobotTexture.Height * scale)), 
                null, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha), 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(redRobotTexture, new Vector2(viewport.Width - (redRobotTexture.Width * scale), viewport.Height - (redRobotTexture.Height * scale)), null, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha),
                0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
