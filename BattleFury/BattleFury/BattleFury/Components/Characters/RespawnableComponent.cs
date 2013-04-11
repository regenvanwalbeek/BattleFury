using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Arenas;
using BattleFury.Components.Animated;

namespace BattleFury.Components.Characters
{
    public class RespawnableComponent : Component
    {
        private VitalityComponent vitalityComponent;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private CubeRenderComponent cubeRenderComponent;

        private Arena arena;

        /// <summary>
        /// Number of milliseconds to stay dead after death.
        /// </summary>
        private const int RESPAWN_TIME = 2000;

        /// <summary>
        /// Amount of time until player can respawn
        /// </summary>
        private int timeTillRespawn;

        public RespawnableComponent(Entity parent, Arena arena)
            : base(parent, "RespawnableComponent")
        {
            this.arena = arena;

        }


        public override void Initialize()
        {
        }

        public override void Start()
        {
            vitalityComponent = (VitalityComponent)Parent.GetComponent("VitalityComponent");
            bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
            cubeRenderComponent = (CubeRenderComponent)Parent.GetComponent("BasicModelComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // If the character is alive, check if it needs to be killed for falling off the arena.
            if (vitalityComponent.IsAlive)
            {
                // Check if the object is outside of the arena. If so, kill it.
                if (arena.GetBoundingBox().Contains(bepuPhysicsComponent.Box.Position) == ContainmentType.Disjoint)
                {
                    // Kill the character
                    vitalityComponent.LivesLeft--;
                    vitalityComponent.IsAlive = false;
                    timeTillRespawn = RESPAWN_TIME;
                    cubeRenderComponent.IsVisible = false; // Just hide the character until respawn.
                    Console.WriteLine("Killing");
                }
            }
            else
            {
                // The character is dead. Check if it needs to be respawned.
                timeTillRespawn -= gameTime.ElapsedGameTime.Milliseconds;
                if (timeTillRespawn < 0)
                {
                    // Respawn
                    vitalityComponent.RageMeter = 100.0f;
                    bepuPhysicsComponent.Box.Position = arena.GetSpawnPosition();
                    bepuPhysicsComponent.Box.LinearVelocity = Vector3.Zero;
                    vitalityComponent.IsAlive = true;
                    cubeRenderComponent.IsVisible = true;
                    Console.WriteLine("Respawning");
                }
            }

        }
    }
}
