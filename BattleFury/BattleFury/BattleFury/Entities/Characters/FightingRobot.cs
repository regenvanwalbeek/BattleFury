﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Components;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Components.Movement;

namespace BattleFury.Entities.Characters
{
    public class FightingRobot : Character
    {
        private const int SPEED = 10;

        private const int JUMP_HEIGHT = 10;

        private const int MASS = 1;

        public FightingRobot(int lives, Vector3 spawnPosition, Model model, PlayerIndex controllingPlayer, int team)
            : base("FightingRobot", lives, new Box(spawnPosition, 1, 1, 1, MASS), controllingPlayer, team)
        {
            
            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, model, scaling);
            this.AttachComponent(drawComponent);

            JumpComponent jumpComponent = new JumpComponent(this, SPEED);
            this.AttachComponent(jumpComponent);

            MovementComponent moveComponent = new MovementComponent(this, JUMP_HEIGHT);
            this.AttachComponent(moveComponent);

        }

  
    }
}
