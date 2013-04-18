using System.Collections.Generic;
using BattleFury.Components.CameraComponents;
using BattleFury.Entities;
using BattleFury.Entities.Arenas;
using BattleFury.Entities.Characters;
using BattleFury.Entities.Physics;
using BattleFury.EntitySystem;
using BattleFury.Input;
using BattleFury.ScreenManagement;
using BattleFury.Settings;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Entities.Items;
using BattleFury.Components;


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
        /// Cameras to view the world.
        /// </summary>
        private List<Camera> cameraEntities = new List<Camera>();
        private DebugCameraComponent debugCameraComponent;

        /// <summary>
        /// Index of current camera to be used for drawing.
        /// </summary>
        private int currentCameraIndex = 0;

        /// <summary>
        /// Characters in the battle
        /// </summary>
        private List<Character> characters;

        /// <summary>
        /// Numer of milliseconds since game over.
        /// </summary>
        private float timeSinceGameOver = 0;

        /// <summary>
        /// Characters still in the battle
        /// </summary>
        private List<Character> livingCharacters;

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

            // Load content necessary for the battle screen
            ContentLoader.LoadContent(content);
            
            Effect colorEffect = content.Load<Effect>("effects/ReplaceColor");


            // Create the Entity Manager.
            entityManager = new EntityManager(content);

            // Create the physics simulator
            PhysicsSimulator physicsEntity = new PhysicsSimulator();

            // Create the Arena Entity.
            Arena arenaEntity;
            if (GameSettings.Arena == GameSettings.ARENA_SETTING.PLAIN_ARENA)
            {
                arenaEntity = new PlainArena(entityManager, physicsEntity);
            }
            else
            {
                arenaEntity = new PlainArena(entityManager, physicsEntity);
            }

            // Create the Item Spawner
            ItemSpawner itemSpawner = new ItemSpawner(entityManager, physicsEntity, arenaEntity, GameSettings.ItemsOn);

            // Create the environment
            Environment environment = new Environment(arenaEntity, itemSpawner);

            // Create the Camera Entity.
            Camera gameCamera = new Camera(new Vector3(0, 10, 60), Vector3.Zero + new Vector3(0, 20, -100), Vector3.Up );
            Camera debugCamera = new Camera(new Vector3(0, 20, 40), Vector3.Zero, Vector3.Up);
            debugCameraComponent = new DebugCameraComponent(debugCamera);
            debugCamera.AttachComponent(debugCameraComponent);
            cameraEntities.Add(gameCamera); // Game Camera
            cameraEntities.Add(debugCamera); // Debug Camera

            // Create the Characters.
            List<PlayerSettings> players = GameSettings.Players;
            int numPlayers = players.Count;
            characters = new List<Character>(numPlayers);
            foreach (PlayerSettings player in players)
            {
                if (player.Character == GameSettings.CHARACTER_SETTING.ROBOT)
                {
                    Character character = new FightingRobot(GameSettings.NumLives, arenaEntity.GetCharacterSpawnPosition(), player.PlayerIndex, player.Team, environment);
                    physicsEntity.AddPhysicsEntity(character.GetBox());
                    characters.Add(character);
                    environment.AddCharacter(character);
                    
                }
            }

            // Create the Hud
            HUD hud = new HUD(characters);

            // Add the entities to the Entity Manager and init the manager
            entityManager.AddEntity(arenaEntity);
            entityManager.AddEntity(gameCamera);
            entityManager.AddEntity(debugCamera);
            entityManager.AddEntity(physicsEntity);
            entityManager.AddEntity(itemSpawner);
            for (int i = 0; i < characters.Count; i++)
            {
                entityManager.AddEntity(characters[i]);
            }
            entityManager.AddEntity(hud);
            entityManager.Initialize();

            // Track the characters that are alive
            livingCharacters = new List<Character>();
            for (int i = 0; i < characters.Count; i++)
            {
                livingCharacters.Add(characters[i]);
            }
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

            // Check if another player has been KO'd. Set placement if KO'd
            int numKOd = 0; // Count the number of characters KO'd this frame
            for (int i = 0; i < livingCharacters.Count; i++)
            {
                if (livingCharacters[i].IsKO())
                {
                    numKOd++;
                }
            }
            int placement = livingCharacters.Count - numKOd + 1;
            for (int i = 0; i < livingCharacters.Count; i++)
            {
                if (livingCharacters[i].IsKO())
                {
                    livingCharacters[i].SetPlacement(placement);
                    livingCharacters.RemoveAt(i);
                    i--;
                }
            }

            // Check if the game is over. If so, set the winner and go to the game over screen.
            if (livingCharacters.Count == 1)
            {
                livingCharacters[0].SetPlacement(1); // Winner Winner Chicken Dinner!
            }
            
            // Exit the Battle Screen
            if (livingCharacters.Count <= 1)
            {
                timeSinceGameOver += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceGameOver >= 2000)
                {
                    // Match over. Go to the game over screen.
                    LoadingScreen.Load(ScreenManager, null, new BackgroundScreen(), new GameOverScreen(characters));
                }
            }

        }

        /// <summary>
        /// Draw the gameplay screen.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Draw(GameTime gameTime)
        {
            // Reset the graphics device (for mixing 2D and 3D)
            this.ScreenManager.GraphicsDevice.BlendState = BlendState.Opaque;
            this.ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            this.ScreenManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            this.ScreenManager.GraphicsDevice.Clear(Color.Black);

            // Draw the entities based on the current camera. Will draw 3D elements first, then 2D.
            Matrix view = cameraEntities[currentCameraIndex].GetView();
            Matrix projection = cameraEntities[currentCameraIndex].GetProjection();
            entityManager.Draw(gameTime, view, projection, ScreenManager.SpriteBatch);
        }

        /// <summary>
        /// Respond to player input. This will only be called when the gameplay screen is active.
        /// </summary>
        /// <param name="input">The current input state.</param>
        public override void HandleInput(InputState input)
        {
            // Handle input.

            // Pause Handling
            if (GameplayBindings.IsPauseGame(ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseScreen(), ControllingPlayer);
            }

            
            // Debug Handling
            #if DEBUG
            if (DebugBindings.IsSwitchCamera(ControllingPlayer)){
                currentCameraIndex++;
                if (currentCameraIndex == cameraEntities.Count)
                {
                    currentCameraIndex = 0;
                
                }
                cameraEntities[currentCameraIndex].ResetCamera();
            }

            #endif
        }

    }
}
