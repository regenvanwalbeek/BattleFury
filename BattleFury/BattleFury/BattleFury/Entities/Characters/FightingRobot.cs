using BattleFury.Components.Animated;
using BattleFury.Components.Movement;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.Entities.Characters
{
    public class FightingRobot : Character
    {
        /// <summary>
        /// Scale of the character. Useful for physics and rendering.
        /// </summary>
        private const float SCALE = 1.2f;

        /// <summary>
        /// Move speed of the character
        /// </summary>
        private const int SPEED = 10;

        /// <summary>
        /// Height the character can jump
        /// </summary>
        private const int JUMP_HEIGHT = 20;

        /// <summary>
        /// Number of times the character can jump
        /// </summary>
        private const int MAX_JUMPS = 2;

        /// <summary>
        /// Mass of the character
        /// </summary>
        private const int MASS = 1;

        /// <summary>
        /// Base damage the character will deal with a throw attack (when rage = 100%)
        /// </summary>
        private const int THROW_BASE_DAMAGE = 5;

        /// <summary>
        /// Max damage the character will deal with a throw attack (when rage = 0%)
        /// </summary>
        private const int THROW_MAX_DAMAGE = 15;

        /// <summary>
        /// Min distance this character will flinch when thrown (when rage = 100%)
        /// </summary>
        private const float THROW_MIN_FLINCH = 25;

        /// <summary>
        /// Max distance this character will flinch when thrown (when rage = 0%)
        /// </summary>
        private const float THROW_MAX_FLINCH = 50;

        /// <summary>
        /// Max Speed the character can punch at
        /// </summary>
        private const int PUNCH_SPEED = 1;

        /// <summary>
        /// Base damage dealt with a punch attack (when rage = 100%)
        /// </summary>
        private const int PUNCH_BASE_DAMAGE = 4;

        /// <summary>
        /// Max damage dealt with a punch attack (when rage = 0%)
        /// </summary>
        private const int PUNCH_MAX_DAMAGE = 20;

        /// <summary>
        /// Min distance this character will flinch when punched (when rage = 100%)
        /// </summary>
        private const float PUNCH_MIN_FLINCH = 0;

        /// <summary>
        /// Max distance this character will flinch when punched (when rage = 0%)
        /// </summary>
        private const float PUNCH_MAX_FLINCH = 50;

        private const float GROUND_POUND_BASE_DAMAGE = 4;

        private const float GROUND_POUND_MAX_DAMAGE = 20;

        private const float GROUND_POUND_MIN_FLINCH = 0;

        private const float GROUND_POUND_MAX_FLINCH = 50;

        /// <summary>
        /// Max speed the character can fire a projectile
        /// </summary>
        private const int FIRE_SPEED = 170;

        /// <summary>
        /// Velocity of a projectile fired
        /// </summary>
        private const int FIRE_VELOCITY = 50;

        /// <summary>
        /// Base damage dealt with a projectile (when rage = 100%)
        /// </summary>
        private const int FIRE_BASE_DAMAGE = 1;

        /// <summary>
        /// Max damage dealt with a projectile (when rage = 0%)
        /// </summary>
        private const int FIRE_MAX_DAMAGE = 10;

        /// <summary>
        /// Base distance an opponent will flinch when hit with a projectile
        /// </summary>
        private const float FIRE_MIN_FLINCH = 0;
         
        /// <summary>
        /// Max distance an opponent will flinch when hit with a projectile
        /// </summary>
        private const float FIRE_MAX_FLINCH = 20;

        /// <summary>
        /// Width of the character's damage taking hitbox
        /// </summary>
        private const float BEPU_PHYSICS_WIDTH = 1 * SCALE;

        /// <summary>
        /// Height of the character's damage taking hitbox
        /// </summary>
        private const float BEPU_PHYSICS_HEIGHT = 2.6f * SCALE;

        /// <summary>
        /// Depth of the character's damage taking hitbox
        /// </summary>
        private const float BEPU_PHYSICS_DEPTH = 1 * SCALE;
        

        public FightingRobot(int lives, Vector3 spawnPosition, PlayerIndex controllingPlayer, int team, Environment environment, Color color)
            : base("FightingRobot", lives, new Box(spawnPosition, BEPU_PHYSICS_WIDTH, BEPU_PHYSICS_HEIGHT, BEPU_PHYSICS_DEPTH, MASS), 
            controllingPlayer, team, environment, color)
        {

            grabbableComponent = new GrabbableComponent(this, THROW_MIN_FLINCH, THROW_MAX_FLINCH);
            this.AttachComponent(grabbableComponent);
    
            BasicModelComponent drawComponent = new RobotRenderComponent(this, BEPU_PHYSICS_HEIGHT, SCALE, color);
            this.AttachComponent(drawComponent);

            //BasicModelComponent drawComponent2 = new CubeRenderComponent(this, Matrix.CreateScale(BEPU_PHYSICS_WIDTH, BEPU_PHYSICS_HEIGHT, BEPU_PHYSICS_WIDTH));
            //this.AttachComponent(drawComponent2);
            
            JumpComponent jumpComponent = new JumpComponent(this, JUMP_HEIGHT, MAX_JUMPS, environment);
            this.AttachComponent(jumpComponent);

            MoveComponent moveComponent = new MoveComponent(this, SPEED);
            this.AttachComponent(moveComponent);

            grabComponent = new GrabComponent(this, environment, THROW_BASE_DAMAGE, THROW_MAX_DAMAGE);
            this.AttachComponent(grabComponent);

            punchComponent = new PunchComponent(this, environment, PUNCH_SPEED, PUNCH_BASE_DAMAGE, PUNCH_MAX_DAMAGE, PUNCH_MIN_FLINCH, PUNCH_MAX_FLINCH);
            this.AttachComponent(punchComponent);

            FireProjectileComponent fireProjectileComponent = new FireProjectileComponent(this, FIRE_SPEED, FIRE_VELOCITY, environment, FIRE_BASE_DAMAGE, FIRE_MAX_DAMAGE, FIRE_MIN_FLINCH, FIRE_MAX_FLINCH);
            this.AttachComponent(fireProjectileComponent);

        }

  
    }
}
