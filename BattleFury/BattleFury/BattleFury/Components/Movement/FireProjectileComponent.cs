using BattleFury.EntitySystem;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Entities.Items;
using BattleFury.Entities;
using BattleFury.Entities.Characters;
using BattleFury.SoundManager;

namespace BattleFury.Components.Movement
{
    public class FireProjectileComponent : Component
    {

        private Environment environment;

        /// <summary>
        /// Number of milliseconds between firing
        /// </summary>
        private int fireSpeed;

        /// <summary>
        /// Milliseconds until anotehr fire is allowed. Firing enabled when <= 0.
        /// </summary>
        private int timeTillFire = 0;

        /// <summary>
        /// Speed of the moving projectile.
        /// </summary>
        private int fireVelocity;

        private PlayerIndex controllingPlayer;

        private MoveComponent movementComponent;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private VitalityComponent health;

        /// <summary>
        /// Strength of the projectile attack.
        /// </summary>
        private float baseDamage;

        private float maxDamage;

        private float minFlinch;

        private float maxFlinch;

        public FireProjectileComponent(Entity parent, int fireSpeed, int fireVelocity, Environment environment, float baseDamage, float maxDamage, float minFlinch, float maxFlinch)
            : base(parent, "FireProjectileComponent")
        {
            this.fireSpeed = fireSpeed;
            this.environment = environment;
            this.fireVelocity = fireVelocity;
            this.baseDamage = baseDamage;
            this.maxDamage = maxDamage;
            this.minFlinch = minFlinch;
            this.maxFlinch = maxFlinch;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.movementComponent = (MoveComponent)Parent.GetComponent("MoveComponent");
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
            this.health = (VitalityComponent)Parent.GetComponent("VitalityComponent");
        }

        public override void Update(GameTime gameTime)
        {
            timeTillFire -= gameTime.ElapsedGameTime.Milliseconds;

            // Fire a shot if the timer allows.
            if (GameplayBindings.IsFire(controllingPlayer) && timeTillFire <= 0)
            {
                AudioManager.PlayFireLaser();
                timeTillFire = fireSpeed;

                int direction = movementComponent.DirectionX;

                // Determine how much damage to do. This will scale linearly
                float rage = this.health.RageMeter;
                float damage = baseDamage + ((maxDamage - baseDamage) / 99) * (100 - rage);
                if (rage == 0)
                {
                    damage *= 2; // DOUBLE DAMAGE! RAAAAAAAAAGE MODE.
                }

                // Create a projectile and add it to the environment.
                Vector3 spawnPosition;
                if (GameSettings.PunchJumpMode)
                {
                    spawnPosition = this.bepuPhysicsComponent.Box.Position + new Vector3(movementComponent.DirectionX, 0, 0);
                }
                else
                {
                    spawnPosition = this.bepuPhysicsComponent.Box.Position + new Vector3(movementComponent.DirectionX * 1.435f, 0, 0);
                }
                Vector3 velocity = new Vector3(movementComponent.DirectionX, 0, 0) * fireVelocity;
                Projectile p = new Projectile(spawnPosition, velocity, environment, (Character) Parent, damage, minFlinch, maxFlinch);
                p.Initialize();
                environment.ItemManager.AddItem(p);
            }



        }
    }
}
