using BattleFury.Components.Animated;
using BattleFury.Components.Movement;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

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

        private const int PUNCH_BASE_STRENGTH = 1;

        private const int PUNCH_MAX_STRENGTH = 10;

        private const int FIRE_SPEED = 200;

        private const int FIRE_VELOCITY = 100;

        private const float BEPU_PHYSICS_WIDTH = 1;

        private const float BEPU_PHYSICS_HEIGHT = 2.6f;

        private const float BEPU_PHYSICS_DEPTH = 1;

        public FightingRobot(int lives, Vector3 spawnPosition, PlayerIndex controllingPlayer, int team, Environment environment)
            : base("FightingRobot", lives, new Box(spawnPosition, BEPU_PHYSICS_WIDTH, BEPU_PHYSICS_HEIGHT, BEPU_PHYSICS_DEPTH, MASS), 
            controllingPlayer, team, environment)
        {
  
    
            BasicModelComponent drawComponent = new RobotRenderComponent(this, BEPU_PHYSICS_HEIGHT);
            this.AttachComponent(drawComponent);

            
            BasicModelComponent drawComponent2 = new CubeRenderComponent(this, Matrix.CreateScale(BEPU_PHYSICS_WIDTH, BEPU_PHYSICS_HEIGHT, BEPU_PHYSICS_WIDTH));
            //this.AttachComponent(drawComponent2);
            
            JumpComponent jumpComponent = new JumpComponent(this, JUMP_HEIGHT, MAX_JUMPS);
            this.AttachComponent(jumpComponent);

            MoveComponent moveComponent = new MoveComponent(this, SPEED);
            this.AttachComponent(moveComponent);

            grabComponent = new GrabComponent(this, environment, THROW_STRENGTH);
            this.AttachComponent(grabComponent);

            punchComponent = new PunchComponent(this, environment, PUNCH_SPEED, PUNCH_BASE_STRENGTH, PUNCH_MAX_STRENGTH);
            this.AttachComponent(punchComponent);

            FireProjectileComponent fireProjectileComponent = new FireProjectileComponent(this, FIRE_SPEED, FIRE_VELOCITY, environment);
            this.AttachComponent(fireProjectileComponent);



        }

  
    }
}
