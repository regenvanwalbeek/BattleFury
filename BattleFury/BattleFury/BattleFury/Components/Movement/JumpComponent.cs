using System.Collections.Generic;
using System.Linq;
using System.Text;

using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Settings;
using BEPUphysics.Collidables;
using BattleFury.SoundManager;
using BattleFury.Entities;
using BattleFury.Entities.Arenas;

namespace BattleFury.Components.Movement
{
    /// <summary>
    /// Component which enables an entity to jump.
    /// </summary>
    public class JumpComponent : Component
    {
        /// <summary>
        /// Height the character jumps.
        /// </summary>
        public int JumpHeight;

        /// <summary>
        /// Number of jumps a character can do simultaneously.
        /// </summary>
        public int MaxJumps;

        /// <summary>
        /// Whether or not the character is jumping.
        /// </summary>
        public bool JumpedThisFrame { get; private set; }

        /// <summary>
        /// Player controlling the jumping entity.
        /// </summary>
        private PlayerIndex controllingPlayer;

        /// <summary>
        /// Number of jumps since hitting the ground.
        /// </summary>
        private int numJumps = 0;

        private Environment environment;

        private const int RESET_JUMP_TIME = 25;
        private int timeSinceJump = RESET_JUMP_TIME;

        private BepuPhysicsComponent bepuPhysicsComponent;

        public JumpComponent(Entity parent, int jumpHeight, int maxJumps, Environment environment)
            : base(parent, "JumpComponent")
        {
            this.JumpHeight = jumpHeight;
            this.MaxJumps = maxJumps;
            this.JumpedThisFrame = false;
            this.environment = environment;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            controllingPlayer = ((CharacterInformationComponent) Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            bepuPhysicsComponent = ((BepuPhysicsComponent) Parent.GetComponent("BepuPhysicsComponent"));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (JumpedThisFrame)
            {
                JumpedThisFrame = false;
            }

            timeSinceJump += gameTime.ElapsedGameTime.Milliseconds;

            // Reset the number of jumps if hitting the ground and didnt just jump
            bool collidingWithPlatform = false;

            if (GameSettings.PunchJumpMode)
            {
                // Because my playtesters are sadists. Why would you play it this way?
                // Apparently it's not a bug, it's a feature...
                collidingWithPlatform = true;
            }
            else
            {
                // The way the game is meant to be played
                List<Platform> platforms = environment.Arena.Platforms;
                EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;
                EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    foreach (Platform p in platforms)
                    {
                        if (p.GetBox() == enumerator.Current)
                        {
                            collidingWithPlatform = true;
                            break;
                        }
                    }
                }
            }
            if (collidingWithPlatform && timeSinceJump > RESET_JUMP_TIME)
            {
                numJumps = 0;
            }

            // Jump if still have jumps left
            if (GameplayBindings.IsJump(controllingPlayer) && numJumps < MaxJumps)
            {
                AudioManager.PlayJump();
                timeSinceJump = 0;
                numJumps++;
                bepuPhysicsComponent.Box.LinearVelocity = new Vector3(0, JumpHeight, 0);
                JumpedThisFrame = true;
            }


        }
    }
}
