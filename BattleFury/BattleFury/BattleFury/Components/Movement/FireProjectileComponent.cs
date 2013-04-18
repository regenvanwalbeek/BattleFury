using BattleFury.EntitySystem;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Entities.Items;
using BattleFury.Entities;

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

        private MovementComponent movementComponent;

        private BepuPhysicsComponent bepuPhysicsComponent;

        public FireProjectileComponent(Entity parent, int fireSpeed, int fireVelocity, Environment environment)
            : base(parent, "FireProjectileComponent")
        {
            this.fireSpeed = fireSpeed;
            this.environment = environment;
            this.fireVelocity = fireVelocity;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.movementComponent = (MovementComponent)Parent.GetComponent("MovementComponent");
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(GameTime gameTime)
        {
            timeTillFire -= gameTime.ElapsedGameTime.Milliseconds;

            // Fire a shot if the timer allows.
            if (GameplayBindings.IsFire(controllingPlayer) && timeTillFire <= 0)
            {
                timeTillFire = fireSpeed;

                int direction = movementComponent.DirectionX;
                
                // Create a projectile and add it to the environment.
                Projectile p = new Projectile(this.bepuPhysicsComponent.Box.Position + new Vector3(movementComponent.DirectionX, 0, 0), new Vector3(movementComponent.DirectionX, 0, 0) * fireVelocity);
                p.Initialize();
                environment.Spawner.AddItem(p);
            }



        }
    }
}
