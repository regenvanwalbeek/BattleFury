using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Components;
using Microsoft.Xna.Framework;
using BattleFury.Components.Animated;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Components.Movement;
using BattleFury.Entities.Arenas;

namespace BattleFury.Entities.Characters
{
    public class FightingRobot : Character
    {
        private const int SPEED = 10;

        private const int JUMP_HEIGHT = 20;

        private const int MAX_JUMPS = 2;

        private const int MASS = 1;

        private const int THROW_STRENGTH = 100;

        private const int PUNCH_SPEED = 1;

        private const int PUNCH_STRENGTH = 100;

        private const int FIRE_SPEED = 200;

        private const int FIRE_VELOCITY = 100;

        public FightingRobot(int lives, Vector3 spawnPosition, PlayerIndex controllingPlayer, int team, Environment environment)
            : base("FightingRobot", lives, new Box(spawnPosition, 1, 1, 1, MASS), controllingPlayer, team, environment)
        {
            
            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(1, 1, 1);
            scaling *= Matrix.CreateRotationX(MathHelper.PiOver2);
            scaling *= Matrix.CreateRotationY(MathHelper.PiOver2);
            scaling *= Matrix.CreateRotationZ(MathHelper.PiOver2);

            BasicModelComponent drawComponent = new RobotRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            JumpComponent jumpComponent = new JumpComponent(this, JUMP_HEIGHT, MAX_JUMPS);
            this.AttachComponent(jumpComponent);

            MovementComponent moveComponent = new MovementComponent(this, SPEED);
            this.AttachComponent(moveComponent);

            grabComponent = new GrabComponent(this, environment, THROW_STRENGTH);
            this.AttachComponent(grabComponent);

            punchComponent = new PunchComponent(this, environment, PUNCH_SPEED, PUNCH_STRENGTH);
            this.AttachComponent(punchComponent);

            FireProjectileComponent fireProjectileComponent = new FireProjectileComponent(this, FIRE_SPEED, FIRE_VELOCITY, environment);
            this.AttachComponent(fireProjectileComponent);



        }

  
    }
}
