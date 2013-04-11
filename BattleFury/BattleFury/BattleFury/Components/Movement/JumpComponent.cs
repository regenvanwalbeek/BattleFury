﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Settings;
using BEPUphysics.Collidables;

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
        /// Player controlling the jumping entity.
        /// </summary>
        private PlayerIndex controllingPlayer;

        /// <summary>
        /// Number of jumps since hitting the ground.
        /// </summary>
        private int numJumps = 0;

        private BepuPhysicsComponent bepuPhysicsComponent;

        public JumpComponent(Entity parent, int jumpHeight, int maxJumps)
            : base(parent, "JumpComponent")
        {
            this.JumpHeight = jumpHeight;
            this.MaxJumps = maxJumps;
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
            // Reset the number of jumps if hitting the ground
            CollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedCollidables;
            if (overlappedCollideables.Count > 0)
            {
                numJumps = 0;
            }

            // Jump if still have jumps left
            if (GameplayBindings.IsJump(controllingPlayer) && numJumps < MaxJumps)
            {
                numJumps++;
                bepuPhysicsComponent.Box.LinearVelocity = new Vector3(0, JumpHeight, 0);
            }


        }
    }
}
