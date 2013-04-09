using BattleFury.Entities;
using BattleFury.Entities.Arenas;
using BattleFury.EntitySystem;
using BattleFury.Input;
using BattleFury.ScreenManagement;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BattleFury.Entities.Physics;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.Entities.Prefabs;
using System.Collections.Generic;


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
        /// 1x1x1 unit cube model.
        /// </summary>
        private Model cube;

        /// <summary>
        /// Battle arena.
        /// </summary>
        private Arena arenaEntity;

        /// <summary>
        /// Cameras to view the world.
        /// </summary>
        private List<Camera> cameraEntities = new List<Camera>();

        /// <summary>
        /// Index of current camera to be used for drawing.
        /// </summary>
        private int currentCameraIndex = 0;

        /// <summary>
        /// Physics Simulator for all the game objects.
        /// </summary>
        private PhysicsSimulator physicsEntity;

        /// <summary>
        /// Constructs the battle screen.
        /// </summary>
        public BattleScreen()
        {
 
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

            // Load the unit cube model.
            cube = content.Load<Model>("meshes/cube");

            // Create the Entity Manager.
            entityManager = new EntityManager(content);

            // Create the Arena Entity.
            if (GameSettings.Arena == GameSettings.ARENA_SETTING.PLAIN_ARENA)
            {
                arenaEntity = new PlainArena();
            }

            // Create the Camera Entity.
            Camera gameCamera = new Camera(new Vector3(0, 10, 40), Vector3.Zero, Vector3.Up);
            Camera debugCamera = new Camera(new Vector3(0, 20, 40), Vector3.Zero, Vector3.Up);
            cameraEntities.Add(gameCamera); // Game Camera
            cameraEntities.Add(debugCamera); // Debug Camera

            // Create the Physics Simulator.
            physicsEntity = new PhysicsSimulator();
            BepuPhysicsBox ground = new BepuPhysicsBox(new Box(Vector3.Zero, 30, 1, 30), cube);
            BepuPhysicsBox stuff1 = new BepuPhysicsBox(new Box(new Vector3(0, 4, 0), 1, 5, 1, 1), cube);
            BepuPhysicsBox stuff2 = new BepuPhysicsBox(new Box(new Vector3(0, 8, 0), 1, 1, 1, 1), cube);
            BepuPhysicsBox stuff3 = new BepuPhysicsBox(new Box(new Vector3(0, 12, 0), 1, 1, 1, 1), cube);
            physicsEntity.AddPhysicsEntity(ground);
            physicsEntity.AddPhysicsEntity(stuff1);
            physicsEntity.AddPhysicsEntity(stuff2);
            physicsEntity.AddPhysicsEntity(stuff3);

            // Add the entities to the Entity Manager and init the manager
            entityManager.AddEntity(arenaEntity);
            entityManager.AddEntity(gameCamera);
            entityManager.AddEntity(debugCamera);
            entityManager.AddEntity(physicsEntity);
            entityManager.AddEntity(ground);
            entityManager.AddEntity(stuff1);
            entityManager.AddEntity(stuff2);
            entityManager.AddEntity(stuff3);
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

            // Update the entities.
            entityManager.Update(gameTime);
        }

        /// <summary>
        /// Draw the gameplay screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Draw(GameTime gameTime)
        {
            this.ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the entities based on the current camera
            Matrix view = cameraEntities[currentCameraIndex].GetView();
            Matrix projection = cameraEntities[currentCameraIndex].GetProjection();
            entityManager.Draw(gameTime, view, projection);
        }

        /// <summary>
        /// Respond to player input. This will only be called when the gameplay screen is active.
        /// </summary>
        /// <param name="input">The current input state.</param>
        public override void HandleInput(InputState input)
        {
            // Handle input.

            // Pause Handling
            if (GameplayBindings.IsPauseGame(input, ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseScreen(), ControllingPlayer);
            }

            
            // Debug Handling
            #if DEBUG
            if (DebugBindings.IsSwitchCamera(input, ControllingPlayer)){
                currentCameraIndex++;
                if (currentCameraIndex == cameraEntities.Count)
                {
                    currentCameraIndex = 0;
                }
            }

            #endif
        }

    }
}
