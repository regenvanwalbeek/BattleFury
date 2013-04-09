using BattleFury.Entities;
using BattleFury.Entities.Arenas;
using BattleFury.EntitySystem;
using BattleFury.Input;
using BattleFury.PhysicsEngine;
using BattleFury.ScreenManagement;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


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

        /// <summary>
        /// Manages the game entities.
        /// </summary>
        private EntityManager entityManager;

        private float pauseAlpha;

        /// <summary>
        /// Battle arena.
        /// </summary>
        private Arena arenaEntity;

        /// <summary>
        /// Camera to view the world.
        /// </summary>
        private Camera cameraEntity;

        private Physics p;

        /// <summary>
        /// Constructs the battle screen.
        /// </summary>
        public BattleScreen()
        {
            this.p = new Physics();
        }

        /// <summary>
        /// Load assets used by the game
        /// </summary>
        public override void LoadContent()
        {
            // Create the Content Manager.
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            // Create the Entity Manager.
            entityManager = new EntityManager(content);

            if (GameSettings.Arena == GameSettings.ARENA_SETTING.PLAIN_ARENA)
            {
                arenaEntity = new PlainArena();
            }
            cameraEntity = new Camera();
  
            entityManager.AddEntity(arenaEntity);
            entityManager.AddEntity(cameraEntity);
            entityManager.Initialize();

            p.LoadContent(content);
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

            p.Update(gameTime);

            entityManager.Update(gameTime);
        }

        /// <summary>
        /// Draw the gameplay screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Draw(GameTime gameTime)
        {
            this.ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);


            Matrix view = cameraEntity.ViewProjection.View;
            Matrix projection = cameraEntity.ViewProjection.Projection;
            entityManager.Draw(gameTime, view, projection);
            p.Draw(gameTime, view, projection);
        }

        /// <summary>
        /// Respond to player input. This will only be called when the gameplay screen is active.
        /// </summary>
        /// <param name="input">The current input state.</param>
        public override void HandleInput(InputState input)
        {
            // TODO handle input.
            if (GameplayBindings.IsPauseGame(input, ControllingPlayer))
            {

                ScreenManager.AddScreen(new PauseScreen(), ControllingPlayer);

            }
        }

    }
}
